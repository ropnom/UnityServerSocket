using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


public class createObject : MonoBehaviour {

	[SerializeField]
	GameObject o;

	GameObject newo;
	//InputField description;
	string description;
	private List<Device> cola;
	private bool hacercola = false;
	private static int position;
	//string Informacion="3:1:35:2.7:Conserje;4:2:42.93:2.34:Marico;4:2:42.1:3.6:Marico";
	// Use this for initialization

	void Start () {
		//description= GameObject.FindGameObjectWithTag("textin").GetComponent<InputField>(); //Create a tag in InputFiel
		hacercola = false;
		position = 0;
		List<Device> listdevices = new List<Device>();
		Device a = new Device();
		a.setIdentification(2);
		a.setType(1);
		a.setLatitude(41.2712);
		a.setLongitude (1.9865);
		a.setDescription("Dinamico creado");
		listdevices.Add(a);
		Device a2 = new Device();
		a2.setIdentification(3);
		a2.setType(3);
		a2.setLatitude(41.2782);
		a2.setLongitude (1.9866);
		a2.setDescription("Dinamico creado2555555555555");
		listdevices.Add(a2);
		newObject(listdevices);

		//newObject();


	}

	public void putCola(List<Device> acola)
	{
		//Debug.Log ("-->Aqui llega 5");
		cola = acola;
		hacercola = true;
		//Debug.Log ("-->Aqui llega 6");
	}

	public void newObject(List<Device> listdevices)
	{
		
		newo = GameObject.Instantiate (o); // new object is a instance of a prefab
		//newo.GetComponent<VirtualObjectMap2>().SetLatLon();
		//newo.GetComponent<VirtualObjectMap2> ().SetDesc ("Prueba de dynamismo");
		//string mensaje = "Dinamico creado2555555555555";

		//newo.GetComponent<VirtualObjectMap2>().ContructVirtualObjectMap2 (1,41.2752,1.982,"Dinamico creado2555555555555");
		//1,441.2752,1.982,"Dinamico creado2555555555555"
		//List<Device> listdevices
		for (int j = 0; j < listdevices.Count; j++) {
			//Crear cada uno de lsoobjetos 
			newo = GameObject.Instantiate (o); // new object is a instance of a prefab
			Debug.Log ("-->Posicion asignada: "+position);
			newo.GetComponent<VirtualObjectMap2>().ContructVirtualObjectMap2(listdevices[j].getType(),listdevices[j].getLatitude(),listdevices[j].getLongitude(),listdevices[j].getDescription(),position);
			//newo.GetComponent<VirtualObjectMap2>().SetLatLon();
			//Debug.Log ("Text" + description);
			//newo.GetComponent<VirtualObjectMap2>().SetDesc("Dinamico");
			position++;
		}
	}
	
	// Update is called once per frame
	void Update () {

		if (hacercola) {
			Debug.Log ("-->Aqui llega 7");
			newObject(cola);
			cola = null;
			hacercola = false;
		} 

	}
}
