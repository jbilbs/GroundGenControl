// Decompiled with JetBrains decompiler
// Type: KDAS.LCD
// Assembly: KDAS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D88A5B4E-9475-405E-AE8F-191C9E1B6D98
// Assembly location: C:\Users\smbrause\AppData\Local\Apps\2.0\4CYGMBHA.OE2\WYOA0OGM.27R\kdas..tion_55564dd1c036797b_0001.0000_6479263921fe5664\KDAS.exe

using KDAS.Properties;
using System;
using System.IO.Ports;

namespace KDAS
{
  internal class LCD
  {
    private SerialPort LCDport;
    private byte[] specialCommands;
    private int index;

    public void closeLCDport()
    {
      this.LCDport.Close();
    }

    public LCD()
    {
      this.LCDport = new SerialPort();
      this.specialCommands = new byte[5]
      {
        (byte) 254,
        (byte) 128,
        (byte) 192,
        (byte) 10,
        (byte) 1
      };
      this.index = 0;
      this.LCDport_Init();
    }

    public void LCDport_Init()
    {
      this.LCDport.PortName = Settings.Default.LCDport;
      this.LCDport.BaudRate = 9600;
      this.LCDport.Parity = Parity.None;
      this.LCDport.StopBits = StopBits.One;
      this.LCDport.DataBits = 8;
      this.LCDport.Handshake = Handshake.None;
      this.LCDport.ReadTimeout = 50000;
      this.LCDport.WriteTimeout = 50000;
      this.LCDport.Close();
      this.LCDport.Open();
    }

    public void homePosition(int line)
    {
      if (line == 1)
      {
        this.LCDport.Write(this.specialCommands, 0, 1);
        this.LCDport.Write(this.specialCommands, 1, 1);
      }
      if (line != 2)
        return;
      this.LCDport.Write(this.specialCommands, 0, 1);
      this.LCDport.Write(this.specialCommands, 2, 1);
    }

    public void clear()
    {
      this.LCDport.Write(this.specialCommands, 0, 1);
      this.LCDport.Write(this.specialCommands, 4, 1);
    }

    public void update(Mission_Snapshot snapshot)
    {
      int int32 = Convert.ToInt32(snapshot.A3R);
      if (int32 > 2500 && int32 < 3500)
      {
        this.clear();
        if (this.index == 5)
          this.index = 0;
        else
          ++this.index;
      }
      else if (int32 > 3500)
      {
        this.clear();
        if (this.index == 0)
          this.index = 5;
        else
          --this.index;
      }
      try
      {
        this.homePosition(1);
        switch (this.index)
        {
          case 0:
            string tailNumber = Settings.Default.TailNumber;
            string vKdas = Settings.Default.vKDAS;
            this.LCDport.Write("TAIL: " + tailNumber);
            this.homePosition(2);
            this.LCDport.Write("FIRM: v" + vKdas);
            break;
          case 1:
            string str1 = snapshot.E1.ToString();
            while (str1.Length < 3)
              str1 += " ";
            string str2 = snapshot.E2.ToString();
            while (str2.Length < 3)
              str2 += " ";
            string str3 = !snapshot.S3 ? "0" : "1";
            string str4 = !snapshot.S4 ? "0" : "1";
            this.LCDport.Write("BIP: " + str1 + "  RB: " + str4);
            this.homePosition(2);
            this.LCDport.Write(" EJ: " + str2 + "  LB: " + str3);
            break;
          case 2:
            string lwF = snapshot.lwF;
            string tempF = snapshot.tempF;
            this.LCDport.Write("LW: " + lwF + " g/m^3");
            this.homePosition(2);
            this.LCDport.Write("TP: " + tempF + " deg C");
            break;
          case 3:
            string latF = snapshot.latF;
            string lonF = snapshot.lonF;
            this.LCDport.Write("LAT: " + latF);
            this.homePosition(2);
            this.LCDport.Write("LON: " + lonF);
            break;
          case 4:
            string spdF = snapshot.spdF;
            string rawAltFeet = snapshot.rawAltFeet;
            this.LCDport.Write("SPD: " + spdF + " knots");
            this.homePosition(2);
            this.LCDport.Write("ALT: " + rawAltFeet + " ft");
            break;
          case 5:
            string rawFix = snapshot.rawFix;
            string rawSat = snapshot.rawSat;
            string rawPosEr = snapshot.rawPosEr;
            this.LCDport.Write("FIX: " + rawFix + "   SAT: " + rawSat);
            this.homePosition(2);
            this.LCDport.Write("ERROR: " + rawPosEr);
            break;
        }
      }
      catch
      {
        this.tryToFixLCD();
      }
    }

    public void setSplashScreen()
    {
      this.homePosition(1);
      string str1 = "WMI-KDAS ";
      string str2 = "Tail ";
      string vKdas = Settings.Default.vKDAS;
      string tailNumber = Settings.Default.TailNumber;
      this.LCDport.Write(str1 + vKdas);
      this.homePosition(2);
      this.LCDport.Write(str2 + tailNumber);
      this.LCDport.Write(this.specialCommands, 0, 1);
      this.LCDport.Write(this.specialCommands, 3, 1);
    }

    public void clearscreen()
    {
      this.LCDport.Write(this.specialCommands, 0, 1);
      this.LCDport.Write(this.specialCommands, 4, 1);
    }

    public void tryToFixLCD()
    {
    }
  }
}
