package Model;

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.io.PrintWriter;
import java.net.InetAddress;
import java.net.ServerSocket;
import java.net.Socket;

public class TCPconnection {

	// inicializamos el socket el buffer de lectura y la salida
	Socket socketcliente = null;
	ServerSocket socketserver = null;
	PrintWriter out = null;
	InputStreamReader instrem = null;
	BufferedReader in = null;
	InetAddress ip = null;
	int puerto;

	// constructor del socket del cliente
	public TCPconnection(InetAddress ip, int puerto) throws Exception {

		this.ip = ip;
		this.puerto = puerto;
		// arancamos el servidor una vez definido
		ArrancarCliente();
	}

	// constructor copia
	public TCPconnection(TCPconnection s) {

		this.ip = s.getIp();
		this.puerto = s.getPuerto();
		this.socketcliente = s.getSocketcliente();
		this.socketserver = s.getSocketserver();
		this.out = s.getOut();
		this.instrem = s.getInstrem();
		this.in = s.getIn();

	}

	public Socket getSocketcliente() {
		return socketcliente;
	}

	public ServerSocket getSocketserver() {
		return socketserver;
	}

	public PrintWriter getOut() {
		return out;
	}

	public InputStreamReader getInstrem() {
		return instrem;
	}

	public BufferedReader getIn() {
		return in;
	}

	public InetAddress getIp() {
		return ip;
	}

	public int getPuerto() {
		return puerto;
	}

	public TCPconnection(int puerto) {

		this.puerto = puerto;
		// arrancamos el server

	}

	public void ArrancarCliente() throws Exception {

		// iniciamos el socket
		socketcliente = new Socket(ip, puerto);
		// printamos salida
		System.out.println(socketcliente);
		// creamos el buffer salida
		out = new PrintWriter(socketcliente.getOutputStream(), true);
		// creamos el input stream y su buffer asociado
		instrem = new InputStreamReader(socketcliente.getInputStream());
		in = new BufferedReader(instrem);
		System.out.println(" Cliente inicializado correctamente -- OK ");

	}

	public void ArrancarServer() {

		try {
			// ponemos el socket del server en espera y acepta las entradas
			socketserver = new ServerSocket(puerto);
			
			contestarcliente();
		} catch (Exception e) {
			// printamos posibles excepciones
			e.printStackTrace();
		}
	}

	public void contestarcliente() throws Exception {
		socketcliente = socketserver.accept();
		// socketserver.setReuseAddress(true);
		// socketserver.bind(new InetSocketAddress(51000));
		// aceptamos el flujo entrante y saliente de datos
		out = new PrintWriter(socketcliente.getOutputStream(), true);
		in = new BufferedReader(new InputStreamReader(socketcliente.getInputStream()));
		System.out.println("Recepcion de Cliente:");
		System.out.print(socketcliente);
		System.out.println();
	}

	public String Read() {
		// realizamso una lectura del socket
		String line = null;

		try {
			// leemos el buffer de entrada
			line = in.readLine();

		} catch (Exception e) {
			// printamos posibles excepciones
			e.printStackTrace();
		}
		// devolvemso el valor obtenido
		return line;
	}

	public void Write(String line) {

		try {
			// preparamso la linea y la enviamos
			out.write(line + "\n");
			out.flush();
		} catch (Exception e) {
			// printamos excepciones
			e.printStackTrace();
		}

	}

	public void close() {
		// cerramos el socket
		closecliente();
		closeserver();

	}

	public void closecliente() {
		try {

			if (out != null)
				out.close();
			if (socketcliente != null)
				socketcliente.close();

		} catch (Exception e) {
			// printamos excepciones
			e.printStackTrace();
			System.out.println(" Error en cierre de Sockets -- FAIL ");
		}
	}

	public void closeserver() {
		try {
			if (in != null)
				in.close();
			if (socketserver != null)
				socketserver.close();
			if (instrem != null)
				instrem.close();
		} catch (Exception e) {
			// printamos excepciones
			e.printStackTrace();
			System.out.println(" Error en cierre de Sockets -- FAIL ");
		}
	}

}