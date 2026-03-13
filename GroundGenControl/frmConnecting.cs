using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;


//	Test for continues integration
namespace GroundGenControl
{
	/// <summary>
	/// Summary description for frmConnecting.
	/// </summary>
	public class frmConnecting : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label lblConnecting;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;


		public frmConnecting()
		{

			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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
			this.lblConnecting = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// lblConnecting
			// 
			this.lblConnecting.Location = new System.Drawing.Point(16, 16);
			this.lblConnecting.Name = "lblConnecting";
			this.lblConnecting.Size = new System.Drawing.Size(256, 16);
			this.lblConnecting.TabIndex = 0;
			this.lblConnecting.Text = "Connecting to remote site Fargo, Please wait...";
			// 
			// frmConnecting
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(362, 46);
			this.Controls.Add(this.lblConnecting);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "frmConnecting";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "frmConnecting";
			this.ResumeLayout(false);

		}
		#endregion
	}
}
