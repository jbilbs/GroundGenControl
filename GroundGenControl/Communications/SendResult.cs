using System;


namespace GroundGenControl.Communications
{
    public class SendResult : IAsyncResult
    {

        private String  _errorString = "";
        private bool    _exceptionState = false;

        public SendResult()
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


        #endregion
    }
}
