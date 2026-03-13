	using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;
using System.Data;
using System.Text;
using System.Net;
using System.Net.Sockets;
using GroundGenControl.Logging;

using GroundGenControl.Configuration;
using GroundGenControl.Communications;
using GroundGenControl.Messages;
using System.Linq;
using System.Collections.Generic;
using System.Threading;

namespace GroundGenControl
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class frmMain : System.Windows.Forms.Form
	{

		private class StatusListIndexes
		{
			public static int PARSE_TIME		=	0;
			public static int REMOTE_TIME		=	PARSE_TIME + 1;
			public static int LAST_RESET		=	REMOTE_TIME + 1;
			public static int SEED_STATUS		=	LAST_RESET + 1;
			public static int SEED_TIME			=	SEED_STATUS + 1;
			public static int FLAME_TEMP		=	SEED_TIME + 1;
			public static int FLOW_METER		=	FLAME_TEMP + 1;
			public static int PRESSURE			=	FLOW_METER + 1;
			public static int PROPANE_RELAY		=	PRESSURE + 1;
			public static int SOLUTION_RELAY	=	PROPANE_RELAY + 1;
			public static int PURGE_RELAY		=	SOLUTION_RELAY + 1;
			public static int IGNITER_STATE		=	PURGE_RELAY + 1;
			public static int BATTERY_VOLTAGE	=	IGNITER_STATE + 1;
			public static int FIRMWARE_VERSION	=	BATTERY_VOLTAGE + 1;
	
		}


		private class LogListIndex
		{
			public static int LOG_TIME			=	1;
			public static int LOG_SITE			=	LOG_TIME + 1;
			public static int LOG_COMMAND		=	LOG_SITE + 1;
			public static int LOG_DESCRIPTION	=	LOG_COMMAND + 1;
			
		}

		private System.Windows.Forms.TabControl tabRemote;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TabPage tabPageWeather;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TabPage tabPageSystemStatus;
		private ShapeControl.ShapeControl shapeControl3;
		private ShapeControl.ShapeControl shapeControl2;
		private ShapeControl.ShapeControl shapePurgeValve;
		private System.Windows.Forms.Label lblBattery;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.PictureBox pictIgnition;
		private System.Windows.Forms.Label lblPurge;
		private System.Windows.Forms.Label lblSolution;
		private System.Windows.Forms.Label lblPropane;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label lblBurning;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.MenuStrip mainMenu1;
		private System.Windows.Forms.ToolStripMenuItem menuItem1;
		private System.Windows.Forms.ToolStripMenuItem menuItem2;
		private System.Windows.Forms.ListView lvStatus;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button btnSyncClock;
		private System.Windows.Forms.Label lblCurrentTime;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Button btnSolution;
		private System.Windows.Forms.Button btnPurge;
		private System.Windows.Forms.Button btnPropane;
		private System.Windows.Forms.Button btnIgnition;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.Button btnStartSeed;
		private System.Windows.Forms.Button btnEndSeeding;
		private System.Windows.Forms.Button btnResetSeed;
		private System.Windows.Forms.Button btnReset;
		private System.Windows.Forms.Label lblLastReset;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Button btnLogCleared;
        private System.Windows.Forms.ListView lvErrorLog;
		private System.Windows.Forms.ToolStripMenuItem menuItem5;
		private System.Windows.Forms.PictureBox pictBattery;
		private System.Windows.Forms.PictureBox pictPurge;
		private System.Windows.Forms.PictureBox pictSolution;
		private System.Windows.Forms.PictureBox pictPropane;
		private System.Windows.Forms.Label lblNozzle;
		private System.Windows.Forms.Label lblFlowStatic;
		private System.Windows.Forms.PictureBox pictureBox2;
		private System.Windows.Forms.PictureBox pictureBox3;
		private System.Windows.Forms.PictureBox pictureBox4;
		private System.Windows.Forms.PictureBox pictureBox5;
		private System.Windows.Forms.LinkLabel lnkWMI;
		private System.Windows.Forms.ToolStripMenuItem mnuRemoteSiteSetup;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.ComboBox cbConnectTo;
		private ShapeControl.ShapeControl shapeConnected;
		private RemoteConnection	_remoteConnection = new RemoteConnection();
		private RemoteCommand		_remoteCommand	= new RemoteCommand();
		private System.Windows.Forms.Label lblSend;
		private ShapeControl.ShapeControl shapeSend;
		private System.Windows.Forms.Label lblConnected;
		private System.Windows.Forms.Label lblRecv;
		private ShapeControl.ShapeControl shapeRecv;
		private System.Windows.Forms.Label lblFlowMeter;
		private System.Windows.Forms.TextBox txtBatteryVoltage;
		private ShapeControl.ShapeControl shapeSolutionValveOff;
		private ShapeControl.ShapeControl shapePropaneOn1;
		private ShapeControl.ShapeControl shapePropaneOn2;
		private ShapeControl.ShapeControl shapePropaneOff;
		private ShapeControl.ShapeControl shapeNozzleOff;
		private ShapeControl.ShapeControl shapePurgeOff;
		private ShapeControl.ShapeControl shapePurgeOn;
		private System.Windows.Forms.Label lblFlameTemp;
		private System.Windows.Forms.PictureBox pictBurning;
		private System.Windows.Forms.Label lblNotBurning;
		private ShapeControl.ShapeControl shapeBurning;
		private System.Windows.Forms.Button btnUpdate;
		private System.Windows.Forms.Button btnNozzle;
		private System.Windows.Forms.Timer timerMasterTime;
		private System.Windows.Forms.TextBox txtMasterTime;
		private System.Windows.Forms.Label lblLastResetTime;
		private ShapeControl.ShapeControl shapePropaneLine3;
		private ShapeControl.ShapeControl shapePropaneLine2;
		private ShapeControl.ShapeControl shapePropaneLine1;
		private ShapeControl.ShapeControl shapeSolutionLine4;
		private ShapeControl.ShapeControl shapeSolutionLine3;
		private ShapeControl.ShapeControl shapeSolutionLine2;
		private ShapeControl.ShapeControl shapeSolutionLine1;
		private ShapeControl.ShapeControl shapeSolutionValveOn;

		private	System.Drawing.Font		_normalBatteryVoltageFont = null;
		private	System.Drawing.Font		_lowBatteryVoltageFont    = null;

		const int DATA_COLULMN = 1;

		private	bool					_updateStatus =	 false;
		private	bool					_keepAliveInProgress = false;
        private bool                    _receiveInProgress = false;

		private StatusMessage			_statusMessage = null;
        private WeatherHistoryMessage   _weatherHistoryMessage = null;
        private bool                    _IgnoreErrorHistoryMessage = false;

		private ShapeControl.ShapeControl shapeSeeding;
		private ShapeControl.ShapeControl shapePurgeLine;

		GlobalConfiguration				_globalConfiguration = GlobalConfiguration.instance;
		private int						_responsesReceived = 0;
		private System.Windows.Forms.ProgressBar _progressReceive;
		private System.Windows.Forms.ToolStripMenuItem menuItemAbout;
		private ShapeControl.ShapeControl shapeNozzleLine1;
		private ShapeControl.ShapeControl shapeNozzleLine2;
		private ShapeControl.ShapeControl shapeNozzleValveOutline;
		private ShapeControl.ShapeControl shapeNozzleOn1;
		private System.Windows.Forms.TabPage tabLog;
		private ShapeControl.ShapeControl shapeNozzleOn2;
		private	System.DateTime			_seedStartTime		= System.DateTime.MinValue;
		private System.DateTime			_lastResetTime		= System.DateTime.MinValue;
		private System.Windows.Forms.TextBox txtCommandRemaining;
		private System.Windows.Forms.ToolStripMenuItem menuVPNSetup;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label logFileName;

		private System.DateTime			_commandEndTime		= System.DateTime.MinValue;
		private System.Windows.Forms.Button btnFolder;
        private GroundGenControl.Logging.EventLog _eventLog = new GroundGenControl.Logging.EventLog();
        private GroundGenControl.Logging.WeatherLog _weatherLog = new GroundGenControl.Logging.WeatherLog();

		private System.Windows.Forms.Button btnUpdateWx;
		private System.Windows.Forms.ToolStripMenuItem menuConfiguration;
		private System.Windows.Forms.Timer timerKeepAlive;
		private System.Windows.Forms.ToolStripMenuItem menuAlertSetup;
		private System.Windows.Forms.Label lblPressure;
		private System.Windows.Forms.Label lblPressureStatic;
        private ListView lvwWeatherLog;
        private Button btnUpdateWxHistory;
        private Button btnWeatherFolder;
        private Label lblWeatherLog;
        private Label label6;
        private System.Windows.Forms.ToolStripMenuItem mnuSkywaveAccount;
        private bool					_connected			=	false;

        //  Delegate for obtaining selected site through thread call
        public delegate String getCurrentSiteHandler();
        //  Set
        public delegate void getConnectHandler();




		public frmMain()
		{

            //  Added for move to VS 2005, all cross thread calls need to be changed at some point
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;

			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			populateListView();
			populateErrorLog();
            populateWeatherLog();
			updateErrorLog();
			populateSites();
			

			communicationControlState(false);
			cbConnectTo.Enabled		= true;

			//	Start master time timer
			timerMasterTime.Enabled = true;
			//	Start the keep alive timer
			timerKeepAlive.Enabled = true;

			//	Set not burning rectange over burning rectangle
			lblNotBurning.Text = "";
			lblNotBurning.Location = pictBurning.Location;
			lblNotBurning.Size = pictBurning.Size;

			//	set default system state
			StatusMessage statusMessage = new StatusMessage();
			updateListView(ref statusMessage);
			updateSystemsDiagram(ref statusMessage);

			//	Add WMI link
			// Create a new link using the Add method of the LinkCollection class.
			lnkWMI.Links.Add(0,lnkWMI.Text.Length,"www.weathermod.com/");
            this.Text = "WMI Remote Ground Generator Control Interface " + GlobalConfiguration.instance.BuildVersion;



        }


        private bool logUpdating;
		//	Update log
		private void updateErrorLog()
		{
            while(logUpdating)
            {
                Thread.Sleep(500);
            }
            logUpdating = true;
			lvErrorLog.Items.Clear();


			ArrayList	arEvents = _eventLog.eventList;


			foreach( Object obj in arEvents)
			{
				LogEntry entry = (LogEntry) obj;

				ListViewItem item = lvErrorLog.Items.Add(entry.logTime.ToString("MM/dd/yyyy HH:mm:ss"));
				item.SubItems.Add(entry.logSite);
				item.SubItems.Add(entry.logCommand);
				item.SubItems.Add(entry.logDescription);

			}



			//	Update filename
			logFileName.Text = _eventLog.logFileName;
            logUpdating = false;
		}

         /// <summary>
         /// Update weather log
         /// </summary>
        private void updateWeatherLog()
        {
            //  Clear out the existing log list
            lvwWeatherLog.Items.Clear();

            ArrayList arEvents = _weatherHistoryMessage.WeatherMessageArray;

            foreach (Object obj in arEvents)
            {
                WeatherMessage entry = (WeatherMessage)obj;

                ListViewItem item = lvwWeatherLog.Items.Add(entry.observationTimeString);
                item.SubItems.Add(  entry.windDirectionString     );
                item.SubItems.Add(  entry.windSpeedString         );
                item.SubItems.Add(  entry.airTempString           );
                item.SubItems.Add(  entry.airPressureString       );
                item.SubItems.Add(  entry.relativeHumidityString  );
                item.SubItems.Add(  entry.heaterStateString       );
              
            }


            //	Update filename
            lblWeatherLog.Text = _eventLog.logFileName;
        }


		//	Populate Sites
		public void populateSites()
		{
			cbConnectTo.Items.Clear();
			cbConnectTo.DisplayMember = "name";
			cbConnectTo.ValueMember   = "mobileID";

			//	Add a default site for disconnect
			//cbConnectTo.Items.Add(new Site("Disconnect","DISCONNECT",""));
/*
			foreach( Site siteInfo in _globalConfiguration.configuration.sites )
			{
	
				cbConnectTo.Items.Add( siteInfo );
			}
*/

			//cbConnectTo.SelectedIndex = 0;



			ArrayList	tempList = new ArrayList();

			foreach( Site siteInfo in _globalConfiguration.configuration.sites )
			{
				tempList.Add(siteInfo);
	
			}

			tempList.Sort();

			//	Add a default site for disconnect
			cbConnectTo.Items.Add(new Site("Disconnect","DISCONNECT", false));

			for ( int Index = 0; Index < tempList.Count; Index++ )
			{
				cbConnectTo.Items.Add(  tempList[Index] );
			}

			//	Select disconnected state
			cbConnectTo.SelectedIndex = -1;


		}


		//	Populate list
		public void populateListView()
		{

			lvStatus.Columns.Add("Parameter", -2, HorizontalAlignment.Left);
			lvStatus.Columns.Add("Value", -2, HorizontalAlignment.Left);

			ListViewItem li = new ListViewItem("System Time");
			li.SubItems.Add("");
			lvStatus.Items.Add(li);

			li = new ListViewItem("Remote Time");
			li.SubItems.Add("");
			lvStatus.Items.Add(li);

			li = new ListViewItem("Last Reset");
			li.SubItems.Add("");
			lvStatus.Items.Add(li);

			li = new ListViewItem("Seeding");
			li.SubItems.Add("");
			lvStatus.Items.Add(li);

			li = new ListViewItem("Seed Time");
			li.SubItems.Add("");
			lvStatus.Items.Add(li);

			li = new ListViewItem("Flame Temp");
			li.SubItems.Add("");
			lvStatus.Items.Add(li);

			li = new ListViewItem("Solution Flow");
			li.SubItems.Add("");
			lvStatus.Items.Add(li);

			li = new ListViewItem("Solution Pressure");
			li.SubItems.Add("");
			lvStatus.Items.Add(li);

			li = new ListViewItem("Propane Relay");
			li.SubItems.Add("");
			lvStatus.Items.Add(li);

			li = new ListViewItem("Solution Relay");
			li.SubItems.Add("");
			lvStatus.Items.Add(li);

			li = new ListViewItem("Purge Relay");
			li.SubItems.Add("");
			lvStatus.Items.Add(li);

			li = new ListViewItem("Igniter Status");
			li.SubItems.Add("");
			lvStatus.Items.Add(li);

			li = new ListViewItem("Battery Voltage");
			li.SubItems.Add("");
			lvStatus.Items.Add(li);

			li = new ListViewItem("Firmware Version");
			li.SubItems.Add("");
			lvStatus.Items.Add(li);



		}



		//	Populate list
		public void updateListView(ref StatusMessage status)
		{

			if ( !connected )
			{

				for ( int index=0; index < lvStatus.Items.Count; index ++ )
				{
					lvStatus.Items[index].SubItems[DATA_COLULMN].Text = "";
				}


				return;
			}

			lvStatus.Items[StatusListIndexes.PARSE_TIME].SubItems[DATA_COLULMN].Text			= status.parseTimeString;

			if ( status.remoteDateTime.CompareTo(System.DateTime.MinValue) == 0  )
			{
				lvStatus.Items[StatusListIndexes.REMOTE_TIME].SubItems[DATA_COLULMN].Text			= "N/A";

			}
			else
			{
				lvStatus.Items[StatusListIndexes.REMOTE_TIME].SubItems[DATA_COLULMN].Text			= status.remoteDateTime.ToString("MM/dd/yyyy HH:mm:ss");
			}


			if ( _lastResetTime.CompareTo(System.DateTime.MinValue) == 0  )
			{
				lblLastResetTime.Text = "N/A";
				lvStatus.Items[StatusListIndexes.LAST_RESET].SubItems[DATA_COLULMN].Text			= lblLastResetTime.Text;
			}
			else
			{
				lvStatus.Items[StatusListIndexes.LAST_RESET].SubItems[DATA_COLULMN].Text			= _lastResetTime.ToString("MM/dd/yyyy HH:mm:ss");
				lblLastResetTime.Text = _lastResetTime.ToString("MM/dd/yyyy HH:mm:ss");
			}


			lvStatus.Items[StatusListIndexes.SEED_STATUS].SubItems[DATA_COLULMN].Text			= status.seedingStatusString;
			//	Seed timer set in timer function
			lvStatus.Items[StatusListIndexes.FLAME_TEMP].SubItems[DATA_COLULMN].Text			= String.Format("{0:0.0} �F",status.flameTemp);
			lvStatus.Items[StatusListIndexes.FLOW_METER].SubItems[DATA_COLULMN].Text			= String.Format("{0:0.00} GPH",status.flowMeter);
			lvStatus.Items[StatusListIndexes.PRESSURE].SubItems[DATA_COLULMN].Text				= String.Format("{0:0.0} PSI",status.pressurePSI);
			lvStatus.Items[StatusListIndexes.PROPANE_RELAY].SubItems[DATA_COLULMN].Text			= status.burnerStateString;
			lvStatus.Items[StatusListIndexes.SOLUTION_RELAY].SubItems[DATA_COLULMN].Text		= status.solutionStateString;
			lvStatus.Items[StatusListIndexes.PURGE_RELAY].SubItems[DATA_COLULMN].Text			= status.purgeStateString;
			lvStatus.Items[StatusListIndexes.IGNITER_STATE].SubItems[DATA_COLULMN].Text			= status.igniterStateString;
			lvStatus.Items[StatusListIndexes.BATTERY_VOLTAGE].SubItems[DATA_COLULMN].Text		= String.Format("{0:0.0}V",status.batteryVolts);
			lvStatus.Items[StatusListIndexes.FIRMWARE_VERSION].SubItems[DATA_COLULMN].Text		= String.Format("{0:0.00}",status.firmwareVersion);

		}

		public void setPropane(bool enabled)
		{
			if ( !connected )
			{
				shapePropaneOff.Visible = false;
				shapePropaneOn1.Visible = false;
				shapePropaneOn2.Visible = false;
				shapePropaneLine2.BackColor		= StatusColors.DISCONNECTED_SYSTEM;
				shapePropaneLine2.BorderColor	= StatusColors.DISCONNECTED_SYSTEM;

				shapePropaneLine3.BackColor		= StatusColors.DISCONNECTED_SYSTEM;
				shapePropaneLine3.BorderColor	= StatusColors.DISCONNECTED_SYSTEM;
				return;
			}

			shapePropaneOff.Visible = !enabled;
			shapePropaneOn1.Visible = enabled;
			shapePropaneOn2.Visible = enabled;

			System.Drawing.Color lineColor = StatusColors.VALVE_ON;

			if ( !enabled  )
			{
				lineColor = StatusColors.VALVE_OFF;
			}

			//shapePropaneLine1.BackColor = lineColor;
			shapePropaneLine2.BackColor		= lineColor;
			shapePropaneLine2.BorderColor	= lineColor;

			shapePropaneLine3.BackColor		= lineColor;
			shapePropaneLine3.BorderColor	= lineColor;



		}

		public void setNozzle(bool enabled)
		{

			if ( !connected )
			{

				shapeNozzleOff.Visible = false;
				shapeNozzleOn1.Visible = false;
				shapeNozzleOn2.Visible = false;

				shapeNozzleLine2.BackColor		= StatusColors.DISCONNECTED_SYSTEM;
				shapeNozzleLine2.BorderColor	= StatusColors.DISCONNECTED_SYSTEM;

				return;
			}


			shapeNozzleOff.Visible = !enabled;
			shapeNozzleOn1.Visible = enabled;
			shapeNozzleOn2.Visible = enabled;

			System.Drawing.Color lineColor = StatusColors.VALVE_ON;

			if ( !enabled )
			{
				lineColor = StatusColors.VALVE_OFF;
			}

			shapeNozzleLine2.BackColor		= lineColor;
			shapeNozzleLine2.BorderColor	= lineColor;


		}

		public void setSolution(ref StatusMessage status)
		{

			if ( !connected )
			{

				shapeSolutionValveOff.Visible = false;
				shapeSolutionValveOn.Visible = false;

				shapeSolutionLine2.BackColor	= StatusColors.DISCONNECTED_SYSTEM;
				shapeSolutionLine2.BorderColor	= StatusColors.DISCONNECTED_SYSTEM;

				shapeSolutionLine3.BackColor	= StatusColors.DISCONNECTED_SYSTEM;
				shapeSolutionLine3.BorderColor	= StatusColors.DISCONNECTED_SYSTEM;

				shapeSolutionLine4.BackColor	= StatusColors.DISCONNECTED_SYSTEM;
				shapeSolutionLine4.BorderColor	= StatusColors.DISCONNECTED_SYSTEM;

				return;

			}


			shapeSolutionValveOff.Visible = !status.solutionState;
			shapeSolutionValveOn.Visible = status.solutionState;

			System.Drawing.Color lineColor = StatusColors.SOLUTION_VALVE_ON;

			if ( !status.solutionState )
			{
				lineColor = StatusColors.SOLUTION_VALVE_OFF;
			}

			//	If purge is selected, show the purge color
			if ( status.purgeState  )
			{
				lineColor = StatusColors.PURGE_VALVE_ON;
			}

			shapeSolutionLine2.BackColor	= lineColor;
			shapeSolutionLine2.BorderColor	= lineColor;

			shapeSolutionLine3.BackColor	= lineColor;
			shapeSolutionLine3.BorderColor	= lineColor;

			shapeSolutionLine4.BackColor	= lineColor;
			shapeSolutionLine4.BorderColor	= lineColor;

		}


		public void setPurge(ref StatusMessage status)
		{

			if ( !connected )
			{

				shapePurgeOn.Visible		= false;
				shapePurgeOff.Visible		= false;


				shapePurgeOn.BackColor		= StatusColors.DISCONNECTED_SYSTEM;
				shapePurgeOn.BorderColor	= StatusColors.DISCONNECTED_SYSTEM;

				return;

			}
			
			shapePurgeOn.Visible	= status.purgeState;
			shapePurgeOff.Visible	= !status.purgeState;

			System.Drawing.Color valveColor = StatusColors.PURGE_VALVE_ON;

			if ( !status.purgeState || !status.solutionState   )
			{
				valveColor = StatusColors.PURGE_VALVE_ON;
			}

			shapePurgeOn.BackColor = valveColor;
			shapePurgeOn.BorderColor = valveColor;

		}

		public void updateBatteryVoltage(ref StatusMessage status)
		{

			if ( status.batteryVolts < _globalConfiguration.configuration.AlertInfo.batteryVoltage )
			{
				txtBatteryVoltage.ForeColor = StatusColors.BATTERY_TEXT_LOW;
				txtBatteryVoltage.Font = _lowBatteryVoltageFont;			
			}
			else
			{
				txtBatteryVoltage.ForeColor = StatusColors.BATTERY_TEXT_NORMAL;
				txtBatteryVoltage.Font = _normalBatteryVoltageFont;			

			}

			txtBatteryVoltage.Text = String.Format("{0:0.0}V",status.batteryVolts);

		}


		public void updateSystemsDiagram(ref StatusMessage status)
		{

			if ( !connected )
			{

				//	Solution valve
				setSolution(ref status);
				//	Nozzle valve	
				setNozzle(status.nozzleState);
				//	Purge
				setPurge(ref status);
				//	Propane
				setPropane( status.burnerState );
				//	Battery voltage
				txtBatteryVoltage.Text = String.Format("--.-V",status.batteryVolts);

				lblFlowMeter.Text = String.Format(" GPH",status.flowMeter);
				lblPressure.Text = String.Format(" PSI",status.pressurePSI);
				lblFlameTemp.Text = String.Format(" �F",status.flameTemp);

				pictBurning.Visible = false;
				lblNotBurning.Visible = true;
				shapeBurning.BackColor = StatusColors.NOT_BURNING;
				shapeSeeding.BackColor = StatusColors.NOT_SEEDING;

				return;
			}

			//	Solution valve
			setSolution(ref status);
			//	Nozzle valve	
			setNozzle(status.nozzleState);
			//	Purge
			setPurge(ref status);
			//	Propane
			setPropane( status.burnerState );
			//	Seed lines

			//	Battery voltage
			updateBatteryVoltage(ref status );
			//	Flow
			lblFlowMeter.Text = String.Format("{0:0.00} GPH",status.flowMeter);
			//	Pressure
			lblPressure.Text = String.Format("{0:0.0} PSI",status.pressurePSI);
			//	Flame temperature
			lblFlameTemp.Text = String.Format("{0:0.0} �F",status.flameTemp);


			//	Set burning state
			if ( status.tempsensorState  )
			{
				pictBurning.Visible = true;
				lblNotBurning.Visible = false;
				shapeBurning.BackColor = StatusColors.BURNING;
			}
			else
			{
				pictBurning.Visible = false;
				lblNotBurning.Visible = true;
				shapeBurning.BackColor = StatusColors.NOT_BURNING;
			}
			//	Set seeding state
			if ( status.seedingStatus  )
			{
				shapeSeeding.BackColor = StatusColors.SEEDING;
			}
			else
			{
				shapeSeeding.BackColor = StatusColors.NOT_SEEDING;
			}


		}

		//	Populate list
		public void populateErrorLog()
		{

			lvErrorLog.Columns.Add("Time", 120, HorizontalAlignment.Left);
			lvErrorLog.Columns.Add("Site", 100, HorizontalAlignment.Left);
			lvErrorLog.Columns.Add("Command/Status", 100, HorizontalAlignment.Left);
			lvErrorLog.Columns.Add("Description", -2, HorizontalAlignment.Left);

		}


        //	Populate weather log list
        public void populateWeatherLog()
        {

            lvwWeatherLog.Columns.Add(@"Time", 120, HorizontalAlignment.Right);
            lvwWeatherLog.Columns.Add(@"Wind Dir (Degrees)", 120, HorizontalAlignment.Right);
            lvwWeatherLog.Columns.Add(@"Wind Speed (m/s)", 120, HorizontalAlignment.Right);
            lvwWeatherLog.Columns.Add(@"Temperature (�C)", 120, HorizontalAlignment.Right);
            lvwWeatherLog.Columns.Add(@"Pressure (mmHg)", 120, HorizontalAlignment.Right);
            lvwWeatherLog.Columns.Add(@"Rel. Humidity (%)", 120, HorizontalAlignment.Right);
            lvwWeatherLog.Columns.Add(@"Heater State", -2, HorizontalAlignment.Right);

        }

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{


			if( disposing )
			{

				//	Disconnect from the VPN
//				VPNConnect.disconnect(_globalConfiguration.configuration.VPNConnectInfo.connectionName);


				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.tabRemote = new System.Windows.Forms.TabControl();
            this.tabPageSystemStatus = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblPressureStatic = new System.Windows.Forms.Label();
            this.lblPressure = new System.Windows.Forms.Label();
            this.shapeNozzleOn2 = new ShapeControl.ShapeControl();
            this.lblLastResetTime = new System.Windows.Forms.Label();
            this.lblNotBurning = new System.Windows.Forms.Label();
            this.lblFlameTemp = new System.Windows.Forms.Label();
            this.shapePurgeOn = new ShapeControl.ShapeControl();
            this.shapeNozzleOn1 = new ShapeControl.ShapeControl();
            this.shapePropaneOff = new ShapeControl.ShapeControl();
            this.shapeSolutionValveOff = new ShapeControl.ShapeControl();
            this.shapeNozzleLine1 = new ShapeControl.ShapeControl();
            this.lblFlowStatic = new System.Windows.Forms.Label();
            this.lblFlowMeter = new System.Windows.Forms.Label();
            this.shapeNozzleOff = new ShapeControl.ShapeControl();
            this.shapeNozzleLine2 = new ShapeControl.ShapeControl();
            this.shapeNozzleValveOutline = new ShapeControl.ShapeControl();
            this.lblNozzle = new System.Windows.Forms.Label();
            this.lblLastReset = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.lblBurning = new System.Windows.Forms.Label();
            this.shapeSeeding = new ShapeControl.ShapeControl();
            this.shapeBurning = new ShapeControl.ShapeControl();
            this.shapePurgeOff = new ShapeControl.ShapeControl();
            this.shapePropaneOn1 = new ShapeControl.ShapeControl();
            this.shapePropaneOn2 = new ShapeControl.ShapeControl();
            this.shapeSolutionValveOn = new ShapeControl.ShapeControl();
            this.shapePropaneLine3 = new ShapeControl.ShapeControl();
            this.shapeSolutionLine4 = new ShapeControl.ShapeControl();
            this.shapeSolutionLine3 = new ShapeControl.ShapeControl();
            this.pictBurning = new System.Windows.Forms.PictureBox();
            this.shapeSolutionLine2 = new ShapeControl.ShapeControl();
            this.shapePropaneLine2 = new ShapeControl.ShapeControl();
            this.shapeSolutionLine1 = new ShapeControl.ShapeControl();
            this.shapePropaneLine1 = new ShapeControl.ShapeControl();
            this.shapeControl3 = new ShapeControl.ShapeControl();
            this.shapeControl2 = new ShapeControl.ShapeControl();
            this.shapePurgeLine = new ShapeControl.ShapeControl();
            this.shapePurgeValve = new ShapeControl.ShapeControl();
            this.txtBatteryVoltage = new System.Windows.Forms.TextBox();
            this.pictBattery = new System.Windows.Forms.PictureBox();
            this.lblBattery = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictIgnition = new System.Windows.Forms.PictureBox();
            this.lblPurge = new System.Windows.Forms.Label();
            this.pictPurge = new System.Windows.Forms.PictureBox();
            this.lblSolution = new System.Windows.Forms.Label();
            this.pictSolution = new System.Windows.Forms.PictureBox();
            this.lblPropane = new System.Windows.Forms.Label();
            this.pictPropane = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tabLog = new System.Windows.Forms.TabPage();
            this.btnFolder = new System.Windows.Forms.Button();
            this.logFileName = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnLogCleared = new System.Windows.Forms.Button();
            this.lvErrorLog = new System.Windows.Forms.ListView();
            this.tabPageWeather = new System.Windows.Forms.TabPage();
            this.btnWeatherFolder = new System.Windows.Forms.Button();
            this.lblWeatherLog = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnUpdateWxHistory = new System.Windows.Forms.Button();
            this.lvwWeatherLog = new System.Windows.Forms.ListView();
            this.btnUpdateWx = new System.Windows.Forms.Button();
            this.cbConnectTo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblRecv = new System.Windows.Forms.Label();
            this.shapeRecv = new ShapeControl.ShapeControl();
            this.lblSend = new System.Windows.Forms.Label();
            this.shapeSend = new ShapeControl.ShapeControl();
            this.lblConnected = new System.Windows.Forms.Label();
            this.shapeConnected = new ShapeControl.ShapeControl();
            this._progressReceive = new System.Windows.Forms.ProgressBar();
            this.txtCommandRemaining = new System.Windows.Forms.TextBox();
            this.mainMenu1 = new System.Windows.Forms.MenuStrip();
            this.menuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuConfiguration = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSkywaveAccount = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRemoteSiteSetup = new System.Windows.Forms.ToolStripMenuItem();
            this.menuVPNSetup = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAlertSetup = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.lvStatus = new System.Windows.Forms.ListView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtMasterTime = new System.Windows.Forms.TextBox();
            this.btnSyncClock = new System.Windows.Forms.Button();
            this.lblCurrentTime = new System.Windows.Forms.Label();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.btnNozzle = new System.Windows.Forms.Button();
            this.btnIgnition = new System.Windows.Forms.Button();
            this.btnPropane = new System.Windows.Forms.Button();
            this.btnPurge = new System.Windows.Forms.Button();
            this.btnSolution = new System.Windows.Forms.Button();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnEndSeeding = new System.Windows.Forms.Button();
            this.btnStartSeed = new System.Windows.Forms.Button();
            this.btnResetSeed = new System.Windows.Forms.Button();
            this.lnkWMI = new System.Windows.Forms.LinkLabel();
            this.timerMasterTime = new System.Windows.Forms.Timer(this.components);
            this.timerKeepAlive = new System.Windows.Forms.Timer(this.components);
            this.tabRemote.SuspendLayout();
            this.tabPageSystemStatus.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictBurning)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictBattery)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictIgnition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictPurge)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictSolution)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictPropane)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabLog.SuspendLayout();
            this.tabPageWeather.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabRemote
            // 
            this.tabRemote.Controls.Add(this.tabPageSystemStatus);
            this.tabRemote.Controls.Add(this.tabLog);
            this.tabRemote.Controls.Add(this.tabPageWeather);
            this.tabRemote.Location = new System.Drawing.Point(264, 88);
            this.tabRemote.Name = "tabRemote";
            this.tabRemote.SelectedIndex = 0;
            this.tabRemote.Size = new System.Drawing.Size(544, 392);
            this.tabRemote.TabIndex = 0;
            this.tabRemote.SelectedIndexChanged += new System.EventHandler(this.tabRemote_SelectedIndexChanged);
            // 
            // tabPageSystemStatus
            // 
            this.tabPageSystemStatus.Controls.Add(this.panel1);
            this.tabPageSystemStatus.Location = new System.Drawing.Point(4, 22);
            this.tabPageSystemStatus.Name = "tabPageSystemStatus";
            this.tabPageSystemStatus.Size = new System.Drawing.Size(536, 366);
            this.tabPageSystemStatus.TabIndex = 2;
            this.tabPageSystemStatus.Text = "System Status";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.lblPressureStatic);
            this.panel1.Controls.Add(this.lblPressure);
            this.panel1.Controls.Add(this.shapeNozzleOn2);
            this.panel1.Controls.Add(this.lblLastResetTime);
            this.panel1.Controls.Add(this.lblNotBurning);
            this.panel1.Controls.Add(this.lblFlameTemp);
            this.panel1.Controls.Add(this.shapePurgeOn);
            this.panel1.Controls.Add(this.shapeNozzleOn1);
            this.panel1.Controls.Add(this.shapePropaneOff);
            this.panel1.Controls.Add(this.shapeSolutionValveOff);
            this.panel1.Controls.Add(this.shapeNozzleLine1);
            this.panel1.Controls.Add(this.lblFlowStatic);
            this.panel1.Controls.Add(this.lblFlowMeter);
            this.panel1.Controls.Add(this.shapeNozzleOff);
            this.panel1.Controls.Add(this.shapeNozzleLine2);
            this.panel1.Controls.Add(this.shapeNozzleValveOutline);
            this.panel1.Controls.Add(this.lblNozzle);
            this.panel1.Controls.Add(this.lblLastReset);
            this.panel1.Controls.Add(this.btnReset);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.lblBurning);
            this.panel1.Controls.Add(this.shapeSeeding);
            this.panel1.Controls.Add(this.shapeBurning);
            this.panel1.Controls.Add(this.shapePurgeOff);
            this.panel1.Controls.Add(this.shapePropaneOn1);
            this.panel1.Controls.Add(this.shapePropaneOn2);
            this.panel1.Controls.Add(this.shapeSolutionValveOn);
            this.panel1.Controls.Add(this.shapePropaneLine3);
            this.panel1.Controls.Add(this.shapeSolutionLine4);
            this.panel1.Controls.Add(this.shapeSolutionLine3);
            this.panel1.Controls.Add(this.pictBurning);
            this.panel1.Controls.Add(this.shapeSolutionLine2);
            this.panel1.Controls.Add(this.shapePropaneLine2);
            this.panel1.Controls.Add(this.shapeSolutionLine1);
            this.panel1.Controls.Add(this.shapePropaneLine1);
            this.panel1.Controls.Add(this.shapeControl3);
            this.panel1.Controls.Add(this.shapeControl2);
            this.panel1.Controls.Add(this.shapePurgeLine);
            this.panel1.Controls.Add(this.shapePurgeValve);
            this.panel1.Controls.Add(this.txtBatteryVoltage);
            this.panel1.Controls.Add(this.pictBattery);
            this.panel1.Controls.Add(this.lblBattery);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.pictIgnition);
            this.panel1.Controls.Add(this.lblPurge);
            this.panel1.Controls.Add(this.pictPurge);
            this.panel1.Controls.Add(this.lblSolution);
            this.panel1.Controls.Add(this.pictSolution);
            this.panel1.Controls.Add(this.lblPropane);
            this.panel1.Controls.Add(this.pictPropane);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(8, 8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(520, 352);
            this.panel1.TabIndex = 0;
            // 
            // lblPressureStatic
            // 
            this.lblPressureStatic.Location = new System.Drawing.Point(336, 224);
            this.lblPressureStatic.Name = "lblPressureStatic";
            this.lblPressureStatic.Size = new System.Drawing.Size(56, 16);
            this.lblPressureStatic.TabIndex = 58;
            this.lblPressureStatic.Text = "Pressure";
            // 
            // lblPressure
            // 
            this.lblPressure.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPressure.Location = new System.Drawing.Point(264, 224);
            this.lblPressure.Name = "lblPressure";
            this.lblPressure.Size = new System.Drawing.Size(64, 16);
            this.lblPressure.TabIndex = 57;
            this.lblPressure.Text = "--.- PSI";
            // 
            // shapeNozzleOn2
            // 
            this.shapeNozzleOn2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(141)))), ((int)(((byte)(0)))));
            this.shapeNozzleOn2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(141)))), ((int)(((byte)(0)))));
            this.shapeNozzleOn2.BorderStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.shapeNozzleOn2.BorderWidth = 1;
            this.shapeNozzleOn2.CenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.shapeNozzleOn2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.shapeNozzleOn2.Location = new System.Drawing.Point(184, 112);
            this.shapeNozzleOn2.Name = "shapeNozzleOn2";
            this.shapeNozzleOn2.Shape = ShapeControl.ShapeType.Rectangle;
            this.shapeNozzleOn2.Size = new System.Drawing.Size(16, 8);
            this.shapeNozzleOn2.SurroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.shapeNozzleOn2.TabIndex = 55;
            this.shapeNozzleOn2.UseGradient = false;
            // 
            // lblLastResetTime
            // 
            this.lblLastResetTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLastResetTime.Location = new System.Drawing.Point(16, 32);
            this.lblLastResetTime.Name = "lblLastResetTime";
            this.lblLastResetTime.Size = new System.Drawing.Size(128, 23);
            this.lblLastResetTime.TabIndex = 54;
            // 
            // lblNotBurning
            // 
            this.lblNotBurning.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblNotBurning.ForeColor = System.Drawing.Color.Black;
            this.lblNotBurning.Location = new System.Drawing.Point(169, 289);
            this.lblNotBurning.Name = "lblNotBurning";
            this.lblNotBurning.Size = new System.Drawing.Size(56, 56);
            this.lblNotBurning.TabIndex = 53;
            this.lblNotBurning.Text = "Not Burning";
            // 
            // lblFlameTemp
            // 
            this.lblFlameTemp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblFlameTemp.Location = new System.Drawing.Point(235, 155);
            this.lblFlameTemp.Name = "lblFlameTemp";
            this.lblFlameTemp.Size = new System.Drawing.Size(48, 16);
            this.lblFlameTemp.TabIndex = 52;
            this.lblFlameTemp.Text = "-.- �F";
            this.lblFlameTemp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // shapePurgeOn
            // 
            this.shapePurgeOn.BackColor = System.Drawing.Color.Yellow;
            this.shapePurgeOn.BorderColor = System.Drawing.Color.Yellow;
            this.shapePurgeOn.BorderStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.shapePurgeOn.BorderWidth = 1;
            this.shapePurgeOn.CenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.shapePurgeOn.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.shapePurgeOn.Location = new System.Drawing.Point(354, 118);
            this.shapePurgeOn.Name = "shapePurgeOn";
            this.shapePurgeOn.Shape = ShapeControl.ShapeType.Rectangle;
            this.shapePurgeOn.Size = new System.Drawing.Size(21, 7);
            this.shapePurgeOn.SurroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.shapePurgeOn.TabIndex = 51;
            this.shapePurgeOn.UseGradient = false;
            // 
            // shapeNozzleOn1
            // 
            this.shapeNozzleOn1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(141)))), ((int)(((byte)(0)))));
            this.shapeNozzleOn1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(141)))), ((int)(((byte)(0)))));
            this.shapeNozzleOn1.BorderStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.shapeNozzleOn1.BorderWidth = 1;
            this.shapeNozzleOn1.CenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.shapeNozzleOn1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.shapeNozzleOn1.Location = new System.Drawing.Point(184, 120);
            this.shapeNozzleOn1.Name = "shapeNozzleOn1";
            this.shapeNozzleOn1.Shape = ShapeControl.ShapeType.Rectangle;
            this.shapeNozzleOn1.Size = new System.Drawing.Size(8, 10);
            this.shapeNozzleOn1.SurroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.shapeNozzleOn1.TabIndex = 50;
            this.shapeNozzleOn1.UseGradient = false;
            // 
            // shapePropaneOff
            // 
            this.shapePropaneOff.BackColor = System.Drawing.Color.LightGray;
            this.shapePropaneOff.BorderColor = System.Drawing.Color.LightGray;
            this.shapePropaneOff.BorderStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.shapePropaneOff.BorderWidth = 1;
            this.shapePropaneOff.CenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.shapePropaneOff.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.shapePropaneOff.Location = new System.Drawing.Point(218, 191);
            this.shapePropaneOff.Name = "shapePropaneOff";
            this.shapePropaneOff.Shape = ShapeControl.ShapeType.Rectangle;
            this.shapePropaneOff.Size = new System.Drawing.Size(20, 8);
            this.shapePropaneOff.SurroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.shapePropaneOff.TabIndex = 49;
            this.shapePropaneOff.UseGradient = false;
            // 
            // shapeSolutionValveOff
            // 
            this.shapeSolutionValveOff.BackColor = System.Drawing.Color.LightGray;
            this.shapeSolutionValveOff.BorderColor = System.Drawing.Color.LightGray;
            this.shapeSolutionValveOff.BorderStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.shapeSolutionValveOff.BorderWidth = 1;
            this.shapeSolutionValveOff.CenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.shapeSolutionValveOff.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.shapeSolutionValveOff.Location = new System.Drawing.Point(281, 184);
            this.shapeSolutionValveOff.Name = "shapeSolutionValveOff";
            this.shapeSolutionValveOff.Shape = ShapeControl.ShapeType.Rectangle;
            this.shapeSolutionValveOff.Size = new System.Drawing.Size(21, 8);
            this.shapeSolutionValveOff.SurroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.shapeSolutionValveOff.TabIndex = 48;
            this.shapeSolutionValveOff.UseGradient = false;
            // 
            // shapeNozzleLine1
            // 
            this.shapeNozzleLine1.BackColor = System.Drawing.Color.Green;
            this.shapeNozzleLine1.BorderColor = System.Drawing.Color.Green;
            this.shapeNozzleLine1.BorderStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.shapeNozzleLine1.BorderWidth = 1;
            this.shapeNozzleLine1.CenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.shapeNozzleLine1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.shapeNozzleLine1.Location = new System.Drawing.Point(184, 128);
            this.shapeNozzleLine1.Name = "shapeNozzleLine1";
            this.shapeNozzleLine1.Shape = ShapeControl.ShapeType.Rectangle;
            this.shapeNozzleLine1.Size = new System.Drawing.Size(8, 64);
            this.shapeNozzleLine1.SurroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.shapeNozzleLine1.TabIndex = 47;
            this.shapeNozzleLine1.UseGradient = false;
            // 
            // lblFlowStatic
            // 
            this.lblFlowStatic.Location = new System.Drawing.Point(336, 200);
            this.lblFlowStatic.Name = "lblFlowStatic";
            this.lblFlowStatic.Size = new System.Drawing.Size(32, 16);
            this.lblFlowStatic.TabIndex = 46;
            this.lblFlowStatic.Text = "Flow";
            // 
            // lblFlowMeter
            // 
            this.lblFlowMeter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblFlowMeter.Location = new System.Drawing.Point(264, 208);
            this.lblFlowMeter.Name = "lblFlowMeter";
            this.lblFlowMeter.Size = new System.Drawing.Size(64, 16);
            this.lblFlowMeter.TabIndex = 45;
            this.lblFlowMeter.Text = ".-- GPH";
            // 
            // shapeNozzleOff
            // 
            this.shapeNozzleOff.BackColor = System.Drawing.Color.LightGray;
            this.shapeNozzleOff.BorderColor = System.Drawing.Color.LightGray;
            this.shapeNozzleOff.BorderStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.shapeNozzleOff.BorderWidth = 1;
            this.shapeNozzleOff.CenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.shapeNozzleOff.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.shapeNozzleOff.Location = new System.Drawing.Point(184, 106);
            this.shapeNozzleOff.Name = "shapeNozzleOff";
            this.shapeNozzleOff.Shape = ShapeControl.ShapeType.Rectangle;
            this.shapeNozzleOff.Size = new System.Drawing.Size(8, 21);
            this.shapeNozzleOff.SurroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.shapeNozzleOff.TabIndex = 44;
            this.shapeNozzleOff.UseGradient = false;
            // 
            // shapeNozzleLine2
            // 
            this.shapeNozzleLine2.BackColor = System.Drawing.Color.LightGray;
            this.shapeNozzleLine2.BorderColor = System.Drawing.Color.LightGray;
            this.shapeNozzleLine2.BorderStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.shapeNozzleLine2.BorderWidth = 1;
            this.shapeNozzleLine2.CenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.shapeNozzleLine2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.shapeNozzleLine2.Location = new System.Drawing.Point(200, 112);
            this.shapeNozzleLine2.Name = "shapeNozzleLine2";
            this.shapeNozzleLine2.Shape = ShapeControl.ShapeType.Rectangle;
            this.shapeNozzleLine2.Size = new System.Drawing.Size(40, 8);
            this.shapeNozzleLine2.SurroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.shapeNozzleLine2.TabIndex = 43;
            this.shapeNozzleLine2.UseGradient = false;
            // 
            // shapeNozzleValveOutline
            // 
            this.shapeNozzleValveOutline.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.shapeNozzleValveOutline.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.shapeNozzleValveOutline.BorderStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.shapeNozzleValveOutline.BorderWidth = 2;
            this.shapeNozzleValveOutline.CenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.shapeNozzleValveOutline.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.shapeNozzleValveOutline.Location = new System.Drawing.Point(176, 104);
            this.shapeNozzleValveOutline.Name = "shapeNozzleValveOutline";
            this.shapeNozzleValveOutline.Shape = ShapeControl.ShapeType.Ellipse;
            this.shapeNozzleValveOutline.Size = new System.Drawing.Size(24, 24);
            this.shapeNozzleValveOutline.SurroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.shapeNozzleValveOutline.TabIndex = 41;
            this.shapeNozzleValveOutline.UseGradient = false;
            // 
            // lblNozzle
            // 
            this.lblNozzle.BackColor = System.Drawing.Color.White;
            this.lblNozzle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNozzle.Location = new System.Drawing.Point(168, 80);
            this.lblNozzle.Name = "lblNozzle";
            this.lblNozzle.Size = new System.Drawing.Size(48, 23);
            this.lblNozzle.TabIndex = 40;
            this.lblNozzle.Text = "Nozzle";
            // 
            // lblLastReset
            // 
            this.lblLastReset.Location = new System.Drawing.Point(16, 8);
            this.lblLastReset.Name = "lblLastReset";
            this.lblLastReset.Size = new System.Drawing.Size(72, 23);
            this.lblLastReset.TabIndex = 37;
            this.lblLastReset.Text = "Last Reset:";
            // 
            // btnReset
            // 
            this.btnReset.BackColor = System.Drawing.Color.Red;
            this.btnReset.ForeColor = System.Drawing.Color.White;
            this.btnReset.Location = new System.Drawing.Point(16, 64);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 36;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(448, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 16);
            this.label3.TabIndex = 35;
            this.label3.Text = "Seeding";
            // 
            // lblBurning
            // 
            this.lblBurning.Location = new System.Drawing.Point(448, 16);
            this.lblBurning.Name = "lblBurning";
            this.lblBurning.Size = new System.Drawing.Size(48, 16);
            this.lblBurning.TabIndex = 34;
            this.lblBurning.Text = "Burning";
            // 
            // shapeSeeding
            // 
            this.shapeSeeding.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.shapeSeeding.BorderColor = System.Drawing.Color.Black;
            this.shapeSeeding.BorderStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.shapeSeeding.BorderWidth = 1;
            this.shapeSeeding.CenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.shapeSeeding.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.shapeSeeding.Location = new System.Drawing.Point(424, 40);
            this.shapeSeeding.Name = "shapeSeeding";
            this.shapeSeeding.Shape = ShapeControl.ShapeType.Ellipse;
            this.shapeSeeding.Size = new System.Drawing.Size(16, 16);
            this.shapeSeeding.SurroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.shapeSeeding.TabIndex = 33;
            this.shapeSeeding.UseGradient = false;
            // 
            // shapeBurning
            // 
            this.shapeBurning.BackColor = System.Drawing.Color.White;
            this.shapeBurning.BorderColor = System.Drawing.Color.Black;
            this.shapeBurning.BorderStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.shapeBurning.BorderWidth = 1;
            this.shapeBurning.CenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.shapeBurning.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.shapeBurning.Location = new System.Drawing.Point(424, 16);
            this.shapeBurning.Name = "shapeBurning";
            this.shapeBurning.Shape = ShapeControl.ShapeType.Ellipse;
            this.shapeBurning.Size = new System.Drawing.Size(16, 16);
            this.shapeBurning.SurroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.shapeBurning.TabIndex = 32;
            this.shapeBurning.UseGradient = false;
            // 
            // shapePurgeOff
            // 
            this.shapePurgeOff.BackColor = System.Drawing.Color.LightGray;
            this.shapePurgeOff.BorderColor = System.Drawing.Color.LightGray;
            this.shapePurgeOff.BorderStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.shapePurgeOff.BorderWidth = 1;
            this.shapePurgeOff.CenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.shapePurgeOff.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.shapePurgeOff.Location = new System.Drawing.Point(360, 112);
            this.shapePurgeOff.Name = "shapePurgeOff";
            this.shapePurgeOff.Shape = ShapeControl.ShapeType.Rectangle;
            this.shapePurgeOff.Size = new System.Drawing.Size(8, 21);
            this.shapePurgeOff.SurroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.shapePurgeOff.TabIndex = 31;
            this.shapePurgeOff.UseGradient = false;
            // 
            // shapePropaneOn1
            // 
            this.shapePropaneOn1.BackColor = System.Drawing.Color.Green;
            this.shapePropaneOn1.BorderColor = System.Drawing.Color.Green;
            this.shapePropaneOn1.BorderStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.shapePropaneOn1.BorderWidth = 1;
            this.shapePropaneOn1.CenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.shapePropaneOn1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.shapePropaneOn1.Location = new System.Drawing.Point(216, 191);
            this.shapePropaneOn1.Name = "shapePropaneOn1";
            this.shapePropaneOn1.Shape = ShapeControl.ShapeType.Rectangle;
            this.shapePropaneOn1.Size = new System.Drawing.Size(16, 8);
            this.shapePropaneOn1.SurroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.shapePropaneOn1.TabIndex = 30;
            this.shapePropaneOn1.UseGradient = false;
            // 
            // shapePropaneOn2
            // 
            this.shapePropaneOn2.BackColor = System.Drawing.Color.Green;
            this.shapePropaneOn2.BorderColor = System.Drawing.Color.Green;
            this.shapePropaneOn2.BorderStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.shapePropaneOn2.BorderWidth = 1;
            this.shapePropaneOn2.CenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.shapePropaneOn2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.shapePropaneOn2.Location = new System.Drawing.Point(224, 184);
            this.shapePropaneOn2.Name = "shapePropaneOn2";
            this.shapePropaneOn2.Shape = ShapeControl.ShapeType.Rectangle;
            this.shapePropaneOn2.Size = new System.Drawing.Size(8, 8);
            this.shapePropaneOn2.SurroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.shapePropaneOn2.TabIndex = 29;
            this.shapePropaneOn2.UseGradient = false;
            // 
            // shapeSolutionValveOn
            // 
            this.shapeSolutionValveOn.BackColor = System.Drawing.Color.DarkBlue;
            this.shapeSolutionValveOn.BorderColor = System.Drawing.Color.DarkBlue;
            this.shapeSolutionValveOn.BorderStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.shapeSolutionValveOn.BorderWidth = 1;
            this.shapeSolutionValveOn.CenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.shapeSolutionValveOn.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.shapeSolutionValveOn.Location = new System.Drawing.Point(288, 177);
            this.shapeSolutionValveOn.Name = "shapeSolutionValveOn";
            this.shapeSolutionValveOn.Shape = ShapeControl.ShapeType.Rectangle;
            this.shapeSolutionValveOn.Size = new System.Drawing.Size(8, 21);
            this.shapeSolutionValveOn.SurroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.shapeSolutionValveOn.TabIndex = 28;
            this.shapeSolutionValveOn.UseGradient = false;
            // 
            // shapePropaneLine3
            // 
            this.shapePropaneLine3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.shapePropaneLine3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.shapePropaneLine3.BorderStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.shapePropaneLine3.BorderWidth = 1;
            this.shapePropaneLine3.CenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.shapePropaneLine3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.shapePropaneLine3.Location = new System.Drawing.Point(224, 136);
            this.shapePropaneLine3.Name = "shapePropaneLine3";
            this.shapePropaneLine3.Shape = ShapeControl.ShapeType.Rectangle;
            this.shapePropaneLine3.Size = new System.Drawing.Size(15, 8);
            this.shapePropaneLine3.SurroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.shapePropaneLine3.TabIndex = 27;
            this.shapePropaneLine3.UseGradient = false;
            // 
            // shapeSolutionLine4
            // 
            this.shapeSolutionLine4.BackColor = System.Drawing.Color.DarkBlue;
            this.shapeSolutionLine4.BorderColor = System.Drawing.Color.DarkBlue;
            this.shapeSolutionLine4.BorderStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.shapeSolutionLine4.BorderWidth = 1;
            this.shapeSolutionLine4.CenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.shapeSolutionLine4.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.shapeSolutionLine4.Location = new System.Drawing.Point(296, 118);
            this.shapeSolutionLine4.Name = "shapeSolutionLine4";
            this.shapeSolutionLine4.Shape = ShapeControl.ShapeType.Rectangle;
            this.shapeSolutionLine4.Size = new System.Drawing.Size(57, 8);
            this.shapeSolutionLine4.SurroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.shapeSolutionLine4.TabIndex = 26;
            this.shapeSolutionLine4.UseGradient = false;
            // 
            // shapeSolutionLine3
            // 
            this.shapeSolutionLine3.BackColor = System.Drawing.Color.DarkBlue;
            this.shapeSolutionLine3.BorderColor = System.Drawing.Color.DarkBlue;
            this.shapeSolutionLine3.BorderStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.shapeSolutionLine3.BorderWidth = 1;
            this.shapeSolutionLine3.CenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.shapeSolutionLine3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.shapeSolutionLine3.Location = new System.Drawing.Point(280, 118);
            this.shapeSolutionLine3.Name = "shapeSolutionLine3";
            this.shapeSolutionLine3.Shape = ShapeControl.ShapeType.Rectangle;
            this.shapeSolutionLine3.Size = new System.Drawing.Size(16, 8);
            this.shapeSolutionLine3.SurroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.shapeSolutionLine3.TabIndex = 25;
            this.shapeSolutionLine3.UseGradient = false;
            // 
            // pictBurning
            // 
            this.pictBurning.BackColor = System.Drawing.Color.White;
            this.pictBurning.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictBurning.Image = ((System.Drawing.Image)(resources.GetObject("pictBurning.Image")));
            this.pictBurning.Location = new System.Drawing.Point(240, 103);
            this.pictBurning.Name = "pictBurning";
            this.pictBurning.Size = new System.Drawing.Size(40, 48);
            this.pictBurning.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictBurning.TabIndex = 23;
            this.pictBurning.TabStop = false;
            this.pictBurning.Visible = false;
            // 
            // shapeSolutionLine2
            // 
            this.shapeSolutionLine2.BackColor = System.Drawing.Color.DarkBlue;
            this.shapeSolutionLine2.BorderColor = System.Drawing.Color.DarkBlue;
            this.shapeSolutionLine2.BorderStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.shapeSolutionLine2.BorderWidth = 1;
            this.shapeSolutionLine2.CenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.shapeSolutionLine2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.shapeSolutionLine2.Location = new System.Drawing.Point(288, 118);
            this.shapeSolutionLine2.Name = "shapeSolutionLine2";
            this.shapeSolutionLine2.Shape = ShapeControl.ShapeType.Rectangle;
            this.shapeSolutionLine2.Size = new System.Drawing.Size(8, 58);
            this.shapeSolutionLine2.SurroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.shapeSolutionLine2.TabIndex = 22;
            this.shapeSolutionLine2.UseGradient = false;
            // 
            // shapePropaneLine2
            // 
            this.shapePropaneLine2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.shapePropaneLine2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.shapePropaneLine2.BorderStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.shapePropaneLine2.BorderWidth = 1;
            this.shapePropaneLine2.CenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.shapePropaneLine2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.shapePropaneLine2.Location = new System.Drawing.Point(224, 144);
            this.shapePropaneLine2.Name = "shapePropaneLine2";
            this.shapePropaneLine2.Shape = ShapeControl.ShapeType.Rectangle;
            this.shapePropaneLine2.Size = new System.Drawing.Size(8, 40);
            this.shapePropaneLine2.SurroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.shapePropaneLine2.TabIndex = 21;
            this.shapePropaneLine2.UseGradient = false;
            // 
            // shapeSolutionLine1
            // 
            this.shapeSolutionLine1.BackColor = System.Drawing.Color.DarkBlue;
            this.shapeSolutionLine1.BorderColor = System.Drawing.Color.DarkBlue;
            this.shapeSolutionLine1.BorderStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.shapeSolutionLine1.BorderWidth = 1;
            this.shapeSolutionLine1.CenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.shapeSolutionLine1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.shapeSolutionLine1.Location = new System.Drawing.Point(288, 199);
            this.shapeSolutionLine1.Name = "shapeSolutionLine1";
            this.shapeSolutionLine1.Shape = ShapeControl.ShapeType.Rectangle;
            this.shapeSolutionLine1.Size = new System.Drawing.Size(8, 48);
            this.shapeSolutionLine1.SurroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.shapeSolutionLine1.TabIndex = 20;
            this.shapeSolutionLine1.UseGradient = false;
            // 
            // shapePropaneLine1
            // 
            this.shapePropaneLine1.BackColor = System.Drawing.Color.Green;
            this.shapePropaneLine1.BorderColor = System.Drawing.Color.Green;
            this.shapePropaneLine1.BorderStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.shapePropaneLine1.BorderWidth = 1;
            this.shapePropaneLine1.CenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.shapePropaneLine1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.shapePropaneLine1.Location = new System.Drawing.Point(153, 191);
            this.shapePropaneLine1.Name = "shapePropaneLine1";
            this.shapePropaneLine1.Shape = ShapeControl.ShapeType.Rectangle;
            this.shapePropaneLine1.Size = new System.Drawing.Size(64, 8);
            this.shapePropaneLine1.SurroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.shapePropaneLine1.TabIndex = 19;
            this.shapePropaneLine1.UseGradient = false;
            // 
            // shapeControl3
            // 
            this.shapeControl3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.shapeControl3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.shapeControl3.BorderStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.shapeControl3.BorderWidth = 2;
            this.shapeControl3.CenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.shapeControl3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.shapeControl3.Location = new System.Drawing.Point(216, 183);
            this.shapeControl3.Name = "shapeControl3";
            this.shapeControl3.Shape = ShapeControl.ShapeType.Ellipse;
            this.shapeControl3.Size = new System.Drawing.Size(24, 24);
            this.shapeControl3.SurroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.shapeControl3.TabIndex = 18;
            this.shapeControl3.UseGradient = false;
            // 
            // shapeControl2
            // 
            this.shapeControl2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.shapeControl2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.shapeControl2.BorderStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.shapeControl2.BorderWidth = 2;
            this.shapeControl2.CenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.shapeControl2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.shapeControl2.Location = new System.Drawing.Point(280, 175);
            this.shapeControl2.Name = "shapeControl2";
            this.shapeControl2.Shape = ShapeControl.ShapeType.Ellipse;
            this.shapeControl2.Size = new System.Drawing.Size(24, 24);
            this.shapeControl2.SurroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.shapeControl2.TabIndex = 17;
            this.shapeControl2.UseGradient = false;
            // 
            // shapePurgeLine
            // 
            this.shapePurgeLine.BackColor = System.Drawing.Color.Yellow;
            this.shapePurgeLine.BorderColor = System.Drawing.Color.Yellow;
            this.shapePurgeLine.BorderStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.shapePurgeLine.BorderWidth = 1;
            this.shapePurgeLine.CenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.shapePurgeLine.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.shapePurgeLine.Location = new System.Drawing.Point(376, 118);
            this.shapePurgeLine.Name = "shapePurgeLine";
            this.shapePurgeLine.Shape = ShapeControl.ShapeType.Rectangle;
            this.shapePurgeLine.Size = new System.Drawing.Size(34, 8);
            this.shapePurgeLine.SurroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.shapePurgeLine.TabIndex = 16;
            this.shapePurgeLine.UseGradient = false;
            // 
            // shapePurgeValve
            // 
            this.shapePurgeValve.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.shapePurgeValve.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.shapePurgeValve.BorderStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.shapePurgeValve.BorderWidth = 2;
            this.shapePurgeValve.CenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.shapePurgeValve.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.shapePurgeValve.Location = new System.Drawing.Point(352, 110);
            this.shapePurgeValve.Name = "shapePurgeValve";
            this.shapePurgeValve.Shape = ShapeControl.ShapeType.Ellipse;
            this.shapePurgeValve.Size = new System.Drawing.Size(24, 24);
            this.shapePurgeValve.SurroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.shapePurgeValve.TabIndex = 15;
            this.shapePurgeValve.UseGradient = false;
            // 
            // txtBatteryVoltage
            // 
            this.txtBatteryVoltage.BackColor = System.Drawing.Color.Black;
            this.txtBatteryVoltage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBatteryVoltage.ForeColor = System.Drawing.Color.White;
            this.txtBatteryVoltage.Location = new System.Drawing.Point(416, 280);
            this.txtBatteryVoltage.Name = "txtBatteryVoltage";
            this.txtBatteryVoltage.Size = new System.Drawing.Size(40, 13);
            this.txtBatteryVoltage.TabIndex = 14;
            this.txtBatteryVoltage.Text = "--.-V";
            // 
            // pictBattery
            // 
            this.pictBattery.BackColor = System.Drawing.Color.White;
            this.pictBattery.Image = ((System.Drawing.Image)(resources.GetObject("pictBattery.Image")));
            this.pictBattery.Location = new System.Drawing.Point(408, 256);
            this.pictBattery.Name = "pictBattery";
            this.pictBattery.Size = new System.Drawing.Size(80, 64);
            this.pictBattery.TabIndex = 13;
            this.pictBattery.TabStop = false;
            // 
            // lblBattery
            // 
            this.lblBattery.BackColor = System.Drawing.Color.White;
            this.lblBattery.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBattery.Location = new System.Drawing.Point(424, 325);
            this.lblBattery.Name = "lblBattery";
            this.lblBattery.Size = new System.Drawing.Size(48, 23);
            this.lblBattery.TabIndex = 12;
            this.lblBattery.Text = "Battery";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(240, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 23);
            this.label2.TabIndex = 8;
            this.label2.Text = "Ignition";
            // 
            // pictIgnition
            // 
            this.pictIgnition.BackColor = System.Drawing.Color.White;
            this.pictIgnition.Image = ((System.Drawing.Image)(resources.GetObject("pictIgnition.Image")));
            this.pictIgnition.Location = new System.Drawing.Point(248, 32);
            this.pictIgnition.Name = "pictIgnition";
            this.pictIgnition.Size = new System.Drawing.Size(24, 80);
            this.pictIgnition.TabIndex = 7;
            this.pictIgnition.TabStop = false;
            // 
            // lblPurge
            // 
            this.lblPurge.BackColor = System.Drawing.Color.White;
            this.lblPurge.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPurge.Location = new System.Drawing.Point(440, 152);
            this.lblPurge.Name = "lblPurge";
            this.lblPurge.Size = new System.Drawing.Size(48, 23);
            this.lblPurge.TabIndex = 6;
            this.lblPurge.Text = "Purge";
            // 
            // pictPurge
            // 
            this.pictPurge.BackColor = System.Drawing.Color.White;
            this.pictPurge.Image = ((System.Drawing.Image)(resources.GetObject("pictPurge.Image")));
            this.pictPurge.Location = new System.Drawing.Point(400, 108);
            this.pictPurge.Name = "pictPurge";
            this.pictPurge.Size = new System.Drawing.Size(27, 96);
            this.pictPurge.TabIndex = 5;
            this.pictPurge.TabStop = false;
            // 
            // lblSolution
            // 
            this.lblSolution.BackColor = System.Drawing.Color.White;
            this.lblSolution.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSolution.Location = new System.Drawing.Point(272, 322);
            this.lblSolution.Name = "lblSolution";
            this.lblSolution.Size = new System.Drawing.Size(48, 23);
            this.lblSolution.TabIndex = 4;
            this.lblSolution.Text = "Solution";
            // 
            // pictSolution
            // 
            this.pictSolution.BackColor = System.Drawing.Color.White;
            this.pictSolution.Image = ((System.Drawing.Image)(resources.GetObject("pictSolution.Image")));
            this.pictSolution.Location = new System.Drawing.Point(270, 244);
            this.pictSolution.Name = "pictSolution";
            this.pictSolution.Size = new System.Drawing.Size(48, 80);
            this.pictSolution.TabIndex = 3;
            this.pictSolution.TabStop = false;
            // 
            // lblPropane
            // 
            this.lblPropane.BackColor = System.Drawing.Color.White;
            this.lblPropane.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPropane.Location = new System.Drawing.Point(47, 184);
            this.lblPropane.Name = "lblPropane";
            this.lblPropane.Size = new System.Drawing.Size(65, 23);
            this.lblPropane.TabIndex = 2;
            this.lblPropane.Text = "Propane";
            // 
            // pictPropane
            // 
            this.pictPropane.BackColor = System.Drawing.Color.White;
            this.pictPropane.Image = ((System.Drawing.Image)(resources.GetObject("pictPropane.Image")));
            this.pictPropane.Location = new System.Drawing.Point(104, 183);
            this.pictPropane.Name = "pictPropane";
            this.pictPropane.Size = new System.Drawing.Size(72, 72);
            this.pictPropane.TabIndex = 1;
            this.pictPropane.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(8, 290);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(150, 53);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 38;
            this.pictureBox1.TabStop = false;
            // 
            // tabLog
            // 
            this.tabLog.Controls.Add(this.btnFolder);
            this.tabLog.Controls.Add(this.logFileName);
            this.tabLog.Controls.Add(this.label4);
            this.tabLog.Controls.Add(this.btnLogCleared);
            this.tabLog.Controls.Add(this.lvErrorLog);
            this.tabLog.Location = new System.Drawing.Point(4, 22);
            this.tabLog.Name = "tabLog";
            this.tabLog.Size = new System.Drawing.Size(536, 366);
            this.tabLog.TabIndex = 1;
            this.tabLog.Text = "Log";
            // 
            // btnFolder
            // 
            this.btnFolder.Location = new System.Drawing.Point(437, 302);
            this.btnFolder.Name = "btnFolder";
            this.btnFolder.Size = new System.Drawing.Size(75, 23);
            this.btnFolder.TabIndex = 4;
            this.btnFolder.Text = "Open Folder";
            this.btnFolder.Click += new System.EventHandler(this.btnFolder_Click);
            // 
            // logFileName
            // 
            this.logFileName.Location = new System.Drawing.Point(105, 330);
            this.logFileName.Name = "logFileName";
            this.logFileName.Size = new System.Drawing.Size(628, 23);
            this.logFileName.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(17, 330);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(495, 23);
            this.label4.TabIndex = 2;
            this.label4.Text = "Current Log File:";
            // 
            // btnLogCleared
            // 
            this.btnLogCleared.Location = new System.Drawing.Point(16, 304);
            this.btnLogCleared.Name = "btnLogCleared";
            this.btnLogCleared.Size = new System.Drawing.Size(75, 23);
            this.btnLogCleared.TabIndex = 1;
            this.btnLogCleared.Text = "Clear Log";
            this.btnLogCleared.Click += new System.EventHandler(this.btnLogCleared_Click);
            // 
            // lvErrorLog
            // 
            this.lvErrorLog.GridLines = true;
            this.lvErrorLog.Location = new System.Drawing.Point(16, 24);
            this.lvErrorLog.Name = "lvErrorLog";
            this.lvErrorLog.Size = new System.Drawing.Size(496, 273);
            this.lvErrorLog.TabIndex = 0;
            this.lvErrorLog.UseCompatibleStateImageBehavior = false;
            this.lvErrorLog.View = System.Windows.Forms.View.Details;
            // 
            // tabPageWeather
            // 
            this.tabPageWeather.Controls.Add(this.btnWeatherFolder);
            this.tabPageWeather.Controls.Add(this.lblWeatherLog);
            this.tabPageWeather.Controls.Add(this.label6);
            this.tabPageWeather.Controls.Add(this.btnUpdateWxHistory);
            this.tabPageWeather.Controls.Add(this.lvwWeatherLog);
            this.tabPageWeather.Controls.Add(this.btnUpdateWx);
            this.tabPageWeather.Location = new System.Drawing.Point(4, 22);
            this.tabPageWeather.Name = "tabPageWeather";
            this.tabPageWeather.Size = new System.Drawing.Size(536, 366);
            this.tabPageWeather.TabIndex = 0;
            this.tabPageWeather.Text = "Weather";
            // 
            // btnWeatherFolder
            // 
            this.btnWeatherFolder.Location = new System.Drawing.Point(439, 304);
            this.btnWeatherFolder.Name = "btnWeatherFolder";
            this.btnWeatherFolder.Size = new System.Drawing.Size(75, 23);
            this.btnWeatherFolder.TabIndex = 27;
            this.btnWeatherFolder.Text = "Open Folder";
            this.btnWeatherFolder.Click += new System.EventHandler(this.btnWeatherFolder_Click);
            // 
            // lblWeatherLog
            // 
            this.lblWeatherLog.Location = new System.Drawing.Point(146, 335);
            this.lblWeatherLog.Name = "lblWeatherLog";
            this.lblWeatherLog.Size = new System.Drawing.Size(366, 23);
            this.lblWeatherLog.TabIndex = 26;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(18, 335);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(122, 23);
            this.label6.TabIndex = 25;
            this.label6.Text = "Current Wx Log File:";
            // 
            // btnUpdateWxHistory
            // 
            this.btnUpdateWxHistory.Location = new System.Drawing.Point(100, 304);
            this.btnUpdateWxHistory.Name = "btnUpdateWxHistory";
            this.btnUpdateWxHistory.Size = new System.Drawing.Size(75, 23);
            this.btnUpdateWxHistory.TabIndex = 24;
            this.btnUpdateWxHistory.Text = "History WX";
            this.btnUpdateWxHistory.Click += new System.EventHandler(this.btnUpdateWxHistory_Click);
            // 
            // lvwWeatherLog
            // 
            this.lvwWeatherLog.GridLines = true;
            this.lvwWeatherLog.Location = new System.Drawing.Point(16, 24);
            this.lvwWeatherLog.Name = "lvwWeatherLog";
            this.lvwWeatherLog.Size = new System.Drawing.Size(496, 272);
            this.lvwWeatherLog.TabIndex = 23;
            this.lvwWeatherLog.UseCompatibleStateImageBehavior = false;
            this.lvwWeatherLog.View = System.Windows.Forms.View.Details;
            // 
            // btnUpdateWx
            // 
            this.btnUpdateWx.Location = new System.Drawing.Point(16, 304);
            this.btnUpdateWx.Name = "btnUpdateWx";
            this.btnUpdateWx.Size = new System.Drawing.Size(75, 23);
            this.btnUpdateWx.TabIndex = 22;
            this.btnUpdateWx.Text = "Recent WX";
            this.btnUpdateWx.Click += new System.EventHandler(this.btnUpdateWx_Click);
            // 
            // cbConnectTo
            // 
            this.cbConnectTo.Location = new System.Drawing.Point(16, 37);
            this.cbConnectTo.Name = "cbConnectTo";
            this.cbConnectTo.Size = new System.Drawing.Size(176, 21);
            this.cbConnectTo.TabIndex = 1;
            this.cbConnectTo.SelectedIndexChanged += new System.EventHandler(this.connectTo_IndexChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(24, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Active Site:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblRecv);
            this.groupBox1.Controls.Add(this.shapeRecv);
            this.groupBox1.Controls.Add(this.lblSend);
            this.groupBox1.Controls.Add(this.shapeSend);
            this.groupBox1.Controls.Add(this.lblConnected);
            this.groupBox1.Controls.Add(this.shapeConnected);
            this.groupBox1.Controls.Add(this.cbConnectTo);
            this.groupBox1.Controls.Add(this._progressReceive);
            this.groupBox1.Location = new System.Drawing.Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(480, 72);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Satellite Site Status";
            // 
            // lblRecv
            // 
            this.lblRecv.Location = new System.Drawing.Point(368, 16);
            this.lblRecv.Name = "lblRecv";
            this.lblRecv.Size = new System.Drawing.Size(32, 18);
            this.lblRecv.TabIndex = 41;
            this.lblRecv.Text = "Recv";
            // 
            // shapeRecv
            // 
            this.shapeRecv.BackColor = System.Drawing.SystemColors.Control;
            this.shapeRecv.BorderColor = System.Drawing.Color.Black;
            this.shapeRecv.BorderStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.shapeRecv.BorderWidth = 1;
            this.shapeRecv.CenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.shapeRecv.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.shapeRecv.Location = new System.Drawing.Point(352, 16);
            this.shapeRecv.Name = "shapeRecv";
            this.shapeRecv.Shape = ShapeControl.ShapeType.Ellipse;
            this.shapeRecv.Size = new System.Drawing.Size(16, 16);
            this.shapeRecv.SurroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.shapeRecv.TabIndex = 40;
            this.shapeRecv.UseGradient = false;
            // 
            // lblSend
            // 
            this.lblSend.Location = new System.Drawing.Point(312, 16);
            this.lblSend.Name = "lblSend";
            this.lblSend.Size = new System.Drawing.Size(32, 18);
            this.lblSend.TabIndex = 39;
            this.lblSend.Text = "Send";
            // 
            // shapeSend
            // 
            this.shapeSend.BackColor = System.Drawing.SystemColors.Control;
            this.shapeSend.BorderColor = System.Drawing.Color.Black;
            this.shapeSend.BorderStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.shapeSend.BorderWidth = 1;
            this.shapeSend.CenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.shapeSend.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.shapeSend.Location = new System.Drawing.Point(288, 16);
            this.shapeSend.Name = "shapeSend";
            this.shapeSend.Shape = ShapeControl.ShapeType.Ellipse;
            this.shapeSend.Size = new System.Drawing.Size(16, 16);
            this.shapeSend.SurroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.shapeSend.TabIndex = 38;
            this.shapeSend.UseGradient = false;
            // 
            // lblConnected
            // 
            this.lblConnected.Location = new System.Drawing.Point(224, 16);
            this.lblConnected.Name = "lblConnected";
            this.lblConnected.Size = new System.Drawing.Size(64, 18);
            this.lblConnected.TabIndex = 37;
            this.lblConnected.Text = "Connected";
            // 
            // shapeConnected
            // 
            this.shapeConnected.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.shapeConnected.BorderColor = System.Drawing.Color.Black;
            this.shapeConnected.BorderStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.shapeConnected.BorderWidth = 1;
            this.shapeConnected.CenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.shapeConnected.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.shapeConnected.Location = new System.Drawing.Point(200, 16);
            this.shapeConnected.Name = "shapeConnected";
            this.shapeConnected.Shape = ShapeControl.ShapeType.Ellipse;
            this.shapeConnected.Size = new System.Drawing.Size(16, 16);
            this.shapeConnected.SurroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.shapeConnected.TabIndex = 36;
            this.shapeConnected.UseGradient = false;
            this.shapeConnected.Click += new System.EventHandler(this.shapeControl17_Click);
            // 
            // _progressReceive
            // 
            this._progressReceive.Location = new System.Drawing.Point(205, 40);
            this._progressReceive.Name = "_progressReceive";
            this._progressReceive.Size = new System.Drawing.Size(136, 16);
            this._progressReceive.TabIndex = 42;
            // 
            // txtCommandRemaining
            // 
            this.txtCommandRemaining.Location = new System.Drawing.Point(368, 48);
            this.txtCommandRemaining.Name = "txtCommandRemaining";
            this.txtCommandRemaining.Size = new System.Drawing.Size(59, 20);
            this.txtCommandRemaining.TabIndex = 43;
            this.txtCommandRemaining.Text = "00:00";
            this.txtCommandRemaining.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // mainMenu1
            // 
            this.mainMenu1.Items.AddRange(new System.Windows.Forms.ToolStripMenuItem[] {
            this.menuItem1,
            this.menuConfiguration,
            this.menuItem5});
            // 
            // menuItem1
            // 
            this.menuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripMenuItem[] {
            this.menuItem2});
            this.menuItem1.Text = "File";
            // 
            // menuItem2
            // 
            this.menuItem2.Text = "Exit";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // menuConfiguration
            // 
            this.menuConfiguration.DropDownItems.AddRange(new System.Windows.Forms.ToolStripMenuItem[] {
            this.mnuSkywaveAccount,
            this.mnuRemoteSiteSetup,
            this.menuVPNSetup,
            this.menuAlertSetup});
            this.menuConfiguration.Text = "Configuration";
            // 
            // mnuSkywaveAccount
            // 
            this.mnuSkywaveAccount.Text = "Skywave Account...";
            this.mnuSkywaveAccount.Click += new System.EventHandler(this.mnuSkywaveAccount_Click);
            // 
            // mnuRemoteSiteSetup
            // 
            this.mnuRemoteSiteSetup.Text = "Remote Site Setup...";
            this.mnuRemoteSiteSetup.Click += new System.EventHandler(this.mnuRemoteSiteSetup_Click);
            // 
            // menuVPNSetup
            // 
            this.menuVPNSetup.Text = "VPN Setup...";
            this.menuVPNSetup.Visible = false;
            this.menuVPNSetup.Click += new System.EventHandler(this.menu_VPNSetupClick);
            // 
            // menuAlertSetup
            // 
            this.menuAlertSetup.Text = "Alert Setup...";
            this.menuAlertSetup.Click += new System.EventHandler(this.menuAlertSetup_Click);
            // 
            // menuItem5
            // 
            this.menuItem5.DropDownItems.AddRange(new System.Windows.Forms.ToolStripMenuItem[] {
            this.menuItemAbout});
            this.menuItem5.Text = "Help";
            // 
            // menuItemAbout
            // 
            this.menuItemAbout.Text = "About WMI Ground Control Interface...";
            this.menuItemAbout.Click += new System.EventHandler(this.menuItemAbout_Click);
            // 
            // lvStatus
            // 
            this.lvStatus.BackColor = System.Drawing.Color.White;
            this.lvStatus.GridLines = true;
            this.lvStatus.Location = new System.Drawing.Point(16, 104);
            this.lvStatus.Name = "lvStatus";
            this.lvStatus.Size = new System.Drawing.Size(232, 240);
            this.lvStatus.TabIndex = 5;
            this.lvStatus.UseCompatibleStateImageBehavior = false;
            this.lvStatus.View = System.Windows.Forms.View.Details;
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(8, 88);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(248, 272);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Parameter Status";
            // 
            // txtMasterTime
            // 
            this.txtMasterTime.Location = new System.Drawing.Point(496, 48);
            this.txtMasterTime.Name = "txtMasterTime";
            this.txtMasterTime.Size = new System.Drawing.Size(112, 20);
            this.txtMasterTime.TabIndex = 7;
            // 
            // btnSyncClock
            // 
            this.btnSyncClock.Enabled = false;
            this.btnSyncClock.Location = new System.Drawing.Point(632, 48);
            this.btnSyncClock.Name = "btnSyncClock";
            this.btnSyncClock.Size = new System.Drawing.Size(72, 24);
            this.btnSyncClock.TabIndex = 8;
            this.btnSyncClock.Text = "Sync Clock";
            // 
            // lblCurrentTime
            // 
            this.lblCurrentTime.Location = new System.Drawing.Point(496, 24);
            this.lblCurrentTime.Name = "lblCurrentTime";
            this.lblCurrentTime.Size = new System.Drawing.Size(72, 16);
            this.lblCurrentTime.TabIndex = 9;
            this.lblCurrentTime.Text = "Master Time:";
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(24, 376);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 10;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.pictureBox4);
            this.groupBox3.Controls.Add(this.pictureBox3);
            this.groupBox3.Controls.Add(this.pictureBox2);
            this.groupBox3.Controls.Add(this.btnNozzle);
            this.groupBox3.Controls.Add(this.btnIgnition);
            this.groupBox3.Controls.Add(this.btnPropane);
            this.groupBox3.Controls.Add(this.btnPurge);
            this.groupBox3.Controls.Add(this.btnSolution);
            this.groupBox3.Controls.Add(this.pictureBox5);
            this.groupBox3.Location = new System.Drawing.Point(272, 488);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(528, 48);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Manual Operation";
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(314, 16);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(24, 16);
            this.pictureBox4.TabIndex = 7;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(205, 16);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(24, 16);
            this.pictureBox3.TabIndex = 6;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(96, 16);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(24, 16);
            this.pictureBox2.TabIndex = 5;
            this.pictureBox2.TabStop = false;
            // 
            // btnNozzle
            // 
            this.btnNozzle.Location = new System.Drawing.Point(232, 16);
            this.btnNozzle.Name = "btnNozzle";
            this.btnNozzle.Size = new System.Drawing.Size(75, 23);
            this.btnNozzle.TabIndex = 4;
            this.btnNozzle.Text = "Nozzle";
            this.btnNozzle.Click += new System.EventHandler(this.btnNozzle_Click);
            // 
            // btnIgnition
            // 
            this.btnIgnition.Location = new System.Drawing.Point(124, 16);
            this.btnIgnition.Name = "btnIgnition";
            this.btnIgnition.Size = new System.Drawing.Size(75, 23);
            this.btnIgnition.TabIndex = 3;
            this.btnIgnition.Text = "Ignition";
            this.btnIgnition.Click += new System.EventHandler(this.btnIgnition_Click);
            // 
            // btnPropane
            // 
            this.btnPropane.Location = new System.Drawing.Point(16, 16);
            this.btnPropane.Name = "btnPropane";
            this.btnPropane.Size = new System.Drawing.Size(75, 23);
            this.btnPropane.TabIndex = 2;
            this.btnPropane.Text = "Propane";
            this.btnPropane.Click += new System.EventHandler(this.btnPropane_Click);
            // 
            // btnPurge
            // 
            this.btnPurge.Location = new System.Drawing.Point(448, 16);
            this.btnPurge.Name = "btnPurge";
            this.btnPurge.Size = new System.Drawing.Size(75, 23);
            this.btnPurge.TabIndex = 1;
            this.btnPurge.Text = "Purge";
            this.btnPurge.Click += new System.EventHandler(this.btnPurge_Click);
            // 
            // btnSolution
            // 
            this.btnSolution.Location = new System.Drawing.Point(340, 16);
            this.btnSolution.Name = "btnSolution";
            this.btnSolution.Size = new System.Drawing.Size(75, 23);
            this.btnSolution.TabIndex = 0;
            this.btnSolution.Text = "Solution";
            this.btnSolution.Click += new System.EventHandler(this.btnSolution_Click);
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox5.Image")));
            this.pictureBox5.Location = new System.Drawing.Point(423, 16);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(24, 16);
            this.pictureBox5.TabIndex = 16;
            this.pictureBox5.TabStop = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnEndSeeding);
            this.groupBox4.Controls.Add(this.btnStartSeed);
            this.groupBox4.Location = new System.Drawing.Point(16, 488);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(240, 48);
            this.groupBox4.TabIndex = 12;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Auto Sequence";
            // 
            // btnEndSeeding
            // 
            this.btnEndSeeding.Location = new System.Drawing.Point(128, 16);
            this.btnEndSeeding.Name = "btnEndSeeding";
            this.btnEndSeeding.Size = new System.Drawing.Size(88, 23);
            this.btnEndSeeding.TabIndex = 5;
            this.btnEndSeeding.Text = "End Seeding";
            this.btnEndSeeding.Click += new System.EventHandler(this.btnEndSeeding_Click);
            // 
            // btnStartSeed
            // 
            this.btnStartSeed.Location = new System.Drawing.Point(16, 16);
            this.btnStartSeed.Name = "btnStartSeed";
            this.btnStartSeed.Size = new System.Drawing.Size(88, 23);
            this.btnStartSeed.TabIndex = 4;
            this.btnStartSeed.Text = "Start Seeding";
            this.btnStartSeed.Click += new System.EventHandler(this.btnStartSeed_Click);
            // 
            // btnResetSeed
            // 
            this.btnResetSeed.Location = new System.Drawing.Point(136, 376);
            this.btnResetSeed.Name = "btnResetSeed";
            this.btnResetSeed.Size = new System.Drawing.Size(112, 23);
            this.btnResetSeed.TabIndex = 13;
            this.btnResetSeed.Text = "Reset Seed Timer";
            this.btnResetSeed.Click += new System.EventHandler(this.btnResetSeed_Click);
            // 
            // lnkWMI
            // 
            this.lnkWMI.Location = new System.Drawing.Point(360, 544);
            this.lnkWMI.Name = "lnkWMI";
            this.lnkWMI.Size = new System.Drawing.Size(152, 23);
            this.lnkWMI.TabIndex = 16;
            this.lnkWMI.TabStop = true;
            this.lnkWMI.Text = "Weather Modification, Inc.";
            this.lnkWMI.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkWMI_Clicked);
            // 
            // timerMasterTime
            // 
            this.timerMasterTime.Interval = 1000;
            this.timerMasterTime.Tick += new System.EventHandler(this.onTick_MasterTime);
            // 
            // timerKeepAlive
            // 
            this.timerKeepAlive.Interval = 30000;
            this.timerKeepAlive.Tick += new System.EventHandler(this.onTick_TimerKeepAlive);
            // 
            // frmMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(824, 566);
            this.Controls.Add(this.lnkWMI);
            this.Controls.Add(this.txtMasterTime);
            this.Controls.Add(this.txtCommandRemaining);
            this.Controls.Add(this.btnResetSeed);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.lblCurrentTime);
            this.Controls.Add(this.btnSyncClock);
            this.Controls.Add(this.lvStatus);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tabRemote);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainMenu1;
            this.Name = "frmMain";
            this.Text = "WMI Remote Ground Generator Control Interface ";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.frmMain_Close);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.tabRemote.ResumeLayout(false);
            this.tabPageSystemStatus.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictBurning)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictBattery)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictIgnition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictPurge)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictSolution)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictPropane)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabLog.ResumeLayout(false);
            this.tabPageWeather.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new frmMain());
		}

		private void mnuRemoteSiteSetup_Click(object sender, System.EventArgs e)
		{

			frmRemoteSiteSetup siteSetup = new frmRemoteSiteSetup();
			siteSetup.ShowDialog();
			populateSites();
		
		}

		private void shapeControl17_Click(object sender, System.EventArgs e)
		{
		
		}

		private void connectTo_IndexChanged(object sender, System.EventArgs e)
		{

			if ( cbConnectTo.SelectedItem == null )
			{
				return;
			}
			

			try
			{

				//	Indicate that we are disconnected (if we aren't, we will be);
				connected = false;

				//	If the user asked for a disconnect, disconnect
				if ( cbConnectTo.SelectedIndex == 0 )
				{
				    communicationControlState(true);

				}
				else
				{

                    _eventLog.initNewLog(cbConnectTo.Text);
                    _weatherLog.initNewLog(cbConnectTo.Text);

					//	Log command
					LogEntry entry = new LogEntry(cbConnectTo.Text,"Connected","Connected To Site" );
					_eventLog.addEntry(ref entry);
					updateErrorLog();

					communicationControlState(false);
					Site siteInfo = ((Site) cbConnectTo.SelectedItem);
                    _globalConfiguration.configuration.ConnectedSite = siteInfo;

                    //  Complete site solection process
                    SiteSelectedCommand();
                    
                        
				}
			}
			catch(Exception ex)
			{
				String errorString = "Unable to connect to " + ((Site) cbConnectTo.SelectedItem).name + "\n" + 
									 ex.Message; 
				MessageBox.Show(this,errorString,"Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
				communicationControlState(true);

			}

		
		}

		/*
		 *	connectDisable
		 * 
		 *  This method disables controls used during connections.
		 */

		private void communicationControlState(bool enabled)
		{

			if ( enabled )
			{
				commandEndTime = System.DateTime.MinValue;
			}

			cbConnectTo.Enabled		= enabled;
			btnUpdate.Enabled		= enabled;
			btnStartSeed.Enabled	= enabled;
			btnEndSeeding.Enabled	= enabled;
			btnPropane.Enabled		= enabled;
			btnIgnition.Enabled		= enabled;
			btnNozzle.Enabled		= enabled;
			btnSolution.Enabled		= enabled;
			btnPurge.Enabled		= enabled;
			btnReset.Enabled		= enabled;
			btnUpdateWx.Enabled		= enabled;

			//menuConfiguration.Enabled =	enabled;

		}

/*
		private void button1_Click(object sender, System.EventArgs e)
		{


			IPEndPoint remoteIPEndPoint = new IPEndPoint(IPAddress.Parse("10.128.16.66"), 9000); 
			//EndPoint senderRemote = (EndPoint) new IPEndPoint(IPAddress.Any, 0); 
			EndPoint remoteEndPoint = (EndPoint) remoteIPEndPoint; 

			IPEndPoint localIPEndPoint = new IPEndPoint(IPAddress.Parse("10.13.100.50"), 9100); 
			EndPoint localEndPoint = (EndPoint) localIPEndPoint; 


			Socket sendSock = new Socket(AddressFamily.InterNetwork,SocketType.Dgram, ProtocolType.Udp);

			Socket recvSock = new Socket(AddressFamily.InterNetwork,SocketType.Dgram, ProtocolType.Udp);
			recvSock.SetSocketOption( SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 2000 );
			recvSock.Bind(localEndPoint);

			// Sends a message to the host to which you have connected.
			Byte[] sendBytes = Encoding.ASCII.GetBytes("7");

			sendSock.SendTo(sendBytes,remoteEndPoint);


			Byte[] receiveBytes = new Byte[50];

			try
			{
				recvSock.ReceiveFrom(receiveBytes,50,SocketFlags.None,ref localEndPoint);
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
			}


			sendSock.Close();
			recvSock.Close();
		
  
		}
		private void button1_Click(object sender, System.EventArgs e)
		{


			IPEndPoint remoteIPEndPoint = new IPEndPoint(IPAddress.Parse("10.128.16.65"), 9000); 
			//EndPoint senderRemote = (EndPoint) new IPEndPoint(IPAddress.Any, 0); 
			EndPoint remoteEndPoint = (EndPoint) remoteIPEndPoint; 

			Socket sock = new Socket(AddressFamily.InterNetwork,SocketType.Stream, ProtocolType.Tcp);
			//sock.SetSocketOption( SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 2000 );
			sock.Connect(remoteEndPoint);

			// Sends a message to the host to which you have connected.
			Byte[] sendBytes = Encoding.ASCII.GetBytes("7");

			sock.Send(sendBytes);

			//			Socket recvSock = new Socket(AddressFamily.InterNetwork,SocketType.Dgram, ProtocolType.Udp);

			Byte[] receiveBytes = new Byte[50];

			try
			{
				sock.Receive(receiveBytes);
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
			}


			sock.Close();
		
  
		}
*/

		public void clearConnectSelection() 
		{
			cbConnectTo.SelectedIndex = -1;
		}

		public bool	connected
		{
			get
			{	
				return _connected;
			}

			set
			{	

				_connected = value;

				if ( value )
				{

                    setSiteConnected();

				}
				else
				{
                    setSiteDisconnected();

				}


			}		
		}


		public void setSendStatus( bool status ) 
		{

			if ( status )
			{
				shapeSend.BackColor = StatusColors.SENDING;;
			}
			else
			{
				shapeSend.BackColor = StatusColors.NOT_SENDING;

			}
		}

		public void setRecvStatus( bool status ) 
		{

            _receiveInProgress = status;

			if ( status )
			{
				shapeRecv.BackColor = StatusColors.RECEIVING;
			}
			else
			{
				resetProgressBar();
				shapeRecv.BackColor = StatusColors.NOT_RECEIVING;
			}
		}

		public void initProgressBar(int Max) 
		{
			//	Reset progress bar position
			_progressReceive.Value	 = 0;
			//	Set max
			_progressReceive.Maximum = _responsesReceived; 
		}

		public void resetProgressBar() 
		{
			//	Reset progress bar position
			_progressReceive.Value	 = 0;
		}

		public void incrementProgressBar() 	
		{
			_progressReceive.Increment(1);
		}


        /// <summary>
        /// Command that is executed on site selection
        /// </summary>
		public void SiteSelectedCommand() 
		{
	
			LogEntry entry = null;

            entry = new LogEntry(cbConnectTo.Text, "Connected", "Connected To Site");
			_eventLog.addEntry(ref entry);
			updateErrorLog();

			//	Set connection status to connected
			connected = true;

			setSendStatus(true);

			_responsesReceived = _remoteCommand.getStatusMessageCount(RemoteCommand.Commands.GET_STATUS);
			//	Initialize progressbar
			initProgressBar(_responsesReceived);

			//	Log command
			entry = new LogEntry(cbConnectTo.Text,RemoteCommand.Commands.GET_STATUS,_remoteCommand.getStatusMessageDescription(RemoteCommand.Commands.GET_STATUS));
			_eventLog.addEntry(ref entry);
			updateErrorLog();

            //	Send status request string
            _remoteConnection.startSendString(RemoteCommand.Commands.GET_STATUS,new AsyncCallback(ConnectSendCallback));

    	}

		public void ConnectSendCallback( IAsyncResult ar ) 
		{

			setSendStatus(false);

            SendResult sendResult = (SendResult) ar;



			//	Our command was sent, now receive the response
			if( sendResult.exceptionState == true )
			{
				//	Log command
				LogEntry entry = new LogEntry(cbConnectTo.Text,"Error","Unable to send message, 0 bytes sent" );
				_eventLog.addEntry(ref entry);
				updateErrorLog();


				MessageBox.Show("Unable to send status message to site " + ((Site) cbConnectTo.SelectedItem).name + "...",
								"Communications Error",
								MessageBoxButtons.OK,
								MessageBoxIcon.Error );
								communicationControlState(true);

				return;
			}

			setRecvStatus(true);

			//	Recieve the status comand
			//	Send status request string
			//	Get timeout for last message
			int timeoutMS = _remoteCommand.getStatusMessageTimeoutMS( _remoteConnection.lastCommand );

			_remoteConnection.startReceiveString(new AsyncCallback(ConnectReceiveCallback),timeoutMS);
			commandEndTime = System.DateTime.Now + new TimeSpan(0,
				0,
				0,
				//_remoteCommand.getStatusMessageTimeout(_remoteConnection.lastCommand) * _remoteCommand.getStatusMessageCount(_remoteConnection.lastCommand),
                _remoteCommand.getStatusMessageTimeout(_remoteConnection.lastCommand),
                0);

		}

		public void ConnectReceiveCallback( IAsyncResult ar ) 
		{

                
                //		Socket sock = (Socket) ar.AsyncState;
                //		int bytesReceived = sock.EndReceive(ar);


                //	MessageBox.Show(_remoteConnection.receiveString);

                LogEntry entry = null;

                if (((ReceiveResult)ar).exceptionState)
                {

                    //	Log command
                    entry = new LogEntry(cbConnectTo.Text, "Error", ((ReceiveResult)ar).errorString);
                    _eventLog.addEntry(ref entry);
                    updateErrorLog();

                    if (((ReceiveResult)ar).connected)
                    {
                        String warningString = "An initial connection was established with the ground generator, but the initial status message was not returned (" + ((ReceiveResult)ar).errorString + ").";

                        MessageBox.Show(this,
                            warningString,
                            "Communications Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                            );

                    }
                    else
                    {
                        String errorString = "An initial connection was established with the ground generator, but the connection was dropped (" + ((ReceiveResult)ar).errorString + ").  Reselect the site to reconnect.";

                        MessageBox.Show(this,
                            errorString,
                            "Communications Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                            );


                    }


                    setRecvStatus(false);
                    communicationControlState(true);
                    if (!((ReceiveResult)ar).connected)
                    {
                        clearConnectSelection();
                        connected = false;
                        resetProgressBar();
                    }
                    return;
                }


                _statusMessage = new StatusMessage();
                _statusMessage.parse(((ReceiveResult)ar).receiveString);
                _updateStatus = true;


                entry = new LogEntry(cbConnectTo.Text, ((ReceiveResult)ar).receiveString, "Received Status");
                _eventLog.addEntry(ref entry);
                updateErrorLog();

                /*
                            StatusMessage statusMessage = new StatusMessage();
                            statusMessage.parse(_remoteConnection.receiveString);
                            updateListView(statusMessage);
                            updateSystemsDiagram(statusMessage);
                */

                setRecvStatus(false);
                communicationControlState(true);

                //  Create weather history object
                _weatherHistoryMessage = new WeatherHistoryMessage(cbConnectTo.Text);

                //  Request weather history, ignore error message on this receive
                _IgnoreErrorHistoryMessage = true;
                if(_globalConfiguration.configuration.sites.Where(x => x.name == cbConnectTo.Text).First().checkWeather)
                    btnUpdateWxHistory_Click(this, new EventArgs());

      

		}

		private void btnUpdate_Click(object sender, System.EventArgs e)
		{
			communicationControlState(false);
			setSendStatus(true);

			_responsesReceived = _remoteCommand.getStatusMessageCount(RemoteCommand.Commands.GET_STATUS);
			//	Initialize progressbar
			initProgressBar(_responsesReceived);


			//	Log command
			LogEntry entry = new LogEntry(cbConnectTo.Text,RemoteCommand.Commands.GET_STATUS,_remoteCommand.getStatusMessageDescription(RemoteCommand.Commands.GET_STATUS));
			_eventLog.addEntry(ref entry);
			updateErrorLog();

			//	Send status request string
			_remoteConnection.startSendString(RemoteCommand.Commands.GET_STATUS,new AsyncCallback(CommandSendCallback));
			commandEndTime = System.DateTime.Now + new TimeSpan(0,
				0,
				0,
				//_remoteCommand.getStatusMessageTimeout(_remoteConnection.lastCommand) * _remoteCommand.getStatusMessageCount(_remoteConnection.lastCommand),
                _remoteCommand.getStatusMessageTimeout(_remoteConnection.lastCommand),
                0);

		}


		private void btnPropane_Click(object sender, System.EventArgs e)
		{
			communicationControlState(false);
			setSendStatus(true);
			//	Set number of responses to expect

			
			_responsesReceived = _remoteCommand.getStatusMessageCount(RemoteCommand.Commands.TOGGLE_PROPANE);
			//	Initialize progressbar
			initProgressBar(_responsesReceived);

			//	Log command
			LogEntry entry = new LogEntry(cbConnectTo.Text,RemoteCommand.Commands.TOGGLE_PROPANE,_remoteCommand.getStatusMessageDescription(RemoteCommand.Commands.TOGGLE_PROPANE));
			_eventLog.addEntry(ref entry);
			updateErrorLog();


			//	Send status request string
			_remoteConnection.startSendString(RemoteCommand.Commands.TOGGLE_PROPANE,new AsyncCallback(CommandSendCallback));
			commandEndTime = System.DateTime.Now + new TimeSpan(0,
				0,
				0,
//				_remoteCommand.getStatusMessageTimeout(_remoteConnection.lastCommand) * _remoteCommand.getStatusMessageCount(_remoteConnection.lastCommand),
                _remoteCommand.getStatusMessageTimeout(_remoteConnection.lastCommand),
				0);
		
		}



		private void btnSolution_Click(object sender, System.EventArgs e)
		{
			communicationControlState(false);
			setSendStatus(true);

			_responsesReceived = _remoteCommand.getStatusMessageCount(RemoteCommand.Commands.TOGGLE_SOLUTION);


			//	Initialize progressbar
			initProgressBar(_responsesReceived);


			//	Log command
			LogEntry entry = new LogEntry(cbConnectTo.Text,RemoteCommand.Commands.TOGGLE_SOLUTION,_remoteCommand.getStatusMessageDescription(RemoteCommand.Commands.TOGGLE_SOLUTION));
			_eventLog.addEntry(ref entry);
			updateErrorLog();


			//	Send status request string
			_remoteConnection.startSendString(RemoteCommand.Commands.TOGGLE_SOLUTION,new AsyncCallback(CommandSendCallback));
			commandEndTime = System.DateTime.Now + new TimeSpan(0,
				0,
				0,
				//_remoteCommand.getStatusMessageTimeout(_remoteConnection.lastCommand) * _remoteCommand.getStatusMessageCount(_remoteConnection.lastCommand),
                _remoteCommand.getStatusMessageTimeout(_remoteConnection.lastCommand),
 				0);
		
		}




		private void btnIgnition_Click(object sender, System.EventArgs e)
		{
			communicationControlState(false);
			setSendStatus(true);

			_responsesReceived = _remoteCommand.getStatusMessageCount(RemoteCommand.Commands.IGNITER_ON);
			//	Initialize progressbar
			initProgressBar(_responsesReceived);

			//	Log command
			LogEntry entry = new LogEntry(cbConnectTo.Text,RemoteCommand.Commands.IGNITER_ON,_remoteCommand.getStatusMessageDescription(RemoteCommand.Commands.IGNITER_ON));
			_eventLog.addEntry(ref entry);
			updateErrorLog();

			//	Send status request string
			_remoteConnection.startSendString(RemoteCommand.Commands.IGNITER_ON,new AsyncCallback(CommandSendCallback));
			commandEndTime = System.DateTime.Now + new TimeSpan(0,
				0,
				0,
				//_remoteCommand.getStatusMessageTimeout(_remoteConnection.lastCommand) * _remoteCommand.getStatusMessageCount(_remoteConnection.lastCommand),
                _remoteCommand.getStatusMessageTimeout(_remoteConnection.lastCommand),
 				0);
		
		}

		private void btnNozzle_Click(object sender, System.EventArgs e)
		{
			communicationControlState(false);
			setSendStatus(true);

			_responsesReceived = _remoteCommand.getStatusMessageCount(RemoteCommand.Commands.TOGGLE_NOZZLE);
			//	Initialize progressbar
			initProgressBar(_responsesReceived);

			//	Log command
			LogEntry entry = new LogEntry(cbConnectTo.Text,RemoteCommand.Commands.TOGGLE_NOZZLE,_remoteCommand.getStatusMessageDescription(RemoteCommand.Commands.TOGGLE_NOZZLE));
			_eventLog.addEntry(ref entry);
			updateErrorLog();

			//	Send status request string
			_remoteConnection.startSendString(RemoteCommand.Commands.TOGGLE_NOZZLE,new AsyncCallback(CommandSendCallback));
			commandEndTime = System.DateTime.Now + new TimeSpan(0,
				0,
				0,
				//_remoteCommand.getStatusMessageTimeout(_remoteConnection.lastCommand) * _remoteCommand.getStatusMessageCount(_remoteConnection.lastCommand),
                _remoteCommand.getStatusMessageTimeout(_remoteConnection.lastCommand),
 				0);
	
		}

		private void btnPurge_Click(object sender, System.EventArgs e)
		{
			communicationControlState(false);
			setSendStatus(true);

			_responsesReceived = _remoteCommand.getStatusMessageCount(RemoteCommand.Commands.TOGGLE_PURGE);
			//	Initialize progressbar
			initProgressBar(_responsesReceived);

			//	Log command
			LogEntry entry = new LogEntry(cbConnectTo.Text,RemoteCommand.Commands.TOGGLE_PURGE,_remoteCommand.getStatusMessageDescription(RemoteCommand.Commands.TOGGLE_PURGE));
			_eventLog.addEntry(ref entry);
			updateErrorLog();

			//	Send status request string
			_remoteConnection.startSendString(RemoteCommand.Commands.TOGGLE_PURGE,new AsyncCallback(CommandSendCallback));
			commandEndTime = System.DateTime.Now + new TimeSpan(0,
				0,
				0,
				//_remoteCommand.getStatusMessageTimeout(_remoteConnection.lastCommand) * _remoteCommand.getStatusMessageCount(_remoteConnection.lastCommand),
                _remoteCommand.getStatusMessageTimeout(_remoteConnection.lastCommand),
                 0);
		
		}

		private void btnStartSeed_Click(object sender, System.EventArgs e)
		{
			communicationControlState(false);
			setSendStatus(true);
			_responsesReceived = _remoteCommand.getStatusMessageCount(RemoteCommand.Commands.START_SEEDING);
			//	Initialize progressbar
			initProgressBar(_responsesReceived);

			//	Log command
			LogEntry entry = new LogEntry(cbConnectTo.Text,RemoteCommand.Commands.START_SEEDING,_remoteCommand.getStatusMessageDescription(RemoteCommand.Commands.START_SEEDING));
			_eventLog.addEntry(ref entry);
			updateErrorLog();

			//	Send status request string
			_remoteConnection.startSendString(RemoteCommand.Commands.START_SEEDING,new AsyncCallback(CommandSendCallback));
			commandEndTime = System.DateTime.Now + new TimeSpan(0,
				0,
				0,
				//_remoteCommand.getStatusMessageTimeout(_remoteConnection.lastCommand) * _remoteCommand.getStatusMessageCount(_remoteConnection.lastCommand),
                _remoteCommand.getStatusMessageTimeout(_remoteConnection.lastCommand),
                 0);
		
		}

		private void btnEndSeeding_Click(object sender, System.EventArgs e)
		{
			communicationControlState(false);
			setSendStatus(true);
			_responsesReceived = _remoteCommand.getStatusMessageCount(RemoteCommand.Commands.END_SEEDING);
			//	Initialize progressbar
			initProgressBar(_responsesReceived);

			//	Log command
			LogEntry entry = new LogEntry(cbConnectTo.Text,RemoteCommand.Commands.END_SEEDING,_remoteCommand.getStatusMessageDescription(RemoteCommand.Commands.END_SEEDING));
			_eventLog.addEntry(ref entry);
			updateErrorLog();

			//	Send status request string
			_remoteConnection.startSendString(RemoteCommand.Commands.END_SEEDING,new AsyncCallback(CommandSendCallback));
			commandEndTime = System.DateTime.Now + new TimeSpan(0,
				0,
				0,
				//_remoteCommand.getStatusMessageTimeout(_remoteConnection.lastCommand) * _remoteCommand.getStatusMessageCount(_remoteConnection.lastCommand),
                _remoteCommand.getStatusMessageTimeout(_remoteConnection.lastCommand),
 				0);
		
		}

		private void btnReset_Click(object sender, System.EventArgs e)
		{
			communicationControlState(false);
			setSendStatus(true);

			_responsesReceived = _remoteCommand.getStatusMessageCount(RemoteCommand.Commands.CLOSE_ALL);
			//	Initialize progressbar
			initProgressBar(_responsesReceived);

			//	Log command
			LogEntry entry = new LogEntry(cbConnectTo.Text,RemoteCommand.Commands.CLOSE_ALL,_remoteCommand.getStatusMessageDescription(RemoteCommand.Commands.CLOSE_ALL));
			_eventLog.addEntry(ref entry);
			updateErrorLog();

			//	Send close all request string
			_remoteConnection.startSendString(RemoteCommand.Commands.CLOSE_ALL,new AsyncCallback(CommandSendCallback));
			commandEndTime = System.DateTime.Now + new TimeSpan(0,
				0,
				0,
				//_remoteCommand.getStatusMessageTimeout(_remoteConnection.lastCommand) * _remoteCommand.getStatusMessageCount(_remoteConnection.lastCommand),
                _remoteCommand.getStatusMessageTimeout(_remoteConnection.lastCommand),
                 0);

		}
        
		public void CommandSendCallback( IAsyncResult ar ) 
		{

			setSendStatus(false);

            SendResult sendResult = (SendResult) ar;

			//	Our command was sent, now receive the response
			if(sendResult.exceptionState == true)
			{
		

				MessageBox.Show("Unable to send message to site " + ((Site) cbConnectTo.SelectedItem).name + "...",
					"Communications Error",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error );
				communicationControlState(false);

				return;
			}

			//	If we don't need any responses, we can return without waiting
			if ( _responsesReceived <= 0 )
			{
				communicationControlState(true);
				return;
			}

			//	Indicate we are receiving
			setRecvStatus(true);

			//	Get timeout for last message
			int timeoutMS = _remoteCommand.getStatusMessageTimeoutMS( _remoteConnection.lastCommand );


			_remoteConnection.startReceiveString(new AsyncCallback(CommandReceiveCallback),
												 timeoutMS);




		}

		public void CommandReceiveCallback( IAsyncResult ar ) 
		{

//			Socket sock = (Socket) ar.AsyncState;
//			int bytesReceive = sock.EndReceive(ar);


			if ( ((ReceiveResult) ar).exceptionState )
			{

				//	Log command
				LogEntry entry = new LogEntry(cbConnectTo.Text,"Error",((ReceiveResult) ar).errorString );
				_eventLog.addEntry(ref entry);
				updateErrorLog();

/*
				MessageBox.Show(this,
					((ReceiveResult) ar).errorString,
					"Communications Error",			
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
					);
*/


				if ( ((ReceiveResult) ar).connected )
				{
					String warningString = "The ground generator did not return a response for the command (" + ((ReceiveResult) ar).errorString + ").";
								
					MessageBox.Show(this,
						warningString,
						"Communications Error",			
						MessageBoxButtons.OK,
						MessageBoxIcon.Warning
						);
																																										
				}
				else
				{
					String errorString = "The connection to the ground generator was dropped before the command was received (" + ((ReceiveResult) ar).errorString + ").  Reselect the site to reconnect.";
								
					MessageBox.Show(this,
						errorString,
						"Communications Error",			
						MessageBoxButtons.OK,
						MessageBoxIcon.Error
						);

				}



				setRecvStatus(false);
				communicationControlState(true);

				if ( !((ReceiveResult) ar).connected )
				{
					clearConnectSelection();
					connected = false;
					resetProgressBar();
				}
				return;
			}

			//	Our command was sent, now receive the response
			if( ((ReceiveResult) ar).bytesReceived <= 0 )
			{
		
				//	Log command
				LogEntry entry = new LogEntry(cbConnectTo.Text,"Error","Unable to receive message from site (no characters sent)" );
				_eventLog.addEntry(ref entry);
				updateErrorLog();



				MessageBox.Show("Unable to receive message from site " + ((Site) cbConnectTo.SelectedItem).name + "...",
					"Communications Error",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error );


				setRecvStatus(false);
				communicationControlState(true);
				cbConnectTo.SelectedIndex = 0;
				resetProgressBar();

				return;
			}


			processCommand((ReceiveResult) ar);


		}

		public void keepAliveCallback( IAsyncResult ar ) 
		{


			Socket sock = (Socket) ar.AsyncState;
			int bytesSend = sock.EndSend(ar);
			_keepAliveInProgress = false;

		}



		public void processCommand(ReceiveResult rr) 
		{
            List<String> receivedStrings = rr.receiveString.Split('\r').Where(x => !String.IsNullOrEmpty(x)).ToList();
            _remoteConnection.updateGatewayTime(rr.nextGatewayTime);
            foreach (String recStr in receivedStrings)
            {
                //	Check if the last command was an error, if so, display it
                int typeMessage = MessageTypes.getMessageType(recStr);
                //	Indicate that we've received a new response
                _responsesReceived--;
                //	Update progress bar showing status
                incrementProgressBar();

                if (typeMessage == MessageTypes.MessageType.INFORMATIONAL)
                {
                    //	At present no action is required, this message is ignored

                }
                else
                if (typeMessage == MessageTypes.MessageType.ERROR)
                {

                    //	Log command
                    LogEntry entry = new LogEntry(cbConnectTo.Text, "Error", recStr);
                    _eventLog.addEntry(ref entry);
                    updateErrorLog();


                    setRecvStatus(false);
                    //	Communication control state is reset by the timer
                    communicationControlState(true);

                    MessageBox.Show(this,
                                    recStr,
                                    "Error Message From Remote Site",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error
                                    );

                    return;
                }
                else
                if (_remoteConnection.lastCommand == RemoteCommand.Commands.GET_WEATHER)
                {

                    //	Log command
                    LogEntry entry = new LogEntry(cbConnectTo.Text, recStr, "Received Weather");
                    _eventLog.addEntry(ref entry);
                    updateErrorLog();



                    if (WeatherMessage.isWeatherAvailable(recStr))
                    {
                        //  Create weather message parser
                        WeatherMessage weatherMessage = new WeatherMessage(cbConnectTo.Text);
                        weatherMessage.parse(recStr);
                        _weatherHistoryMessage.AddWeatherMessage(weatherMessage);

                        _weatherLog.addEntry(ref weatherMessage);

                        updateWeatherLog();
                    }
                    else
                    {

                        MessageBox.Show(this,
                                        "Weather information is currently unavailable.",
                                        "Weather Request Information",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);

                    }

                }
                else
                if (_remoteConnection.lastCommand == RemoteCommand.Commands.GET_WEATHER_HISTORY)
                {


                    //	Log command
                    LogEntry entry = new LogEntry(cbConnectTo.Text, @"n/a", "Received Weather History");
                    _eventLog.addEntry(ref entry);
                    updateErrorLog();


                    if (WeatherMessage.isWeatherAvailable(recStr))
                    {


                        _weatherHistoryMessage.parse(recStr);

                        _weatherLog.addEntries(ref _weatherHistoryMessage);

                        updateWeatherLog();
                    }
                    else
                    {

                        if (!_IgnoreErrorHistoryMessage)
                        {
                            MessageBox.Show(this,
                                            "Weather history is currently unavailable.",
                                            "Weather Request Information",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Information
                                            );
                        }
                    }

                    _IgnoreErrorHistoryMessage = false;

                }
                else
                if (typeMessage != MessageTypes.MessageType.STATUS)
                {

                    //	Log command
                    LogEntry entry = new LogEntry(cbConnectTo.Text, "Error", "Unknown Message Received");
                    _eventLog.addEntry(ref entry);
                    updateErrorLog();


                    setRecvStatus(false);
                    //	Communication control state is reset by the timer
                    communicationControlState(true);

                    MessageBox.Show(this,
                                    "An unknown message was received from the remote site.",
                                    "Unkown Message From Site",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error
                                   );
                    return;

                }
                else
                if (_remoteConnection.lastCommand == RemoteCommand.Commands.GET_STATUS ||
                    _remoteConnection.lastCommand == RemoteCommand.Commands.TOGGLE_PROPANE ||
                    _remoteConnection.lastCommand == RemoteCommand.Commands.TOGGLE_NOZZLE ||
                    _remoteConnection.lastCommand == RemoteCommand.Commands.TOGGLE_SOLUTION ||
                    _remoteConnection.lastCommand == RemoteCommand.Commands.IGNITER_ON ||
                    _remoteConnection.lastCommand == RemoteCommand.Commands.TOGGLE_PURGE ||
                    _remoteConnection.lastCommand == RemoteCommand.Commands.START_SEEDING ||
                    _remoteConnection.lastCommand == RemoteCommand.Commands.END_SEEDING
                    )
                {

                    //	Log command
                    LogEntry entry = new LogEntry(cbConnectTo.Text, recStr, "Received Status");
                    _eventLog.addEntry(ref entry);
                    updateErrorLog();


                    _statusMessage = new StatusMessage();
                    if (_remoteConnection.lastCommand == RemoteCommand.Commands.START_SEEDING)
                    {
                        int debug = 1;
                    }
                    _statusMessage.parse(recStr);
                    _updateStatus = true;
                }
                else
                if (_remoteConnection.lastCommand == RemoteCommand.Commands.CLOSE_ALL)
                {

                    //	Log command
                    LogEntry entry = new LogEntry(cbConnectTo.Text, recStr, "Received Status");
                    _eventLog.addEntry(ref entry);
                    updateErrorLog();



                    _statusMessage = new StatusMessage();
                    _statusMessage.parse(recStr);

                    //	Set last reset time
                    _lastResetTime = System.DateTime.Now;
                    _updateStatus = true;

                }
            }
            if (_responsesReceived > 0)
            {
                //	Get timeout for last message
                int timeout = _remoteCommand.getStatusMessageTimeoutMS(_remoteConnection.lastCommand);
                _remoteConnection.startReceiveString(new AsyncCallback(CommandReceiveCallback), timeout);
            }
            else
            {
                setRecvStatus(false);
                //	Communication control state is reset by the timer
                communicationControlState(true);
            }

        }

		private void updateCommandRemainingTime()
		{
			if ( commandEndTime.CompareTo(System.DateTime.MinValue ) != 0 )
			{
				//	Get timespan
				System.TimeSpan remainingTime = commandEndTime - System.DateTime.Now;

				String strRemaining =  String.Format("{0:D2}:{1:D2}",
													  remainingTime.Minutes,
													  remainingTime.Seconds
													 );

				txtCommandRemaining.Text = strRemaining;

			}
			else
			{


				txtCommandRemaining.Text = "00:00";
			}

		}

		private void onTick_MasterTime(object sender, System.EventArgs e)
		{
			if ( _updateStatus )
			{
				_updateStatus = false;
				updateListView(ref _statusMessage);
				updateSystemsDiagram(ref _statusMessage);

				if ( _statusMessage.seedingStatus && _seedStartTime.CompareTo(System.DateTime.MinValue) == 0  )
				{
					_seedStartTime = System.DateTime.Now;
				}
				else
				{	
					_seedStartTime = System.DateTime.MinValue;
				}



			}


			if ( connected )
			{
				if ( _seedStartTime.CompareTo(System.DateTime.MinValue) == 0 )
				{
					lvStatus.Items[StatusListIndexes.SEED_TIME].SubItems[DATA_COLULMN].Text				= "00:00:00";
				}
				else
				{
					TimeSpan seedingTime = System.DateTime.Now - _seedStartTime;
					String strSeedingTime =  String.Format("{00}:{00}:{00}",
						seedingTime.TotalHours,
						seedingTime.TotalMinutes,
						seedingTime.TotalSeconds
						);
				}


			
				updateCommandRemainingTime();

			}

			txtMasterTime.Text = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
		}

		private void menuItem2_Click(object sender, System.EventArgs e)
		{
            if (!_receiveInProgress)
            {
                Application.Exit();
            }
		}

		private void btnResetSeed_Click(object sender, System.EventArgs e)
		{
			_seedStartTime = System.DateTime.MinValue;
		}

		private void trackBar1_Scroll(object sender, System.EventArgs e)
		{
		
		}

		private void menuItemAbout_Click(object sender, System.EventArgs e)
		{
			frmAbout about = new frmAbout();
			about.ShowDialog(this);
		}

		private void linkWMI_Clicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{

			// Determine which link was clicked within the LinkLabel.
			lnkWMI.Links[lnkWMI.Links.IndexOf(e.Link)].Visited = true;

			// Display the appropriate link based on the value of the 
			// LinkData property of the Link object.
			string target = e.Link.LinkData as string;

			// If the value looks like a URL, navigate to it.
			// Otherwise, display it in a message box.
			if(null != target && target.StartsWith("www"))
			{
				System.Diagnostics.Process.Start(target);
			}
			else
			{    
				MessageBox.Show("Item clicked: " + target);
			}

		
		}


		private void frmMain_Load(object sender, System.EventArgs e)
		{

	/*
			if ( !VPNConnect.connectTo(_globalConfiguration.configuration.VPNConnectInfo.connectionName,
				_globalConfiguration.configuration.VPNConnectInfo.userName,
				_globalConfiguration.configuration.VPNConnectInfo.password) )
			{
				MessageBox.Show( "Unable to connect to VPN, please check the VPN configuration...");
			}
*/

			//	Load battery voltage fonts
			_normalBatteryVoltageFont = txtBatteryVoltage.Font;
			_lowBatteryVoltageFont	  = new Font(txtBatteryVoltage.Font, FontStyle.Bold);

		}

		private void menu_VPNSetupClick(object sender, System.EventArgs e)
		{
            /*
			frmVPNSetup vpnSetup = new frmVPNSetup();
			vpnSetup.ShowDialog();	
			vpnSetup.ShowDialog();
            */
		}

		private void menuAlertSetup_Click(object sender, System.EventArgs e)
		{
			frmAlerts alertSetup = new frmAlerts();
			alertSetup.ShowDialog();
		}

		private void tabRemote_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}

		private void btnLogCleared_Click(object sender, System.EventArgs e)
		{
            if (cbConnectTo.Text != "")
            {
                _eventLog.initNewLog(cbConnectTo.Text);
                updateErrorLog();
            }
		}

		private void btnFolder_Click(object sender, System.EventArgs e)
		{
			// Start explorer, open to our directory path
			Process.Start("explorer.exe",_eventLog.logDirectoryPath );

		}

		private void btnUpdateWx_Click(object sender, System.EventArgs e)
		{
		
			communicationControlState(false);
			setSendStatus(true);

			_responsesReceived = _remoteCommand.getStatusMessageCount(RemoteCommand.Commands.GET_WEATHER);
			//	Initialize progressbar
			initProgressBar(_responsesReceived);


			//	Log command
			LogEntry entry = new LogEntry(cbConnectTo.Text,RemoteCommand.Commands.GET_WEATHER,_remoteCommand.getStatusMessageDescription(RemoteCommand.Commands.GET_WEATHER));
			_eventLog.addEntry(ref entry);
			updateErrorLog();

			//	Send weather request string
			_remoteConnection.startSendString(RemoteCommand.Commands.GET_WEATHER,new AsyncCallback(CommandSendCallback));
			commandEndTime = System.DateTime.Now + new TimeSpan(0,
				0,
				0,
				//_remoteCommand.getStatusMessageTimeout(_remoteConnection.lastCommand) * _remoteCommand.getStatusMessageCount(_remoteConnection.lastCommand),
                _remoteCommand.getStatusMessageTimeout(_remoteConnection.lastCommand),
                 0);

		}



        private void btnUpdateWxHistory_Click(object sender, EventArgs e)
        {


            communicationControlState(false);
            setSendStatus(true);

            _responsesReceived = _remoteCommand.getStatusMessageCount(RemoteCommand.Commands.GET_WEATHER_HISTORY);
            //	Initialize progressbar
            initProgressBar(_responsesReceived);

            //	Log command
            LogEntry entry = new LogEntry(cbConnectTo.Text, RemoteCommand.Commands.GET_WEATHER_HISTORY, _remoteCommand.getStatusMessageDescription(RemoteCommand.Commands.GET_WEATHER_HISTORY));
            _eventLog.addEntry(ref entry);
            updateErrorLog();

            //	Send weather request string
            _remoteConnection.startSendString(RemoteCommand.Commands.GET_WEATHER_HISTORY, new AsyncCallback(CommandSendCallback));
            commandEndTime = System.DateTime.Now + new TimeSpan(0,
                0,
                0,
                //_remoteCommand.getStatusMessageTimeout(_remoteConnection.lastCommand) * _remoteCommand.getStatusMessageCount(_remoteConnection.lastCommand),
                _remoteCommand.getStatusMessageTimeout(_remoteConnection.lastCommand),
                 0);

        }


		private void onTick_TimerKeepAlive(object sender, System.EventArgs e)
		{

            return;
		
			//	Send a keepalive request if a keepalive isn't already in progress
			if ( !_keepAliveInProgress && connected )
			{

				try
				{
					//	Indicate a keepalive is now in progress
					_keepAliveInProgress = true;
					//	Send a keep alive string to keep the connection open
					_remoteConnection.startSendString(RemoteCommand.Commands.KEEP_ALIVE,new AsyncCallback(keepAliveCallback));

				}
				catch(Exception)
				{
					//	Indicate a keepalive is now in progress
					_keepAliveInProgress = false;

					//	If there is an error on keepalives, ignore it
					//	we don't need to bring this to the attention of the user
				}


			}


		}

		private void frmMain_Close(object sender, System.ComponentModel.CancelEventArgs e)
		{

            if (_receiveInProgress)
            {
                //  We are still receiving don't quit
                e.Cancel = true;
            }
		}




		public System.DateTime	commandEndTime
		{
			get
			{
				lock(this)
				{
					return _commandEndTime;
				}
			}  

			set
			{
				lock(this)
				{
					_commandEndTime = value;
				}
			}
		
		}

        /// <summary>
        /// Method to obtain current site from connection combobox.  Used by thread safe delegates
        /// </summary>
        /// <returns></returns>
        private String getCurrentSite()
        {
            return cbConnectTo.Text;
        }


        /// <summary>
        /// Method to set set connection colors
        /// </summary>
        /// <returns></returns>
        private void setSiteConnected()
        {
            shapeConnected.BackColor = StatusColors.CONNECTED;

        }


        /// <summary>
        /// Method to set set connection colors
        /// </summary>
        /// <returns></returns>
        private void setSiteDisconnected()
        {

            shapeConnected.BackColor = StatusColors.DISCONNECTED; ;

            //	set default system state
            StatusMessage statusMessage = new StatusMessage();
            updateListView(ref statusMessage);
            updateSystemsDiagram(ref statusMessage);
        }

        private void btnWeatherFolder_Click(object sender, EventArgs e)
        {
            // Start explorer, open to our directory path
            Process.Start("explorer.exe", _eventLog.logDirectoryPath);
        }

        /// <summary>
        /// Menu item to configure skyware account details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuSkywaveAccount_Click(object sender, EventArgs e)
        {

            
            frmAccountSetup accountSetup = new frmAccountSetup();
            accountSetup.ShowDialog();

        }





        /*
                private void button1_Click(object sender, System.EventArgs e)
                {

                    IPHostEntry IPHost = Dns.Resolve("10.13.100.100"); 
                    string []aliases = IPHost.Aliases; 
                    IPAddress[] addr = IPHost.AddressList; 

                    IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);


                    UdpClient udpClient = new UdpClient();
                    udpClient.Connect("10.128.16.66", 9000);
                    //udpClient.Connect("10.13.100.100", 9000);

                    // Sends a message to the host to which you have connected.
                    Byte[] sendBytes = Encoding.ASCII.GetBytes("7");

                    udpClient.Send(sendBytes, sendBytes.Length);

                    UdpClient recvClient = new UdpClient();
                    Byte[] receiveBytes = recvClient.Receive(ref RemoteIpEndPoint); 

                    string returnData = Encoding.ASCII.GetString(receiveBytes);
                    MessageBox.Show(returnData);

                }

        */






    }
}
