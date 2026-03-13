// Decompiled with JetBrains decompiler
// Type: KDAS.String_Tx
// Assembly: KDAS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D88A5B4E-9475-405E-AE8F-191C9E1B6D98
// Assembly location: C:\Users\smbrause\AppData\Local\Apps\2.0\4CYGMBHA.OE2\WYOA0OGM.27R\kdas..tion_55564dd1c036797b_0001.0000_6479263921fe5664\KDAS.exe

using KDAS.Properties;
using System.IO.Ports;
using System.Windows;

namespace KDAS
{
  internal class String_Tx
  {
    private SerialPort ModemPort;

    public String_Tx()
    {
      this.ModemPort = new SerialPort();
      this.Modem_Init();
    }

    public bool sendString(string data)
    {
      try
      {
        this.ModemPort.Write(data);
        return true;
      }
      catch
      {
        int num = (int) MessageBox.Show("error sending string");
        return false;
      }
    }

    public void closeTXport()
    {
      this.ModemPort.Close();
    }

    private void Modem_Init()
    {
      this.ModemPort.PortName = Settings.Default.MODEMport;
      this.ModemPort.BaudRate = 9600;
      this.ModemPort.Parity = Parity.None;
      this.ModemPort.StopBits = StopBits.One;
      this.ModemPort.DataBits = 8;
      this.ModemPort.Handshake = Handshake.None;
      this.ModemPort.ReadTimeout = 50000;
      this.ModemPort.WriteTimeout = 50000;
      this.ModemPort.Close();
      this.ModemPort.Open();
    }
  }
}
