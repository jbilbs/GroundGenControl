using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using GroundGenControl.Configuration;

//	Test comment for integration test

namespace GroundGenControl
{
	/// <summary>
	/// Summary description for frmAddEditSite.
	/// </summary>
	public class frmAddEditSite : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label lblIPAddress;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.ErrorProvider ipError;
		private System.Windows.Forms.ErrorProvider minError;
		private System.Windows.Forms.ErrorProvider descError;
		private System.Windows.Forms.Label lblSiteName;
		private System.Windows.Forms.TextBox txtName;
        private IContainer components;
        private bool	_editMode	= false;
		private string	_oldName	=	 "";

		GlobalConfiguration	_globalConfiguration = GlobalConfiguration.instance;
        private CheckBox checkboxWeather;
        private TextBox txtMobileID;



		public frmAddEditSite(bool editMode)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			//	Set editing mode
			_editMode = editMode;

			//	Change dialog text to reflect edit mode
			if ( _editMode )
			{

				this.Text	 = "Edit Site";
				btnSave.Text = "Update";
			}



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
            this.components = new System.ComponentModel.Container();
            this.lblSiteName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblIPAddress = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.ipError = new System.Windows.Forms.ErrorProvider(this.components);
            this.minError = new System.Windows.Forms.ErrorProvider(this.components);
            this.descError = new System.Windows.Forms.ErrorProvider(this.components);
            this.txtMobileID = new System.Windows.Forms.TextBox();
            this.checkboxWeather = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.ipError)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minError)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.descError)).BeginInit();
            this.SuspendLayout();
            // 
            // lblSiteName
            // 
            this.lblSiteName.Location = new System.Drawing.Point(24, 24);
            this.lblSiteName.Name = "lblSiteName";
            this.lblSiteName.Size = new System.Drawing.Size(100, 24);
            this.lblSiteName.TabIndex = 0;
            this.lblSiteName.Text = "Site Name";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(24, 48);
            this.txtName.MaxLength = 64;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(232, 20);
            this.txtName.TabIndex = 1;
            // 
            // lblIPAddress
            // 
            this.lblIPAddress.Location = new System.Drawing.Point(24, 80);
            this.lblIPAddress.Name = "lblIPAddress";
            this.lblIPAddress.Size = new System.Drawing.Size(100, 23);
            this.lblIPAddress.TabIndex = 2;
            this.lblIPAddress.Text = "Mobile ID";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(56, 174);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(152, 174);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ipError
            // 
            this.ipError.ContainerControl = this;
            // 
            // minError
            // 
            this.minError.ContainerControl = this;
            // 
            // descError
            // 
            this.descError.ContainerControl = this;
            // 
            // txtMobileID
            // 
            this.txtMobileID.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtMobileID.Location = new System.Drawing.Point(24, 106);
            this.txtMobileID.MaxLength = 64;
            this.txtMobileID.Name = "txtMobileID";
            this.txtMobileID.Size = new System.Drawing.Size(232, 20);
            this.txtMobileID.TabIndex = 2;
            // 
            // checkboxWeather
            // 
            this.checkboxWeather.AutoSize = true;
            this.checkboxWeather.Location = new System.Drawing.Point(24, 142);
            this.checkboxWeather.Name = "checkboxWeather";
            this.checkboxWeather.Size = new System.Drawing.Size(161, 17);
            this.checkboxWeather.TabIndex = 5;
            this.checkboxWeather.Text = "Check Weather On Connect";
            this.checkboxWeather.UseVisualStyleBackColor = true;
            this.checkboxWeather.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // frmAddEditSite
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(304, 225);
            this.Controls.Add(this.checkboxWeather);
            this.Controls.Add(this.txtMobileID);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblIPAddress);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblSiteName);
            this.Name = "frmAddEditSite";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Site";
            this.Load += new System.EventHandler(this.frmAddEditSite_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ipError)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minError)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.descError)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion


		private bool validate()
		{
			bool success = true;			

			//	Clear all old errors
			descError.SetError(txtName	,"");
			ipError.SetError(txtMobileID,"");

			if ( txtName.Text == "" )
			{
				descError.SetError(txtName,"A site name must be provided");
				success = false;
			}
			else
			if ( _globalConfiguration.configuration.isNameInSites(txtName.Text) &&
				 txtName.Text != _oldName )
			{
				descError.SetError(txtName,"The specified site already exists");
				success = false;
			}

			//	Ensure a valid number is typed in for each octet
			if ( txtMobileID.Text.Length < 1 )
			{

				ipError.SetError(txtMobileID, "A valid SkyWave mobile ID must be provided");
				success = false;
			}

			return success;


		}

		private void saveChanges()
		{

            Site tempSite = new Site(txtName.Text, txtMobileID.Text, checkboxWeather.Checked);
			//tempSite.name	= txtName.Text;
			//tempSite.mobileID     = txtMobileID.Text;
	
			if ( _editMode )
			{
				_globalConfiguration.configuration.replaceSite(_oldName, tempSite);
			}
			else
			{
				_globalConfiguration.configuration.addSite(tempSite);
			}
			_globalConfiguration.SaveConfiguration();

		}
		

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			if (validate())
			{
				saveChanges();
				//	User clicked add, validate the description and IP address
				this.DialogResult = DialogResult.OK;
			}
		
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			//	We don't want results saved
			this.DialogResult = DialogResult.Cancel;

		}

		public string name			{ get { return txtName.Text;			} set { txtName.Text = value; _oldName = value;	} }
		public string mobileID		{ get { return txtMobileID.Text;		} set { txtMobileID.Text = value;	} }

        public bool checkWeather {  get { return checkboxWeather.Checked;   } set { checkboxWeather.Checked = value; } }

        private void frmAddEditSite_Load(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
