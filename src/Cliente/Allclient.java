package Cliente;

public class Allclient {

	public static void main(String[] args) {
		// TODO Auto-generated method stub
		Thread one = new Thread() {
			public void run() {
				try {
					ClientAPP.main(null);
				} catch (Exception v) {
					System.out.println(v);
				}
			}
		};
		one.start();
		
		Thread two = new Thread() {
			public void run() {
				try {
					ClientAPP2.main(null);
				} catch (Exception v) {
					System.out.println(v);
				}
			}
		};
		two.start();
		
		Thread three = new Thread() {
			public void run() {
				try {
					ClientAPP3.main(null);
				} catch (Exception v) {
					System.out.println(v);
				}
			}
		};
		three.start();

	}

}
