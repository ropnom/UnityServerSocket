package Model;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.PrintWriter;
import java.net.DatagramPacket;
import java.net.DatagramSocket;
import java.net.InetAddress;

public class UDPconnection {

	// inicializamos los objetosnecesario
	DatagramSocket socket = null;
	PrintWriter out = null;
	BufferedReader in = null;
	InetAddress ipserver = null;
	InetAddress ipclient = null;
	int spuerto = 0;
	int cpuerto = 0;

	// creamos los paquetes de recepcion y de salida
	byte[] receiveData = new byte[1500];
	byte[] sendData = new byte[1500];
	DatagramPacket sendPacket = null;
	DatagramPacket receivePacket = null;

	// contructor para cliente
	public UDPconnection(InetAddress ip, int puerto) {
		this.ipclient = ip;
		this.cpuerto = puerto;
		ArrancarCliente();
	}

	// contructor server
	public UDPconnection(int puerto) {
		this.spuerto = puerto;
	}

	public void ArrancarServer() {
		try {
			socket = new DatagramSocket(spuerto);
			receivePacket = new DatagramPacket(receiveData, receiveData.length);
			socket.receive(receivePacket);
			System.out.println(socket);
			ipclient = receivePacket.getAddress();
			cpuerto = receivePacket.getPort();
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	public void ArrancarCliente() {

		try {
			// inicializamos el Socket Datagrama
			socket = new DatagramSocket();

		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	public void writeLineServer(String line) {

		sendData = line.getBytes();
		DatagramPacket sendPacket = new DatagramPacket(sendData, sendData.length, ipclient, cpuerto);

		try {
			socket.send(sendPacket);
		} catch (IOException e) {
			e.printStackTrace();
			System.out.println("Error al enviar");
		}
	}

	public void writeLineClient(String line) {

		sendData = line.getBytes();
		DatagramPacket sendPacket = new DatagramPacket(sendData, sendData.length, ipclient, cpuerto);

		try {
			socket.send(sendPacket);
		} catch (IOException e) {
			e.printStackTrace();
			System.out.println("Error al enviar");
		}
	}

	public String readLine() {

		String line = "null";
		try {
			DatagramPacket receivePacket = new DatagramPacket(receiveData, receiveData.length);
			socket.receive(receivePacket);
			line = new String(receivePacket.getData());
		} catch (Exception e) {
			e.printStackTrace();
		}
		return line;
	}

	public void close() {
		try {

			if (socket != null)
				socket.close();
		} catch (Exception e) {
			e.printStackTrace();
			System.out.println("Problemas al cerrar el socket");
		}
		return;
	}

}
