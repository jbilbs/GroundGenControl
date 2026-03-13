using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace GroundGenControl
{
	/// <summary>
	/// Summary description for frmAbout.
	/// </summary>
	public class frmAbout : System.Windows.Forms.Form
	{
		private System.Windows.Forms.LinkLabel lnkWMI;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmAbout()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//	Add WMI link
			// Create a new link using the Add method of the LinkCollection class.
			lnkWMI.Links.Add(0,lnkWMI.Text.Length,"www.weathermod.com/");

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
            this.lnkWMI = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lnkWMI
            // 
            this.lnkWMI.Location = new System.Drawing.Point(152, 168);
            this.lnkWMI.Name = "lnkWMI";
            this.lnkWMI.Size = new System.Drawing.Size(152, 23);
            this.lnkWMI.TabIndex = 17;
            this.lnkWMI.TabStop = true;
            this.lnkWMI.Text = "Weather Modification, Inc.";
            this.lnkWMI.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkWMI_Clicked);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(56, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(352, 23);
            this.label1.TabIndex = 18;
            this.label1.Text = "Weather Modification Ground Control Interface";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(184, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 16);
            this.label2.TabIndex = 19;
            this.label2.Text = "Version 2.01";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(104, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(240, 23);
            this.label3.TabIndex = 20;
            this.label3.Text = "Copyright © 2017-2025 Weather Modificaiton,  Inc.";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // frmAbout
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(440, 206);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lnkWMI);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAbout";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Weather Modification Ground Control Interface";
            this.ResumeLayout(false);

		}
		#endregion

		private void label3_Click(object sender, System.EventArgs e)
		{
		
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
	}
}
