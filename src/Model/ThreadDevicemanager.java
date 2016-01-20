package Model;

import java.util.ArrayList;
import java.util.List;

public class ThreadDevicemanager extends Thread {

	// asing to id
	public static int constant = 1;

	// Lista de devices
	protected static List<Device> devices = new ArrayList<Device>();
	protected TCPconnection tcp = null;

	public ThreadDevicemanager(TCPconnection s) {
		this.tcp = s;
	}

	public static void UpdateInfo(int position) {
		String message = devices.get(position).GenMessage();
		SendMessageALL(message, position);
	}
	
	public static String  GenDevicesDAta( int position) {
		
		String message ="DATA;"+(devices.size()-1)+";";
		for (int i = 0; i < devices.size(); i++) {
			if (i != position){				
				message += devices.get(i).GenMessage()+";";
			}
		}		
		return message;
		
	}

	private static void SendMessageALL(String message, int position) {

		for (int i = 0; i < devices.size(); i++) {
			if (i != position)
				devices.get(i).Write(message);
		}

	}

	public static synchronized int getIdentificador() {

		return (++constant);
	}

	public static void CerrarServer() {
		for (int i = 0; i < devices.size(); i++) {
			devices.get(i).CloseSockets();
		}
		devices = new ArrayList<Device>();
	}

	public void run() {

		String mensaje = tcp.Read();
		System.out.println("<New client>: " + mensaje);

		try {

			// System.out.println("Aquiviene el split");
			String[] nombre = mensaje.split(":");
			if (nombre.length != 5) {
				throw new Exception("La longitud del protocolo es " + nombre.length);
			}

			// Creamos el device con su socket y datos

			Device a = new Device();
			a.setIdentification(Integer.parseInt(nombre[0]));
			a.setType(Integer.parseInt(nombre[1]));
			a.setLatitude(Double.parseDouble(nombre[2]));
			a.setLongitude(Double.parseDouble(nombre[3]));
			a.setDescription(nombre[4]);

			a.setTcp(new TCPconnection(tcp));
			// lanzamos la partida
			a.start();

			// guardamso a en la pila
			a.setPosition(devices.size());
			devices.add(a);

		} catch (Exception e) {
			e.printStackTrace();
			System.out.println("Error de sintaxis en protocolo ");
			System.out.println("-- server envia: '-1 --'");
			tcp.Write("-1");
		} 

	}

}
