package Server;

import java.util.Date;
import java.util.Scanner;

import Model.TCPconnection;
import Model.ThreadDevicemanager;

public class RunServer {

	// PArametros del servidor
	protected static TCPconnection tcp = null;
	protected static int etapa = 0;
	protected static int puerto = 52000;

	public static void CerrarServer() {
		etapa = -1;
	}

	@SuppressWarnings("deprecation")
	public static void main(String[] args) {

		System.out.println("*******************");
		System.out.println(" Servidor iniciado ");
		System.out.println("*******************");

//		System.out.println("Introduzca el timepo que desea que funcione [seg]:");
//		int segundos = 180;
//		Scanner a = new Scanner(System.in);
//		try {
//			segundos = Integer.parseInt(a.next());
//		} catch (Exception e) {
//			System.out.println("*******************");
//			System.out.println("Default Value 180 seg.");
//			System.out.println("*******************");
//
//		}
//
//		// Calculamso timepo maximo del server
//		Date now = new Date();
//		now.setSeconds(now.getSeconds() + segundos);

		System.out.println("----------------------------");
		System.out.println(" PROTOCOLO TPC CONCURRENTE ");
		System.out.println("----------------------------");

		// arrancamos el modo escucha del servidor
		while (etapa != -1) {

			switch (etapa) {
			case 0:
				// Solos se hace una vez cuando se arranca el servidor Estado 0
				System.out.println("-- Iniciando Server Esperando Unity devices --");
				tcp = new TCPconnection(puerto);
				System.out.println();
				System.out.println(" Esperando Devices ... ");
				tcp.ArrancarServer();
				etapa = 1;
				break;

			case 1:
				// cuadno llega uncliente lo proceso
				TCPconnection copia1 = new TCPconnection(tcp);
				// metemos el cliente en la pila de devices
				(new ThreadDevicemanager(copia1)).start();
				etapa = 2;

				break;

			case 2:
				try {
					tcp.contestarcliente();
				} catch (Exception e) {
					// printamos posibles excepciones
					e.printStackTrace();
				}
				etapa = 1;
				break;

			default:
				System.out.println("Error en el proceso, Protocolo reiniciado.");
				etapa = 1;
				break;
			}

		}
		System.out.println("*******************");
		System.out.println(" Servidor cerrado ");
		System.out.println("*******************");

	}

}