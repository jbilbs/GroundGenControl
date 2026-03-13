using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

using GroundGenControl.Configuration;
using GroundGenControl.Messages;

namespace GroundGenControl
{
	/// <summary>
	/// Summary description for frmSiteSetup.
	/// </summary>
	public class frmRemoteSiteSetup : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.Button btnDelete;
		private System.Windows.Forms.Button btnEdit;
		private System.Windows.Forms.ListView lvRemoteSites;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Label lblRemoteSites;
		private System.Windows.Forms.Button btnExit;

		GlobalConfiguration	_globalConfiguration = GlobalConfiguration.instance;

		enum siteListOffsets
		{
			NAME_OFFSET			= 0,
			MOBILEID_OFFSET 	= 1,
            CHECKWEATHER_OFFSET = 2,
		}

		public frmRemoteSiteSetup()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//	Create list view columns and populate current sites
			populateListView();
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
            this.lvRemoteSites = new System.Windows.Forms.ListView();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.lblRemoteSites = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lvRemoteSites
            // 
            this.lvRemoteSites.FullRowSelect = true;
            this.lvRemoteSites.GridLines = true;
            this.lvRemoteSites.Location = new System.Drawing.Point(24, 40);
            this.lvRemoteSites.Name = "lvRemoteSites";
            this.lvRemoteSites.Size = new System.Drawing.Size(615, 200);
            this.lvRemoteSites.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvRemoteSites.TabIndex = 0;
            this.lvRemoteSites.UseCompatibleStateImageBehavior = false;
            this.lvRemoteSites.View = System.Windows.Forms.View.Details;
            this.lvRemoteSites.SelectedIndexChanged += new System.EventHandler(this.list_IndexChange);
            this.lvRemoteSites.DoubleClick += new System.EventHandler(this.list_DoubleClick);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(24, 264);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "Add";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Enabled = false;
            this.btnDelete.Location = new System.Drawing.Point(200, 264);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "Delete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Enabled = false;
            this.btnEdit.Location = new System.Drawing.Point(112, 264);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 23);
            this.btnEdit.TabIndex = 3;
            this.btnEdit.Text = "Edit";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // lblRemoteSites
            // 
            this.lblRemoteSites.Location = new System.Drawing.Point(24, 16);
            this.lblRemoteSites.Name = "lblRemoteSites";
            this.lblRemoteSites.Size = new System.Drawing.Size(416, 23);
            this.lblRemoteSites.TabIndex = 4;
            this.lblRemoteSites.Text = "Remote Site List:";
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(564, 264);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 5;
            this.btnExit.Text = "Exit";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // frmRemoteSiteSetup
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(664, 318);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.lblRemoteSites);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.lvRemoteSites);
            this.Name = "frmRemoteSiteSetup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Remote Site Setup";
            this.ResumeLayout(false);

		}
		#endregion

		public void populateListHeaders()
		{
			int	listViewWidth = lvRemoteSites.Width;

			lvRemoteSites.Columns.Add("Name", Convert.ToInt32(listViewWidth*.30), HorizontalAlignment.Left);
			lvRemoteSites.Columns.Add("SkyWave Mobile ID", Convert.ToInt32(listViewWidth*.40), HorizontalAlignment.Left);
            lvRemoteSites.Columns.Add("Check Weather On Connect?", Convert.ToInt32(listViewWidth * .3), HorizontalAlignment.Left);

		}


		//	Populate list
		public void populateListView()
		{
			lvRemoteSites.Clear();

			populateListHeaders();

			foreach( Site siteInfo in _globalConfiguration.configuration.sites )
			{

				ListViewItem li = new ListViewItem(siteInfo.name);
				li.SubItems.Add(siteInfo.mobileID);
                li.SubItems.Add(siteInfo.checkWeather ? "Yes" : "No");
				lvRemoteSites.Items.Add(li);

			}

		}

		private void btnAdd_Click(object sender, System.EventArgs e)
		{

			frmAddEditSite addSite = new frmAddEditSite(false);
			addSite.ShowDialog();
	
			if ( addSite.DialogResult == DialogResult.OK )
			{
				//	If changes were made to the list view, update it
				populateListView();
			}

			list_IndexChange(null,null);

		}

		
		private void btnEdit_Click(object sender, System.EventArgs e)
		{

	

			frmAddEditSite editSite = new frmAddEditSite(true);

			//	We only allow single selects so pull the first item
			ListViewItem listItem	= this.lvRemoteSites.SelectedItems[0];
			editSite.name			= listItem.Text;
			editSite.mobileID		= listItem.SubItems[(int) siteListOffsets.MOBILEID_OFFSET].Text;
            editSite.checkWeather   = listItem.SubItems[(int) siteListOffsets.CHECKWEATHER_OFFSET].Text.Equals("Yes", StringComparison.InvariantCultureIgnoreCase) ? true : false;

			editSite.ShowDialog();
	
			if ( editSite.DialogResult == DialogResult.OK )
			{
				//	If changes were made to the list view, update it
				populateListView();
			}

			list_IndexChange(null,null);

		}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
		
			//	If an item isn't selected ensure we don't get here
			if ( lvRemoteSites.SelectedItems.Count == 0 )
			{
				return;
			}

			//	We only allow single selects so pull the first item
			ListViewItem listItem	= this.lvRemoteSites.SelectedItems[0];

			if ( MessageBox.Show(this,"Delete site " + listItem.Text + "?",
				"Delete Site Confirmation",
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Warning) == DialogResult.Yes )
			{


				_globalConfiguration.configuration.removeSite(listItem.Text);
				//	Update changes to list view
				populateListView();
				//	Save changes
				_globalConfiguration.SaveConfiguration();
			}

			list_IndexChange(null,null);

		}


		private void btnExit_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void list_DoubleClick(object sender, System.EventArgs e)
		{
			//Invoke edit functionality
			btnEdit_Click(null,null);
		}

		private void list_IndexChange(object sender, System.EventArgs e)
		{
			//	Enable/Disable edit and delete based on number of items selected
			if ( lvRemoteSites.SelectedItems.Count == 0 )
			{
				btnEdit.Enabled = false;
				btnDelete.Enabled = false;
			}
			else
			{
				btnEdit.Enabled = true;
				btnDelete.Enabled = true;

			}
		}




 
   



	}
}
