using System;
using System.Windows.Forms;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace GroundGenControl.Connection
{
	/// <summary>
	/// Summary description for RemoteConnection.
	/// </summary>
	public class RemoteConnection
	{

		public class	ServicePorts
		{
			public const int	INFOSAT = 9000;

		}

		Socket	   _sock				= null;
		Byte[]	   _receiveBuffer = new Byte[512];


		public RemoteConnection()
		{
		}



		public void connectTo( string strIPAddress, int intPort, AsyncCallback callback )
		{
			disconnect();
			IPEndPoint remoteIPEndPoint = new IPEndPoint(IPAddress.Parse(strIPAddress), intPort); 
			EndPoint remoteEndPoint = (EndPoint) remoteIPEndPoint; 
			_sock = new Socket(AddressFamily.InterNetwork,SocketType.Stream, ProtocolType.Tcp);
			//_sock.Connect(remoteEndPoint);

            _sock.BeginConnect(remoteEndPoint, new AsyncCallback(callback), _sock);


		//	connectTo
		}

		public void disconnect()
		{

			if ( _sock == null)
			{
				return;
			}

			_sock.Close();
			_sock = null;

		//	disconnect
		}

		public void startSendString( string strString, AsyncCallback callback )
		{

			if (_sock == null)
			{
				throw new Exception("Unable to start async. send, no socket established");
			}

			// Get request string as byte array
			Byte[] sendBytes = Encoding.ASCII.GetBytes(strString);
			_sock.BeginSend(sendBytes,0,sendBytes.Length,SocketFlags.None,callback,_sock);
	
		}

		public void startReceiveString(AsyncCallback callback)
		{

			if (_sock == null)
			{
				throw new Exception("No connection established to this site");
			}

			//Byte[] receiveBytes = new Byte[512];
//			_sock.Receive(receiveBytes);


			System.Text.ASCIIEncoding encoder = new System.Text.ASCIIEncoding();
			_sock.BeginReceive(_receiveBuffer,0,_receiveBuffer.Length,SocketFlags.None,callback,_sock);


		}

		public string receiveString
		{
			get
			{
				System.Text.ASCIIEncoding encoder = new System.Text.ASCIIEncoding();
				return	encoder.GetString(_receiveBuffer);
			}
		}


	}
}
