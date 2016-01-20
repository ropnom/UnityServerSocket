using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Threading;

public class SocketClient : MonoBehaviour {

	static internal Boolean socketReady = false;
	private static TcpClient mySocket;
	private static NetworkStream theStream;
	private static StreamWriter theWriter;
	private static StreamReader theReader;
	private static String Host = "localhost";
	private static Int32 Port = 52000;
	private static createObject creator;

	private static Device mydevice;
	private static int etapa = 0;
	private static GpsWithoutUi gps;
	private static List<Device> listdevices;
	private Thread _t1;



	// Use this for initialization
	void Start () {
		listdevices = new List<Device>();	
		mydevice = new Device ();
		mydevice.setType (4);
		mydevice.setLatitude (41.278);
		mydevice.setLongitude (1.984);
		mydevice.setDescription ("Cliente Unity");
		_t1 = new Thread(Socketfunction);
		_t1.Start ();
	}

	void Awake()
	{
		gps = GameObject.FindGameObjectWithTag("Gps").GetComponent<GpsWithoutUi>();
		creator= GameObject.FindGameObjectWithTag("creador").GetComponent<createObject>();
	}

	public static Device getDevice(int index){
		try{
			return listdevices [index];
		}catch(Exception e){
			return null;
		}

	}

	public static void UpdateGPS(){
		if (gps.Latitude != 0.0f)
		{
			PositionUtilities.Init();
			//PositionUtilities.Zoom=15;
			
			float y = (float)PositionUtilities.DrawCubeY((double)gps.Latitude);
			float x = (float)PositionUtilities.DrawCubeX((double)gps.Longitude);
			
			mydevice.setLatitude(y);
			mydevice.setLongitude(x);

			if(socketReady){
				//int randomNumber = Random.Range(0,20);
				//if(randomNumber == 10){
				UpdateInfo();
				//}
			}
			
		}

	}
	static void UpdateInfo(){
		//Debug.Log ("<Client>: "+ mydevice.GenMessage());
		writeSocket(mydevice.GenMessage());
	}
	static void UpdateDevice(){

		string mesage = readSocket ();
		Debug.Log ("--> Actulizando device:");
		string[] data = mesage.Split (';');
		string[] data2;
		//try {
		data = mesage.Split(';');	
		data2 = data[0].Split(':');
		bool encontrado = false;
		for (int j = 0; j < listdevices.Count; j++) {
			if(listdevices[j].getIdentification() ==Convert.ToInt32 (data2 [0])){			
				listdevices[j].setType (Convert.ToInt32 (data2 [1]));
				listdevices[j].setLatitude (Convert.ToDouble (data2 [2]));
				listdevices[j].setLongitude (Convert.ToDouble (data2 [3]));
				listdevices[j].setDescription (data2 [4]);
				encontrado = true;
			}
		}

		Debug.Log ("--> Device actualizado");

		if (!encontrado) {
			Debug.Log ("--> Creando nuevo device");
			Device a = GameObject.Instantiate(mydevice);
			a.setIdentification(Convert.ToInt32(data2[0]));
			a.setType(Convert.ToInt32(data2[1]));
			a.setLatitude(Convert.ToDouble(data2[2]));
			a.setLongitude(Convert.ToDouble(data2[3]));
			a.setDescription(data2[4]);
			listdevices.Add(a);
		
			//añadimso lso objetos que se van actulizando.
			List<Device> newobjet =  new List<Device>();
			newobjet.Add(a);
			creator.putCola(newobjet);
		}		
	}
	static void GetData(){

		writeSocket("DATA;");
	}

	static void ProcesarData(){
		string mesage = readSocket ();
		Debug.Log ("--> Procesando Datos llegados");
		string[] data = mesage.Split (';');
		string[] data2;

		try {
			data = mesage.Split(';');
			Debug.Log ("-->Data: "+data[0]);
			Debug.Log ("-->Usuarios: "+(data.Length-2));

			for (int j = 2; j < data.Length-1; j++) {
				Debug.Log ("-->Data: "+data[j]);
				data2 = data[j].Split(':');
				Device a = new Device();

				//Debug.Log ("-->Aqui llega 1 j: "+j);
				a.setIdentification(Convert.ToInt32(data2[0]));
				a.setType(Convert.ToInt32(data2[1]));
				//Debug.Log ("-->Aqui llega 2");
				a.setLatitude(Convert.ToDouble(data2[2]));
				a.setLongitude(Convert.ToDouble(data2[3]));
				//Debug.Log ("-->Aqui llega 3");
				a.setDescription(data2[4]);
				listdevices.Add(a);
				etapa = 3;
			}
			//Hacer que los devices se pongna en el mapa  ** Con ANA **
			Debug.Log ("--> Datos procesados son: "+listdevices.Count);

			//Debug.Log ("-->Aqui llega 4");
			creator.putCola(listdevices);


		} catch (Exception e) {
			// printamos posibles excepciones
			Debug.Log ("*** Catch: "+e.ToString());
			etapa = -1;

		}
	}

	static void ACKInfo(){
		string mesage = readSocket ();
		Debug.Log ("<Server>: "+mesage);
		string[] data = mesage.Split (';');
		string[] data2;

		if (data.Length == 2) {
			data2 = data [1].Split (':');
			mydevice.setIdentification (Convert.ToInt32(data2 [0]));
			mydevice.setPosition (Convert.ToInt32 (data2 [1]));
		}
	}

	// Update is called once per frame
	void Update () {


	
	}

	void Socketfunction(){

		setupSocket ();
		//writeSocket ("0:2:42.15:2.31:TestObjeto");
		Debug.Log ("************************");
		Debug.Log ("Inicializamos el socket");
		Debug.Log ("************************");

		etapa = 0;
		while (etapa !=-1) {

			switch (etapa) {
			case 0:
			//Enviamos el primer Update
				//Debug.Log ("LLega a case 0");
				UpdateInfo ();
				//Debug.Log ("sale case cero");
				etapa = 1;
				break;
			
			case 1:
			// Recibimos identificador
			// AKC + identificador + posicion
				ACKInfo ();
				etapa = 2;
				break;
			
			case 2:
			//Reque the data of the virtualobjets to the server
				GetData ();
				ProcesarData ();
			//Modificar el mapa crearlos...
				break;
			
			case 3:
				socketReady = true;
				UpdateDevice ();
				break;
			
			}
			Debug.Log ("--> Nueva etapa: " + etapa);
		}


	}



	// **********************************************
	public static void setupSocket() {
		try {
			mySocket = new TcpClient(Host, Port);
			theStream = mySocket.GetStream();
			theWriter = new StreamWriter(theStream);
			theReader = new StreamReader(theStream);
			socketReady = true;
		}
		catch (Exception e) {
			Debug.Log("Socket error: " + e);
		}
	}
	public static void writeSocket(string theLine) {
		if (!socketReady)
			return;
		String foo = theLine + "\r\n";
		theWriter.Write(foo);
		theWriter.Flush();
		Debug.Log ("<Client>: "+ theLine);
	}
	public static String readSocket() {
		if (!socketReady)
			return "";
		try {
			String message = theReader.ReadLine();
				Debug.Log ("<Server>: "+ message);
			return message;
		} catch (Exception e) {
			return "";
		}
	}
	public static void closeSocket() {
		if (!socketReady)
			return;
		theWriter.Close();
		theReader.Close();
		mySocket.Close();
		socketReady = false;
	}
 // end class s_TCP
}
