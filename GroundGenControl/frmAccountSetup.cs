using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using GroundGenControl.Configuration;

namespace GroundGenControl
{
    public partial class frmAccountSetup : Form
    {


        GlobalConfiguration _globalConfiguration = GlobalConfiguration.instance;


        public frmAccountSetup()
        {
            InitializeComponent();
            

            
            txtAccessID.Text        = _globalConfiguration.configuration.SkyWaveInfo.accessID;
            

            //  Set decrypted value



            String strPassword = "";


            if (_globalConfiguration.configuration.SkyWaveInfo.password != null)
            {
                strPassword = _globalConfiguration.configuration.SkyWaveInfo.password;
            }


            if ( strPassword != "")
            {
                strPassword = Encryption.Encryption.DecryptString(_globalConfiguration.configuration.SkyWaveInfo.password, Encryption.Encryption.DEFAULT_PASSPHRASE);
            }

            txtPassword.Text        = strPassword;
            txtConfirmPassword.Text = strPassword;
                        

        }

        private void btnOK_Click(object sender, EventArgs e)
        {

            //	Verify password and confirmation password match
            if (txtPassword.Text != txtConfirmPassword.Text)
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

        /// <summary>
        /// Processes OK confirmation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {


            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// Saves changes to config file
        /// </summary>
        private void saveChanges()
        {
            
			_globalConfiguration.configuration.SkyWaveInfo.accessID = txtAccessID.Text;
            //  Set encrypted value
			_globalConfiguration.configuration.SkyWaveInfo.password =  Encryption.Encryption.EncryptString(txtPassword.Text, Encryption.Encryption.DEFAULT_PASSPHRASE);
            
            _globalConfiguration.SaveConfiguration();

        }


    }
}
