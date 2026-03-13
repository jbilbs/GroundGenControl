// Decompiled with JetBrains decompiler
// Type: KDAS.Transfer_Functions
// Assembly: KDAS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D88A5B4E-9475-405E-AE8F-191C9E1B6D98
// Assembly location: C:\Users\smbrause\AppData\Local\Apps\2.0\4CYGMBHA.OE2\WYOA0OGM.27R\kdas..tion_55564dd1c036797b_0001.0000_6479263921fe5664\KDAS.exe

using KDAS.Properties;
using System;

namespace KDAS
{
  internal class Transfer_Functions
  {
    private static double L_Raw = 2041.0;
    private static double H_Raw = 4095.0;
    private static double L_Volt = 0.0;
    private static double H_Volt = 5.0;
    private double Raw2Voltslope;
    private double Raw2VoltYint;
    private double _trim;

    public Transfer_Functions()
    {
      double raw2VoltCalibration = Settings.Default.raw2VoltCalibration;
      this.Raw2Voltslope = (Transfer_Functions.H_Volt + raw2VoltCalibration - Transfer_Functions.L_Volt) / (Transfer_Functions.H_Raw - Transfer_Functions.L_Raw);
      this.Raw2VoltYint = Transfer_Functions.H_Volt + raw2VoltCalibration - this.Raw2Voltslope * Transfer_Functions.H_Raw;
    }

    public void updateRaw2VoltTF(object sender, updateTfReceivedArg e)
    {
      double raw2VoltCalibration = Settings.Default.raw2VoltCalibration;
      this.Raw2Voltslope = (Transfer_Functions.H_Volt + raw2VoltCalibration - Transfer_Functions.L_Volt) / (Transfer_Functions.H_Raw - Transfer_Functions.L_Raw);
      this.Raw2VoltYint = Transfer_Functions.H_Volt + raw2VoltCalibration - this.Raw2Voltslope * Transfer_Functions.H_Raw;
    }

    public double raw2volt(double raw)
    {
      return raw * this.Raw2Voltslope + this.Raw2VoltYint;
    }

    public double volt2tempature(double volt)
    {
      double num1 = Convert.ToDouble(Settings.Default.MaxT);
      double num2 = Convert.ToDouble(Settings.Default.MinT);
      double num3 = Convert.ToDouble(Settings.Default.MaxV);
      double num4 = Convert.ToDouble(Settings.Default.MinV);
      double num5 = (num1 - num2) / (num3 - num4);
      double num6 = num1 - num5 * num3;
      return num5 * volt + num6;
    }

    public void zerodata(double Zerovolt)
    {
      this._trim = Zerovolt;
    }

    public double volt2liquidWater2(double volt)
    {
      return 224.942 * (volt - this._trim) / 82.31;
    }

    public double trim
    {
      get
      {
        return this._trim;
      }
      set
      {
        this._trim = value;
      }
    }
  }
}
