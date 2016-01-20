package Cliente;

import java.net.InetAddress;
import java.util.ArrayList;
import java.util.List;

import Model.Device;
import Model.TCPconnection;

public class ClientAPP2 {

	protected static TCPconnection tcp;
	protected static int etapa = 0;
	protected static int puerto = 52000;
	protected static InetAddress ip;
	protected static int intentos = 0;
	protected static Device unityclient;
	public static boolean working = false;

	// Lista de devices
	protected static List<Device> devices = new ArrayList<Device>();

	public static void Update() {
		Write(unityclient.GenMessage());
	}

	protected static void DATA() {
		Write("DATA;");
	}

	protected static void Write(String message) {

		String mensajeserver = "<Client_" + unityclient.getIdentification() + " -->" + message;
		System.out.println(mensajeserver);
		tcp.Write(message);
	}

	protected static void UpdateData(String mensaje) {

	}

	public static void main(String[] args) {

		double lat = 41.272775;
		double longitude = 1.988673;
		
		try{
			Thread.sleep(500);
			}catch(Exception e){
				
			}

		try {

			// Iniciamos el programa test cliente
			System.out.println("---------------------------------------");
			System.out.println("**** INICIANDO EMULADOR DE DEVICE  ****");
			System.out.println("---------------------------------------");
			System.out.println();
			System.out.println("realizando random de seleccion de objeto:");

			try {
				ip = InetAddress.getByName("localhost");
			} catch (Exception e) {
				e.printStackTrace();
			}

			// random number 0-4
			int random = (int) (5 * Math.random());
			
			int random2 = (int) (3 * Math.random());
			int random3 = (int) (3 * Math.random());
			
			lat = lat + Math.pow(-1, random2)*0.005*Math.random();
			longitude = longitude + Math.pow(-1, random3)*0.005*Math.random();
			
			random = 1;

			unityclient = new Device();

			switch (random) {

			case 0:
				unityclient.setType(1);
				unityclient.setLongitude(longitude);
				unityclient.setLatitude(lat);
				unityclient.setDescription("Soy un pelapatatas");

				break;

			case 1:
				unityclient.setType(1);
				unityclient.setLongitude(longitude);
				unityclient.setLatitude(lat);
				unityclient.setDescription("Soy un tecnico");

				break;

			case 2:
				unityclient.setType(2);
				unityclient.setLongitude(longitude);
				unityclient.setLatitude(lat);
				unityclient.setDescription("Soy un alumno");
				break;

			case 3:
				unityclient.setType(3);
				unityclient.setLongitude(longitude);
				unityclient.setLatitude(lat);
				unityclient.setDescription("Soy un profesor");
				break;

			case 4:
				unityclient.setType(4);
				unityclient.setLongitude(longitude);
				unityclient.setLatitude(lat);
				unityclient.setDescription("Soy un conserje");
				break;

			}


			String recibido = "";
			String[] data = null;
			String[] data2 = null;

			int etapa = 0;

			while (etapa != -1) {

				switch (etapa) {
				case 0:
					System.out.println("-- Iniciando Cliente Unity --");
					try {
						tcp = new TCPconnection(ip, puerto);
						System.out.println("** Realizamos peticion de Cliente:");
						Update();
						etapa = 1;
					} catch (Exception e) {
						// printamos lasposibles excepciones
						e.printStackTrace();
						System.out.println(" Error en la conexion -- FAIL ");
						intentos++;
						etapa = 0;
						if (intentos == 5) {
							System.out.println("Cancelacion de conexion");
							etapa = -1;
						}

					}

					break;

				case 1:
					// Recibimos identificador
					System.out.println();
					System.out.println("--Contestacion--");
					recibido = tcp.Read();
					System.out.println("<Servidor>:" + recibido);
					data = recibido.split(";");
					intentos = 0;

					try {
						// recbir ACK y coger identificador.
						if (data.length == 2) {
							data2 = data[1].split(":");
							unityclient.setIdentification(Integer.parseInt(data2[0]));
							unityclient.setPosition(Integer.parseInt(data2[1]));

							working = true;
							Thread thread = new Thread() {
								public void run() {
									while (ClientAPP2.working) {
										ClientAPP2.Update();
										int tiempo = (int) (6000+5000 * Math.random());
										try {
											Thread.sleep(tiempo);
										} catch (InterruptedException e) {
											// TODO Auto-generated catch block
											e.printStackTrace();
										}
									}
								}
							};
							thread.start();

							etapa = 2;
						}

					} catch (Exception e) {
						e.printStackTrace();
						etapa = 1;
						if (intentos == 1) {
							System.out.println("reintentar la conexion");
							etapa = 0;
						}
					}
					break;

				case 2:
					System.out.println();
					System.out.println("**->Peticion de Datos al server...");
					DATA();
					System.out.println(" Esperando datos ... ");
					System.out.println();

					recibido = tcp.Read();
					System.out.println("<Servidor>:" + recibido);
					try {
						data = recibido.split(";");
						for (int j = 2; j < data.length; j++) {
							data2 = data[j].split(":");
							Device a = new Device();
							a.setIdentification(Integer.parseInt(data2[0]));
							a.setType(Integer.parseInt(data2[1]));
							a.setLatitude(Double.parseDouble(data2[2]));
							a.setLongitude(Double.parseDouble(data2[3]));
							a.setDescription(data2[4]);
							devices.add(a);
						}
						etapa = 3;

					} catch (Exception e) {
						// printamos posibles excepciones
						e.printStackTrace();
						etapa = 2;
					}

					break;

				case 3:
					System.out.println("Recibiendo Datos del server...");
					recibido = tcp.Read();
					System.out.println("<Servidor>:" + recibido);
					try {
						UpdateData(recibido);

					} catch (Exception e) {
						// printamos posibles excepciones
						e.printStackTrace();
					}
					//etapa = 1;
					break;

				default:
					System.out.println("Error en el proceso, Protocolo reiniciado.");
					etapa = 1;
					break;
				}

			}
		} catch (Exception e) {
			working = false;
			e.printStackTrace();
			System.out.println("***************");
			System.out.println("***   END  ****");
			System.out.println("***************");


		}

	}
}
