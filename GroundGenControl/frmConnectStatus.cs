using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace GroundGenControl
{
	/// <summary>
	/// Summary description for frmConnecting.
	/// </summary>
	public class frmConnectStatus : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label lblConnecting;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private String	_connectString = "Connecting to remote site {0}, Please wait...";


		public frmConnectStatus()
		{

			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			_connectString = lblConnecting.Text;

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

		public string siteName
		{
			set
			{
				lblConnecting.Text = String.Format(_connectString,value);
			}
		}


		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.lblConnecting = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// lblConnecting
			// 
			this.lblConnecting.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblConnecting.Location = new System.Drawing.Point(8, 16);
			this.lblConnecting.Name = "lblConnecting";
			this.lblConnecting.Size = new System.Drawing.Size(528, 24);
			this.lblConnecting.TabIndex = 0;
			this.lblConnecting.Text = "Connecting to remote site {0}, Please wait...";
			// 
			// frmConnectStatus
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(562, 64);
			this.ControlBox = false;
			this.Controls.Add(this.lblConnecting);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmConnectStatus";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Connection Status";
			this.ResumeLayout(false);

		}
		#endregion
	}
}
