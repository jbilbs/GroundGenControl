// Decompiled with JetBrains decompiler
// Type: KDAS.Data_Strings
// Assembly: KDAS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D88A5B4E-9475-405E-AE8F-191C9E1B6D98
// Assembly location: C:\Users\smbrause\AppData\Local\Apps\2.0\4CYGMBHA.OE2\WYOA0OGM.27R\kdas..tion_55564dd1c036797b_0001.0000_6479263921fe5664\KDAS.exe

using KDAS.Properties;
using System;

namespace KDAS
{
  internal class Data_Strings
  {
    private DateTime utcNow;
    private string Full_Tail_Number;
    private string Tail_Number;
    private string BIP_S;
    private string EJ_S;
    private string RB_S;
    private string LB_S;
    private string ICE_S;
    private string BIP_C;
    private string EJ_C;
    private string NAV_W;
    private string _currentCSV;
    private string _currentWMI;
    private string currentHour;
    private string currentMinute;
    private string currentSecond;
    private string currentTime;

    public Data_Strings()
    {
      this.Full_Tail_Number = Settings.Default.TailNumber;
      this.Tail_Number = this.Full_Tail_Number.Substring(this.Full_Tail_Number.Length - 4);
      this.BIP_S = "0";
      this.EJ_S = "0";
      this.RB_S = "0";
      this.LB_S = "0";
      this.ICE_S = "0";
      this.BIP_C = "0000";
      this.EJ_C = "0000";
      this.NAV_W = "999";
      this._currentCSV = "";
      this._currentWMI = "";
      this.currentHour = "00";
      this.currentMinute = "00";
      this.currentSecond = "00";
      this.currentTime = "00:00:00";
      this.utcNow = DateTime.UtcNow;
    }

    public void make(Mission_Snapshot INFO)
    {
      this.BIP_S = !INFO.S1 ? "0" : "1";
      this.EJ_S = !INFO.S2 ? "0" : "1";
      this.RB_S = !INFO.S4 ? "0" : "1";
      this.LB_S = !INFO.S3 ? "0" : "1";
      this.ICE_S = !INFO.S5 ? "0" : "1";
      this.NAV_W = INFO.fix == "1" || INFO.fix == "2" || (INFO.fix == "3" || INFO.fix == "4") || (INFO.fix == "5" || INFO.fix == "6") ? "0000" : "1024";
      if (INFO.E2 < 10)
        this.EJ_C = "000" + INFO.E2.ToString();
      else if (INFO.E2 >= 10 && INFO.E2 < 100)
        this.EJ_C = "00" + INFO.E2.ToString();
      else if (INFO.E2 >= 100 && INFO.E2 < 1000)
        this.EJ_C = "0" + INFO.E2.ToString();
      else if (INFO.E2 > 1000)
        this.EJ_C = INFO.E2.ToString();
      if (INFO.E1 < 10)
        this.BIP_C = "000" + INFO.E1.ToString();
      else if (INFO.E1 >= 10 && INFO.E1 < 100)
        this.BIP_C = "00" + INFO.E1.ToString();
      else if (INFO.E1 >= 100 && INFO.E1 < 1000)
        this.BIP_C = "0" + INFO.E1.ToString();
      else if (INFO.E1 > 1000)
        this.BIP_C = INFO.E1.ToString();
      this.Full_Tail_Number = Settings.Default.TailNumber;
      this.Tail_Number = this.Full_Tail_Number.Substring(this.Full_Tail_Number.Length - 4);
      int length = this.Tail_Number.Length;
      if (length == 0)
        this.Tail_Number = "####";
      else if (length == 1)
        this.Tail_Number += "###";
      else if (length == 2)
        this.Tail_Number += "##";
      else if (length == 3)
        this.Tail_Number += "#";
      else if (length > 4)
        this.Tail_Number = this.Tail_Number.Substring(0, 4);
      this.utcNow = DateTime.UtcNow;
      this.currentHour = Convert.ToString(this.utcNow.Hour);
      this.currentMinute = Convert.ToString(this.utcNow.Minute);
      this.currentSecond = Convert.ToString(this.utcNow.Second);
      if (this.currentHour.Length == 1)
        this.currentHour = "0" + this.currentHour;
      if (this.currentMinute.Length == 1)
        this.currentMinute = "0" + this.currentMinute;
      if (this.currentSecond.Length == 1)
        this.currentSecond = "0" + this.currentSecond;
      this.currentTime = this.currentHour + ":" + this.currentMinute + ":" + this.currentSecond;
      this._currentCSV = this.Tail_Number + "," + INFO.date + "," + this.currentTime + "," + INFO.fix + "," + Convert.ToString(Convert.ToDouble(INFO.lat) / 10000.0) + "," + Convert.ToString(Convert.ToDouble(INFO.lon) / 10000.0) + "," + INFO.mag + "," + INFO.alt + "," + INFO.rawAltFeet + "," + INFO.speed + "," + INFO.rawSpeed + "," + this.BIP_C + "," + this.BIP_S + "," + this.EJ_C + "," + this.EJ_S + "," + this.LB_S + "," + this.RB_S + "," + this.ICE_S + "," + INFO.lwF + "," + INFO.tempF;
      this._currentWMI = this.Tail_Number + INFO.lat + INFO.lon + INFO.speed + "0000" + INFO.alt + INFO.tempA + INFO.lwA + "+0000000" + this.BIP_S + this.BIP_C + this.EJ_S + this.EJ_C + this.LB_S + this.RB_S + this.ICE_S + this.currentTime + INFO.mag + INFO.posEr + this.NAV_W + "\r\n";
    }

    public string currentCSV
    {
      get
      {
        return this._currentCSV;
      }
      set
      {
        this._currentCSV = value;
      }
    }

    public string currentWMI
    {
      get
      {
        return this._currentWMI;
      }
      set
      {
        this._currentWMI = value;
      }
    }
  }
}
