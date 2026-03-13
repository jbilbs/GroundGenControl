using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using GroundGenControl.Configuration;

namespace GroundGenControl
{
	/// <summary>
	/// Summary description for frmAlerts.
	/// </summary>
	public class frmAlerts : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.TextBox txtBatteryVolts;
		private System.Windows.Forms.Label label2;
		GlobalConfiguration	_globalConfiguration = GlobalConfiguration.instance;

		private	double	MINIMUM_VOLTAGE = 1.0;
		private double	MAXIMUM_VOLTAGE = 11.5;

		public frmAlerts()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//

			txtBatteryVolts.Text		=	_globalConfiguration.configuration.AlertInfo.batteryVoltage.ToString();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
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
			this.label1 = new System.Windows.Forms.Label();
			this.txtBatteryVolts = new System.Windows.Forms.TextBox();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(184, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "Low Battery Voltage Notification:";
			// 
			// txtBatteryVolts
			// 
			this.txtBatteryVolts.Location = new System.Drawing.Point(16, 48);
			this.txtBatteryVolts.MaxLength = 64;
			this.txtBatteryVolts.Name = "txtBatteryVolts";
			this.txtBatteryVolts.Size = new System.Drawing.Size(120, 20);
			this.txtBatteryVolts.TabIndex = 1;
			this.txtBatteryVolts.Text = "";
			// 
			// btnOK
			// 
			this.btnOK.Location = new System.Drawing.Point(56, 88);
			this.btnOK.Name = "btnOK";
			this.btnOK.TabIndex = 8;
			this.btnOK.Text = "OK";
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(160, 88);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.TabIndex = 9;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(160, 48);
			this.label2.Name = "label2";
			this.label2.TabIndex = 10;
			this.label2.Text = "Volts";
			// 
			// frmVPNSetup
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(288, 142);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.txtBatteryVolts);
			this.Controls.Add(this.label1);
			this.Name = "frmVPNSetup";
			this.Text = "Alerts and Notifications";
			this.ResumeLayout(false);

		}
		#endregion

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		private void btnOK_Click(object sender, System.EventArgs e)
		{
			double dblBatteryVoltage = 0;

			try
			{
				dblBatteryVoltage = Double.Parse(txtBatteryVolts.Text);
			}
			catch(Exception)
			{
			
			}


			//	Verify voltage is between 1.0 and 11.5 volts
			if (dblBatteryVoltage < MINIMUM_VOLTAGE || dblBatteryVoltage > MAXIMUM_VOLTAGE )
			{
				MessageBox.Show(this,
					"Battery low voltage warning must be between 1.0 and 11.5.",
					"Battery Voltage Invalid",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);
				return;
			}

			//	Update configuration
			saveChanges();
			this.DialogResult = DialogResult.OK;

			this.Close();


		}

		private void saveChanges()
		{

			_globalConfiguration.configuration.AlertInfo.batteryVoltage = Double.Parse(txtBatteryVolts.Text);
			_globalConfiguration.SaveConfiguration();

		}



	}
}
