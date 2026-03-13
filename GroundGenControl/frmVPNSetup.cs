using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using GroundGenControl.Configuration;

namespace GroundGenControl
{
	/// <summary>
	/// Summary description for frmVPNSetup.
	/// </summary>
	public class frmVPNSetup : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtConnectName;
		private System.Windows.Forms.TextBox txtUserName;
		private System.Windows.Forms.TextBox txtPassword;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.TextBox txtConfirmPassword;
		private System.Windows.Forms.Label label4;
		GlobalConfiguration	_globalConfiguration = GlobalConfiguration.instance;

		public frmVPNSetup()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//

            /*
			txtConnectName.Text			=	_globalConfiguration.configuration.VPNConnectInfo.connectionName;
			txtUserName.Text			=	_globalConfiguration.configuration.VPNConnectInfo.userName;
			txtPassword.Text			=	_globalConfiguration.configuration.VPNConnectInfo.password;
			txtConfirmPassword.Text		=	_globalConfiguration.configuration.VPNConnectInfo.password;
            */
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtConnectName = new System.Windows.Forms.TextBox();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtConfirmPassword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(16, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Connection Name:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(16, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Username:";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(16, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Password:";
            // 
            // txtConnectName
            // 
            this.txtConnectName.Location = new System.Drawing.Point(16, 40);
            this.txtConnectName.MaxLength = 64;
            this.txtConnectName.Name = "txtConnectName";
            this.txtConnectName.Size = new System.Drawing.Size(232, 20);
            this.txtConnectName.TabIndex = 1;
            this.txtConnectName.TextChanged += new System.EventHandler(this.txtConnectName_TextChanged);
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(16, 88);
            this.txtUserName.MaxLength = 64;
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(232, 20);
            this.txtUserName.TabIndex = 3;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(16, 136);
            this.txtPassword.MaxLength = 64;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(232, 20);
            this.txtPassword.TabIndex = 5;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(56, 224);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(160, 224);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtConfirmPassword
            // 
            this.txtConfirmPassword.Location = new System.Drawing.Point(16, 192);
            this.txtConfirmPassword.MaxLength = 64;
            this.txtConfirmPassword.Name = "txtConfirmPassword";
            this.txtConfirmPassword.PasswordChar = '*';
            this.txtConfirmPassword.Size = new System.Drawing.Size(232, 20);
            this.txtConfirmPassword.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(16, 168);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 16);
            this.label4.TabIndex = 6;
            this.label4.Text = "Confim Password:";
            // 
            // frmVPNSetup
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(288, 262);
            this.Controls.Add(this.txtConfirmPassword);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.txtConnectName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "frmVPNSetup";
            this.Text = "Satellite VPN Setup";
            this.Load += new System.EventHandler(this.frmVPNSetup_OnLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		private void btnOK_Click(object sender, System.EventArgs e)
		{
			//	Verify password and confirmation password match
			if (txtPassword.Text != txtConfirmPassword.Text )
			{
				MessageBox.Show(this,
					"Password and confirmation password do not match.  Please retype these passwords.",
					"Password Error",
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
            /*
			_globalConfiguration.configuration.VPNConnectInfo.connectionName = txtConnectName.Text;
			_globalConfiguration.configuration.VPNConnectInfo.userName		 = txtUserName.Text;
			_globalConfiguration.configuration.VPNConnectInfo.password		 = txtPassword.Text;
            */
			_globalConfiguration.SaveConfiguration();

		}

		private void frmVPNSetup_OnLoad(object sender, System.EventArgs e)
		{
	
		}

        private void txtConnectName_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
