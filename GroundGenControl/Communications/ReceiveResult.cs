using System;

namespace GroundGenControl.Communications
{
	/// <summary>
	/// Summary description for ReceiveResult.
	/// </summary>
	public class ReceiveResult : IAsyncResult
	{

		private String	_receiveString	= "";
		private String	_errorString	= "";
        private String  _nextGatewayTime = "";
		private bool	_exceptionState	= false;
		private bool	_connected		= false;
		

		public ReceiveResult()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		#region IAsyncResult Members

		public object AsyncState
		{
			get
			{
				// TODO:  Add ReceiveResult.AsyncState getter implementation
				return null;
			}
		}

		public bool CompletedSynchronously
		{
			get
			{
				// TODO:  Add ReceiveResult.CompletedSynchronously getter implementation
				return false;
			}
		}

		public System.Threading.WaitHandle AsyncWaitHandle
		{
			get
			{
				// TODO:  Add ReceiveResult.AsyncWaitHandle getter implementation
				return null;
			}
		}

		public bool IsCompleted
		{
			get
			{
				// TODO:  Add ReceiveResult.IsCompleted getter implementation
				return true;
			}
		}

		public String receiveString
		{
			get
			{
				return _receiveString;
			}

			set
			{
				_receiveString = value;
			}

		}

        public String nextGatewayTime
        {
            get
            {
                return _nextGatewayTime;
            }
            set
            {
                _nextGatewayTime = value;
            }
        }

		public String errorString
		{
			get
			{
				return _errorString;
			}

			set
			{
				_errorString = value;
			}

		}

		public bool exceptionState
		{
			get
			{
				return _exceptionState;
			}

			set
			{
				_exceptionState = value;
			}

		}

		
		public bool connected
		{
			get
			{
				return _connected;
			}

			set
			{
				_connected = value;
			}

		}

		public int bytesReceived
		{
			get
			{
				return _receiveString.Length;
			}



		}

		#endregion
	}
}
