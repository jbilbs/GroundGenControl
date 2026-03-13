using System;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace GroundGenControl.Configuration
{
	/// <summary>
	/// Summary description for Configuration.
	/// </summary>
	public sealed class GlobalConfiguration
	{
		//	We must initialize here for threadsafety
		private static readonly GlobalConfiguration _instance = new GlobalConfiguration();
		private string	CONFIGURATION_FILE_NAME	= "GConfig.xml";

     
	
		Configuration	_configuration = new Configuration();

		private GlobalConfiguration()
		{
			//	Load existing configuration, create a new one if a config wasn't present
			LoadConfiguration();
			SaveConfiguration();
		}

		public static GlobalConfiguration instance
		{
			get
			{
				return _instance;		
			}
		}


		public Configuration configuration
		{
			get
			{
				return _configuration;		
			}

			set
			{
				_configuration = value;		
			}
		
		}

        public String BuildVersion
        {
            get
            {
                return "v1.1 (SKYWAVE)";
            }
        }


		public void SaveConfiguration()
		{
			try
			{
				// Create an instance of the XmlSerializer class;
				// specify the type of object to serialize.
				XmlSerializer serializer = new XmlSerializer(typeof(Configuration));
				TextWriter writer = new StreamWriter(CONFIGURATION_FILE_NAME);

				// Serialize the purchase order, and close the TextWriter.
				serializer.Serialize(writer, _configuration);
				writer.Close();
			}
			catch(Exception ex)
			{
				MessageBox.Show("Unable to save system configuration (" + ex.Message + ")");
			}
		}


		public void LoadConfiguration()
		{
			try
			{
				// Create an instance of the XmlSerializer class;
				// specify the type of object to be deserialized.
				XmlSerializer serializer = new XmlSerializer(typeof(Configuration));
				/* If the XML document has been altered with unknown 
				nodes or attributes, handle them with the 
				UnknownNode and UnknownAttribute events.*/
				serializer.UnknownNode+= new  XmlNodeEventHandler(serializer_UnknownNode);
				serializer.UnknownAttribute+= new XmlAttributeEventHandler(serializer_UnknownAttribute);
	   
				// A FileStream is needed to read the XML document.
				FileStream fs = new FileStream(CONFIGURATION_FILE_NAME, FileMode.Open);

				/* Use the Deserialize method to restore the object's state with data from the XML document. */
				_configuration = new Configuration();
				_configuration = (Configuration) serializer.Deserialize(fs);

				fs.Close();
			}
			catch(System.IO.FileNotFoundException)
			{
				_configuration = new Configuration();
			}
			catch(Exception ex)
			{
				MessageBox.Show("Unable to load system configuration (" + ex.Message + ")");
			}
		}

		private void serializer_UnknownNode(object sender, XmlNodeEventArgs e)
		{
			System.Diagnostics.Debug.WriteLine("Unknown Node:" +   e.Name + "\t" + e.Text);
		}

		private void serializer_UnknownAttribute(object sender, XmlAttributeEventArgs e)
		{
			System.Xml.XmlAttribute attr = e.Attr;
			System.Diagnostics.Debug.WriteLine("Unknown attribute " + attr.Name + "='" + attr.Value + "'");
		}




	}
}
