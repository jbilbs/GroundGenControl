// Decompiled with JetBrains decompiler
// Type: KDAS.MainWindow
// Assembly: KDAS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D88A5B4E-9475-405E-AE8F-191C9E1B6D98
// Assembly location: C:\Users\smbrause\AppData\Local\Apps\2.0\4CYGMBHA.OE2\WYOA0OGM.27R\kdas..tion_55564dd1c036797b_0001.0000_6479263921fe5664\KDAS.exe

using KDAS.Properties;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;

namespace KDAS
{
  [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
  public partial class MainWindow : Window, IComponentConnector
  {
    private static string vKDAS = "v1.13";
    private Mission_Snapshot missionSnapshot;
    private Input_Output inputOutput;
    private GPS_Parse gpsParse;
    private String_Tx stringTx;
    private File_Save fileSave;
    private Data_Strings dataStrings;
    private LCD lcd;
    private ThreadStart job1;
    private ThreadStart job2;
    private ThreadStart job3;
    private ThreadStart job4;
    private ThreadStart job5;
    private Thread thread1;
    private Thread thread2;
    private Thread thread3;
    private Thread thread4;
    private Thread thread5;
    private bool errorGPS;
    private bool errorMODEM;
    private bool errorInput;
    private bool errorLCD;
    private bool readyLED;
    private bool gpsLED;
    private bool writeLED;
    private bool activityLED;
    private bool shutdownEvent;
    private bool lwZeroEvent;
    

    [DllImport("user32.dll")]
    public static extern int ExitWindowsEx(int uFlags, int dwReason);

    public MainWindow()
    {
      this.InitializeComponent();
      try
      {
        this.inputOutput = new Input_Output();
      }
      catch (Exception e)
      {
        int num = (int) MessageBox.Show("Error Creating Digital Input/Output, Check USB-1208ls");
        this.errorInput = true;
      }
      try
      {
        this.gpsParse = new GPS_Parse();
      }
      catch (Exception e)
      {
        int num = (int) MessageBox.Show("Error Creating GPS_Parse, Check Serial Port Settings");
        this.errorGPS = true;
      }
      try
      {
        this.dataStrings = new Data_Strings();
      }
      catch (Exception e)
      {
        int num = (int) MessageBox.Show("Error Creating Data_String");
      }
      try
      {
        this.fileSave = new File_Save();
      }
      catch (Exception e)
      {
        int num = (int) MessageBox.Show("Error Creating File_Save");
      }
      try
      {
        this.missionSnapshot = new Mission_Snapshot();
      }
      catch (Exception e)
      {
        int num = (int) MessageBox.Show("Error Creating Mission Snapshot");
      }
      try
      {
        this.stringTx = new String_Tx();
      }
      catch (Exception e)
      {
        int num = (int) MessageBox.Show("Error Creating String_Send, Check Serial Port Settings");
        this.errorMODEM = true;
      }
      try
      {
        this.lcd = new LCD();
      }
      catch (Exception e)
      {
        int num = (int) MessageBox.Show("Error Creating LCD, Check Serial Port Settings");
        this.errorLCD = true;
      }
      this.Control();
    }

    private void Control()
    {
      this.txbVersion.Text = MainWindow.vKDAS;
      this.txbTailNumber.Text = Settings.Default.TailNumber;
      if (!this.errorGPS)
      {
        this.job1 = new ThreadStart(this.Thread_GPS);
        this.thread1 = new Thread(this.job1);
        this.thread1.Start();
      }
      this.job3 = new ThreadStart(this.Thread_Save_Data);
      this.thread3 = new Thread(this.job3);
      this.thread3.Start();
      if (!this.errorMODEM)
      {
        this.job4 = new ThreadStart(this.Thread_Transmit);
        this.thread4 = new Thread(this.job4);
        this.thread4.Start();
      }
      if (!this.errorInput)
      {
        this.job2 = new ThreadStart(this.Thread_Input_Output);
        this.thread2 = new Thread(this.job2);
        this.thread2.Start();
      }
      if (!this.errorLCD)
      {
        this.job5 = new ThreadStart(this.Thread_LCD);
        this.thread5 = new Thread(this.job5);
        this.thread5.Start();
      }
      if (this.errorInput || this.errorGPS)
        return;
      this.readyLED = true;
    }

    private void Thread_GPS()
    {
      while (true)
      {
        this.gpsLED = this.gpsParse.fix != "0";
        this.missionSnapshot.lat = this.gpsParse.latitude;
        this.missionSnapshot.lon = this.gpsParse.longitude;
        this.missionSnapshot.alt = this.gpsParse.altitude;
        this.missionSnapshot.speed = this.gpsParse.groundSpeed;
        this.missionSnapshot.sat = this.gpsParse.satellites;
        this.missionSnapshot.fix = this.gpsParse.fix;
        this.missionSnapshot.mag = this.gpsParse.magneticVariation;
        this.missionSnapshot.posEr = this.gpsParse.horizontalError;
        this.missionSnapshot.time = this.gpsParse.time;
        this.missionSnapshot.date = this.gpsParse.date;
        this.missionSnapshot.rawLat = this.gpsParse.rawLatitude;
        this.missionSnapshot.rawLon = this.gpsParse.rawLongitude;
        this.missionSnapshot.rawAlt = this.gpsParse.rawAltitude;
        this.missionSnapshot.rawAltFeet = this.gpsParse.altitudeFeet;
        this.missionSnapshot.rawSpeed = this.gpsParse.rawGroundSpeed;
        this.missionSnapshot.rawSat = this.gpsParse.rawSatellites;
        this.missionSnapshot.rawFix = this.gpsParse.rawFix;
        this.missionSnapshot.rawMag = this.gpsParse.rawMagneticVariation;
        this.missionSnapshot.rawPosEr = this.gpsParse.rawHorizontalError;
        this.missionSnapshot.rawTime = this.gpsParse.rawTime;
        this.missionSnapshot.rawDate = this.gpsParse.rawDate;
        this.missionSnapshot.latF = this.gpsParse.latF;
        this.missionSnapshot.lonF = this.gpsParse.lonF;
        this.missionSnapshot.spdF = this.gpsParse.spdF;
        this.Dispatcher.BeginInvoke((Action) (() =>
        {
          this.txbLatitude.Text = this.gpsParse.latitude;
          this.txbLongitude.Text = this.gpsParse.longitude;
          this.txbGroundSpeed.Text = this.gpsParse.groundSpeed;
          this.txbMagneticVariation.Text = this.gpsParse.magneticVariation;
          this.txbTime.Text = this.gpsParse.time;
          this.txbAltitude.Text = this.gpsParse.altitude;
          this.txbPositionError.Text = this.gpsParse.horizontalError;
          this.txbRawLatitude.Text = this.gpsParse.rawLatitude;
          this.txbRawLongitude.Text = this.gpsParse.rawLongitude;
          this.txbRawGroundSpeed.Text = this.gpsParse.rawGroundSpeed;
          this.txbRawMagneticVariation.Text = this.gpsParse.rawMagneticVariation;
          this.txbRawSatillites.Text = this.gpsParse.rawSatellites;
          this.txbRawDate.Text = this.gpsParse.rawDate;
          this.txbRawTime.Text = this.gpsParse.rawTime;
          this.txbRawFix.Text = this.gpsParse.rawFix;
          this.txbRawAltitude.Text = this.gpsParse.rawAltitude;
          this.txbRawPositionError.Text = this.gpsParse.rawHorizontalError;
          if (this.gpsParse.timeSyced)
          {
            this.txbSynced.Text = "Yes";
            this.txbSynced.Background = (Brush) Brushes.DarkGreen;
            this.txbSynced.Foreground = (Brush) Brushes.White;
          }
          if (this.gpsParse.fix == "1" || this.gpsParse.fix == "2" || this.gpsParse.fix == "3")
            this.txbWarning.Text = "0000";
          else
            this.txbWarning.Text = "1024";
        }));
        Thread.Sleep(950);
      }
    }

    private void Thread_Input_Output()
    {
      while (!this.errorInput)
      {
        this.inputOutput.ScanPorts();
        this.activityLED = this.inputOutput.setActivity();
        this.inputOutput.setLEDs(this.readyLED, this.gpsLED, this.writeLED, this.activityLED);
        this.missionSnapshot.E1 = this.inputOutput.Event1;
        this.missionSnapshot.E2 = this.inputOutput.Event2;
        this.missionSnapshot.E3 = this.inputOutput.Event3;
        this.missionSnapshot.E4 = this.inputOutput.Event4;
        this.missionSnapshot.E5 = this.inputOutput.Event5;
        this.missionSnapshot.E6 = this.inputOutput.Event6;
        this.missionSnapshot.E7 = this.inputOutput.Event7;
        this.missionSnapshot.E8 = this.inputOutput.Event8;
        this.missionSnapshot.S1 = this.inputOutput.State1;
        this.missionSnapshot.S2 = this.inputOutput.State2;
        this.missionSnapshot.S3 = this.inputOutput.State3;
        this.missionSnapshot.S4 = this.inputOutput.State4;
        this.missionSnapshot.S5 = this.inputOutput.State5;
        this.missionSnapshot.S6 = this.inputOutput.State6;
        this.missionSnapshot.S7 = this.inputOutput.State7;
        this.missionSnapshot.S8 = this.inputOutput.State8;
        this.missionSnapshot.A1R = this.inputOutput.AnalogRaw1;
        this.missionSnapshot.A1V = this.inputOutput.AnalogVolts1;
        this.missionSnapshot.A1U = this.inputOutput.AnalogUnits1;
        this.missionSnapshot.A2R = this.inputOutput.AnalogRaw2;
        this.missionSnapshot.A3R = this.inputOutput.AnalogRaw3;
        this.missionSnapshot.A2V = this.inputOutput.AnalogVolts2;
        this.missionSnapshot.A2U = this.inputOutput.AnalogUnits2;
        this.missionSnapshot.tempF = this.inputOutput.tempF;
        this.missionSnapshot.lwF = this.inputOutput.lwF;
        this.missionSnapshot.tempA = this.inputOutput.tempA;
        this.missionSnapshot.lwA = this.inputOutput.lwA;
        this.missionSnapshot.shutdownEvent = this.shutdownEvent;
        this.Dispatcher.BeginInvoke((Action) (() =>
        {
          this.txbEvent1.Text = this.inputOutput.Event1.ToString();
          this.txbEvent2.Text = this.inputOutput.Event2.ToString();
          this.txbEvent3.Text = this.inputOutput.Event3.ToString();
          this.txbEvent4.Text = this.inputOutput.Event4.ToString();
          this.txbEvent5.Text = this.inputOutput.Event5.ToString();
          this.txbEvent6.Text = this.inputOutput.Event6.ToString();
          this.txbEvent7.Text = this.inputOutput.Event7.ToString();
          this.txbEvent8.Text = this.inputOutput.Event8.ToString();
          this.txbState1.Text = this.inputOutput.State1.ToString();
          this.txbState2.Text = this.inputOutput.State2.ToString();
          this.txbState3.Text = this.inputOutput.State3.ToString();
          this.txbState4.Text = this.inputOutput.State4.ToString();
          this.txbState5.Text = this.inputOutput.State5.ToString();
          this.txbState6.Text = this.inputOutput.State6.ToString();
          this.txbState7.Text = this.inputOutput.State7.ToString();
          this.txbState8.Text = this.inputOutput.State8.ToString();
          this.txbTempRaw.Text = string.Format("{0:0}", (object) this.inputOutput.AnalogRaw1);
          this.txbTempUnits.Text = string.Format("{0:0.00}", (object) this.inputOutput.AnalogUnits1);
          this.txbTempVolts.Text = string.Format("{0:0.00}", (object) this.inputOutput.AnalogVolts1);
          this.txbLWraw.Text = string.Format("{0:0}", (object) this.inputOutput.AnalogRaw2);
          this.txbLWvolts.Text = string.Format("{0:0.00}", (object) (this.inputOutput.AnalogVolts2 * 2.0));
          this.txbLWunits.Text = string.Format("{0:0.00}", (object) this.inputOutput.AnalogUnits2);
          this.txbLWtrim.Text = string.Format("{0:0.00}", (object) (this.inputOutput.AnalogVolts2 * 2.0 - this.inputOutput.TF.trim * 2.0));
          this.txbALlw.Text = this.inputOutput.lwA;
          this.txbALtemp.Text = this.inputOutput.tempA;
          if (this.inputOutput.State1)
          {
            this.txbState1.Background = (Brush) Brushes.DarkGreen;
            this.txbState1.Foreground = (Brush) Brushes.White;
          }
          else
          {
            this.txbState1.Background = (Brush) Brushes.White;
            this.txbState1.Foreground = (Brush) Brushes.Black;
          }
          if (this.inputOutput.State2)
          {
            this.txbState2.Background = (Brush) Brushes.DarkGreen;
            this.txbState2.Foreground = (Brush) Brushes.White;
          }
          else
          {
            this.txbState2.Background = (Brush) Brushes.White;
            this.txbState2.Foreground = (Brush) Brushes.Black;
          }
          if (this.inputOutput.State3)
          {
            this.txbState3.Background = (Brush) Brushes.DarkGreen;
            this.txbState3.Foreground = (Brush) Brushes.White;
          }
          else
          {
            this.txbState3.Background = (Brush) Brushes.White;
            this.txbState3.Foreground = (Brush) Brushes.Black;
          }
          if (this.inputOutput.State4)
          {
            this.txbState4.Background = (Brush) Brushes.DarkGreen;
            this.txbState4.Foreground = (Brush) Brushes.White;
          }
          else
          {
            this.txbState4.Background = (Brush) Brushes.White;
            this.txbState4.Foreground = (Brush) Brushes.Black;
          }
          if (this.inputOutput.State5)
          {
            this.txbState5.Background = (Brush) Brushes.DarkGreen;
            this.txbState5.Foreground = (Brush) Brushes.White;
          }
          else
          {
            this.txbState5.Background = (Brush) Brushes.White;
            this.txbState5.Foreground = (Brush) Brushes.Black;
          }
          if (this.inputOutput.State6)
          {
            this.txbState6.Background = (Brush) Brushes.DarkGreen;
            this.txbState6.Foreground = (Brush) Brushes.White;
          }
          else
          {
            this.txbState6.Background = (Brush) Brushes.White;
            this.txbState6.Foreground = (Brush) Brushes.Black;
          }
          if (this.inputOutput.State7)
            this.inputOutput.zeroLW();
          if (!this.inputOutput.State8 || this.missionSnapshot.totalSeconds <= 15)
            return;
          this.shutdown();
        }));
        Thread.Sleep(50);
      }
    }

    private void Thread_Transmit()
    {
      while (true)
      {
        this.stringTx.sendString(this.missionSnapshot.currentWMIstring);
        this.Dispatcher.BeginInvoke((Action) (() =>
        {
          this.txbTXdelay.Background = (Brush) Brushes.DarkGreen;
          this.txbTXdelay.Foreground = (Brush) Brushes.White;
        }));
        Thread.Sleep(200);
        this.Dispatcher.BeginInvoke((Action) (() =>
        {
          this.txbTXdelay.Background = (Brush) Brushes.White;
          this.txbTXdelay.Foreground = (Brush) Brushes.Black;
        }));
        Thread.Sleep((int) Convert.ToInt16(Settings.Default.TXtime) * 1000 - 200);
      }
    }

    private void Thread_LCD()
    {
      while (!this.errorLCD)
      {
        this.lcd.update(this.missionSnapshot);
        this.Dispatcher.BeginInvoke((Action) (() => this.txbLCDraw.Text = this.missionSnapshot.A3R.ToString()));
        Thread.Sleep(400);
      }
    }

    private void Thread_Save_Data()
    {
      while (true)
      {
        this.missionSnapshot.tailNumber = Settings.Default.TailNumber;
        this.missionSnapshot.ready = this.readyLED;
        this.missionSnapshot.gps = this.gpsLED;
        this.missionSnapshot.write = this.writeLED;
        this.missionSnapshot.activity = this.activityLED;
        ++this.missionSnapshot.totalSeconds;
        if (this.missionSnapshot.S3)
          ++this.missionSnapshot.lbSeconds;
        if (this.missionSnapshot.S4)
          ++this.missionSnapshot.rbSeconds;
        if (this.missionSnapshot.S5)
          ++this.missionSnapshot.iceSeconds;
        this.dataStrings.make(this.missionSnapshot);
        this.missionSnapshot.currentWMIstring = this.dataStrings.currentWMI;
        this.missionSnapshot.currentCSVstring = this.dataStrings.currentCSV;
        this.fileSave.saveCSV(this.missionSnapshot);
        this.fileSave.saveWMI(this.missionSnapshot);
        this.Dispatcher.BeginInvoke((Action) (() =>
        {
          this.txbString.Text = this.missionSnapshot.currentWMIstring.Trim();
          this.txbLength.Text = this.missionSnapshot.currentWMIstring.Length.ToString();
          this.txbTXdelay.Text = Convert.ToString(Settings.Default.TXtime);
        }));
        this.inputOutput.resetAll();
        this.writeLED = true;
        this.inputOutput.setLEDs(this.readyLED, this.gpsLED, this.writeLED, this.activityLED);
        Thread.Sleep(200);
        this.writeLED = false;
        this.inputOutput.setLEDs(this.readyLED, this.gpsLED, this.writeLED, this.activityLED);
        Thread.Sleep(770);
      }
    }

    private void closeAllCommPorts()
    {
      try
      {
        this.gpsParse.closeGPSport();
      }
      catch (Exception e)
      {
      }
      try
      {
        this.lcd.closeLCDport();
      }
      catch (Exception e)
      {
      }
      try
      {
        this.stringTx.closeTXport();
      }
      catch (Exception e)
      {
      }
    }

    private new void Exit()
    {
      try
      {
        this.lcd.clearscreen();
      }
      catch (Exception e)
      {
      }
      try
      {
        this.inputOutput.ledsOff();
      }
      catch (Exception e)
      {
      }
      if (!this.errorGPS)
        this.thread1.Abort();
      if (!this.errorInput)
        this.thread2.Abort();
      this.thread3.Abort();
      if (!this.errorMODEM)
        this.thread4.Abort();
      this.thread4.Abort();
      if (!this.errorLCD)
        this.thread5.Abort();
      Application.Current.Shutdown();
    }

    private void shutdown()
    {
      try
      {
        this.lcd.clearscreen();
      }
      catch (Exception e)
      {
      }
      try
      {
        this.inputOutput.ledsOff();
      }
      catch (Exception e)
      {
      }
      this.shutdownEvent = true;
      if (!this.errorGPS)
        this.thread1.Abort();
      if (!this.errorInput)
        this.thread2.Abort();
      this.thread3.Abort();
      if (!this.errorMODEM)
        this.thread4.Abort();
      if (!this.errorLCD)
        this.thread5.Abort();
      Application.Current.Shutdown();
      Thread.Sleep(2000);
      Process.Start(new ProcessStartInfo("shutdown.exe", "-s -f -t 00"));
    }

    private void Shutdown(object sender, RoutedEventArgs e)
    {
      this.shutdown();
    }

    private void Exit(object sender, RoutedEventArgs e)
    {
      this.Exit();
    }

    private void Set_Configuration(object sender, RoutedEventArgs e)
    {
      Configuration configuration = new Configuration();
      configuration.updateTfReceived += new EventHandler<updateTfReceivedArg>(this.inputOutput.TF.updateRaw2VoltTF);
      configuration.Show();
    }

    private void Window_Closed(object sender, EventArgs e)
    {
      this.Exit();
    }

    [DebuggerNonUserCode]
    [EditorBrowsable(EditorBrowsableState.Never)]
    void Connect(int connectionId, object target)
    {
      switch (connectionId)
      {
        case 1:
          ((Window) target).Closed += new EventHandler(this.Window_Closed);
          break;
        case 2:
          this.label9 = (Label) target;
          break;
        case 3:
          this.label10 = (Label) target;
          break;
        case 4:
          this.label13 = (Label) target;
          break;
        case 5:
          this.label14 = (Label) target;
          break;
        case 6:
          this.label15 = (Label) target;
          break;
        case 7:
          this.label16 = (Label) target;
          break;
        case 8:
          this.label17 = (Label) target;
          break;
        case 9:
          this.label18 = (Label) target;
          break;
        case 10:
          this.label19 = (Label) target;
          break;
        case 11:
          this.label21 = (Label) target;
          break;
        case 12:
          this.txbLatitude = (TextBox) target;
          break;
        case 13:
          this.txbLongitude = (TextBox) target;
          break;
        case 14:
          this.txbGroundSpeed = (TextBox) target;
          break;
        case 15:
          this.txbMagneticVariation = (TextBox) target;
          break;
        case 16:
          this.txbWarning = (TextBox) target;
          break;
        case 17:
          this.txbTailNumber = (TextBox) target;
          break;
        case 18:
          this.txbAltitude = (TextBox) target;
          break;
        case 19:
          this.txbTime = (TextBox) target;
          break;
        case 20:
          this.txbString = (TextBox) target;
          break;
        case 21:
          this.txbLength = (TextBox) target;
          break;
        case 22:
          this.txbEvent1 = (TextBox) target;
          break;
        case 23:
          this.txbEvent2 = (TextBox) target;
          break;
        case 24:
          this.txbEvent3 = (TextBox) target;
          break;
        case 25:
          this.txbEvent4 = (TextBox) target;
          break;
        case 26:
          this.txbEvent5 = (TextBox) target;
          break;
        case 27:
          this.txbEvent6 = (TextBox) target;
          break;
        case 28:
          this.txbEvent7 = (TextBox) target;
          break;
        case 29:
          this.txbEvent8 = (TextBox) target;
          break;
        case 30:
          this.txbState1 = (TextBox) target;
          break;
        case 31:
          this.txbState2 = (TextBox) target;
          break;
        case 32:
          this.txbState3 = (TextBox) target;
          break;
        case 33:
          this.txbState4 = (TextBox) target;
          break;
        case 34:
          this.txbState5 = (TextBox) target;
          break;
        case 35:
          this.txbState6 = (TextBox) target;
          break;
        case 36:
          this.txbState7 = (TextBox) target;
          break;
        case 37:
          this.txbState8 = (TextBox) target;
          break;
        case 38:
          this.label1 = (Label) target;
          break;
        case 39:
          this.label2 = (Label) target;
          break;
        case 40:
          this.label3 = (Label) target;
          break;
        case 41:
          this.label4 = (Label) target;
          break;
        case 42:
          this.label5 = (Label) target;
          break;
        case 43:
          this.label6 = (Label) target;
          break;
        case 44:
          this.label7 = (Label) target;
          break;
        case 45:
          this.label8 = (Label) target;
          break;
        case 46:
          this.label11 = (Label) target;
          break;
        case 47:
          this.label12 = (Label) target;
          break;
        case 48:
          this.label22 = (Label) target;
          break;
        case 49:
          this.label23 = (Label) target;
          break;
        case 50:
          this.label24 = (Label) target;
          break;
        case 51:
          this.label25 = (Label) target;
          break;
        case 52:
          this.label26 = (Label) target;
          break;
        case 53:
          this.label27 = (Label) target;
          break;
        case 54:
          this.label28 = (Label) target;
          break;
        case 55:
          this.label29 = (Label) target;
          break;
        case 56:
          this.label30 = (Label) target;
          break;
        case 57:
          this.label31 = (Label) target;
          break;
        case 58:
          this.txbRawLatitude = (TextBox) target;
          break;
        case 59:
          this.txbRawLongitude = (TextBox) target;
          break;
        case 60:
          this.txbRawGroundSpeed = (TextBox) target;
          break;
        case 61:
          this.txbRawMagneticVariation = (TextBox) target;
          break;
        case 62:
          this.txbRawFix = (TextBox) target;
          break;
        case 63:
          this.txbRawSatillites = (TextBox) target;
          break;
        case 64:
          this.txbRawAltitude = (TextBox) target;
          break;
        case 65:
          this.txbRawDate = (TextBox) target;
          break;
        case 66:
          this.txbRawTime = (TextBox) target;
          break;
        case 67:
          this.label32 = (Label) target;
          break;
        case 68:
          this.label34 = (Label) target;
          break;
        case 69:
          this.txbPositionError = (TextBox) target;
          break;
        case 70:
          this.label35 = (Label) target;
          break;
        case 71:
          this.txbRawPositionError = (TextBox) target;
          break;
        case 72:
          this.label33 = (Label) target;
          break;
        case 73:
          this.label36 = (Label) target;
          break;
        case 74:
          this.label37 = (Label) target;
          break;
        case 75:
          this.label38 = (Label) target;
          break;
        case 76:
          this.label39 = (Label) target;
          break;
        case 77:
          this.label40 = (Label) target;
          break;
        case 78:
          this.label41 = (Label) target;
          break;
        case 79:
          this.label42 = (Label) target;
          break;
        case 80:
          this.menu = (Menu) target;
          break;
        case 81:
          ((MenuItem) target).Click += new RoutedEventHandler(this.Set_Configuration);
          break;
        case 82:
          ((MenuItem) target).Click += new RoutedEventHandler(this.Shutdown);
          break;
        case 83:
          ((MenuItem) target).Click += new RoutedEventHandler(this.Exit);
          break;
        case 84:
          this.label43 = (Label) target;
          break;
        case 85:
          this.txbVersion = (TextBox) target;
          break;
        case 86:
          this.txbSynced = (TextBox) target;
          break;
        case 87:
          this.label45 = (Label) target;
          break;
        case 88:
          this.image1 = (Image) target;
          break;
        case 89:
          this.label44 = (Label) target;
          break;
        case 90:
          this.txbTXdelay = (TextBox) target;
          break;
        case 91:
          this.label46 = (Label) target;
          break;
        case 92:
          this.txbTempRaw = (TextBox) target;
          break;
        case 93:
          this.txbLWraw = (TextBox) target;
          break;
        case 94:
          this.label47 = (Label) target;
          break;
        case 95:
          this.label48 = (Label) target;
          break;
        case 96:
          this.txbTempVolts = (TextBox) target;
          break;
        case 97:
          this.txbLWvolts = (TextBox) target;
          break;
        case 98:
          this.txbTempUnits = (TextBox) target;
          break;
        case 99:
          this.label49 = (Label) target;
          break;
        case 100:
          this.label50 = (Label) target;
          break;
        case 101:
          this.label51 = (Label) target;
          break;
        case 102:
          this.txbLWunits = (TextBox) target;
          break;
        case 103:
          this.txbLWtrim = (TextBox) target;
          break;
        case 104:
          this.label55 = (Label) target;
          break;
        case 105:
          this.label56 = (Label) target;
          break;
        case 106:
          this.label57 = (Label) target;
          break;
        case 107:
          this.txbALtemp = (TextBox) target;
          break;
        case 108:
          this.txbALlw = (TextBox) target;
          break;
        case 109:
          this.label52 = (Label) target;
          break;
        case 110:
          this.label58 = (Label) target;
          break;
        case 111:
          this.label59 = (Label) target;
          break;
        case 112:
          this.label60 = (Label) target;
          break;
        case 113:
          this.label62 = (Label) target;
          break;
        case 114:
          this.txbLCDraw = (TextBox) target;
          break;
        case 115:
          this.label20 = (Label) target;
          break;
        default:
          this._contentLoaded = true;
          break;
      }
    }
  }
}
