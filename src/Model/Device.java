package Model;

public class Device extends Thread {

	protected int identification;
	protected int type;
	protected double latitude;
	protected double longitude;
	protected String Description;
	protected TCPconnection tcp = null;
	protected int position;
	protected int etapa=0;

	public Device() {

	}

	public synchronized void Write(String message) {
		String mensajeserver = "<Server>-<Client_" + identification + ">: " + message;
		System.out.println(mensajeserver);
		tcp.Write(message);
	}

	public String GenMessage() {
		String message = this.identification + ":" + this.type + ":" + this.latitude + ":" + this.longitude + ":" + this.Description;
		return message;
	}

	@Override
	public void run() {

		etapa = 0;

		while (etapa != -1) {
			
			switch (etapa) {

			case 0:
				// write ACK using ID
				this.identification = ThreadDevicemanager.getIdentificador();
				Write("AKC;" + this.identification+":"+this.position);
				etapa = 1;
				break;

			case 1:
				// Waiting update information
				try {
					// Processing Update
					String mensaje = tcp.Read();
					System.out.println("<Client_ "+identification+"> envia: " + mensaje);
					// System.out.println("Aquiviene el split");
					String[] campos = mensaje.split(";");
					if(campos[0].equals("DATA"))
					{
						//Gen messageData
						Write(ThreadDevicemanager.GenDevicesDAta(this.position));
										
					}
					etapa = 2;
					
				} catch (Exception e) {
					// printamos posibles excepciones
					e.printStackTrace();
					etapa = -1;
				}
				
				break;

			case 2:
				// Processing Update
				String mensaje = tcp.Read();
				System.out.println("<Client_ "+identification+"> envia: " + mensaje);
				try {

					// System.out.println("Aquiviene el split");
					String[] nombre = mensaje.split(":");
					if (nombre.length != 5) {
						etapa = -1;
						throw new Exception("La longitud del protocolo es " + nombre.length);
					}
					this.identification = Integer.parseInt(nombre[0]);
					this.type = Integer.parseInt(nombre[1]);
					this.latitude = Double.parseDouble(nombre[2]);
					this.longitude = Double.parseDouble(nombre[3]);
					this.Description = nombre[4];

					// Enviamso mensaje a todo los device de la actulizacion
					ThreadDevicemanager.UpdateInfo(this.position);
					etapa = 1;

				} catch (Exception e) {
					e.printStackTrace();
					System.out.println("Error de sintaxis en protocolo ");
					System.out.println("-- server envia: '-1 --'");
					tcp.Write("-1");
					etapa = -1;
				}
				

			default:
				break;

			}
			//System.out.println("Etapa: "+etapa);

		}

	}

	protected void CloseSockets() {
		Write("END");
		etapa = -1;
		tcp.closecliente();
	}

	// GETs & SETs

	public int getType() {
		return type;
	}

	public void setType(int type) {
		this.type = type;
	}

	public String getDescription() {
		return Description;
	}

	public void setDescription(String description) {
		Description = description;
	}

	public double getLatitude() {
		return latitude;
	}

	public void setLatitude(double latitude) {
		this.latitude = latitude;
	}

	public double getLongitude() {
		return longitude;
	}

	public void setLongitude(double longitude) {
		this.longitude = longitude;

	}

	public TCPconnection getTcp() {
		return tcp;
	}

	public void setTcp(TCPconnection tcp) {
		this.tcp = tcp;
	}

	public int getIdentification() {
		return identification;
	}

	public void setIdentification(int identification) {
		this.identification = identification;
	}

	public int getPosition() {
		return position;
	}

	public void setPosition(int position) {
		this.position = position;
	}

}
