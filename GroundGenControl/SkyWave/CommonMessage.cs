using System;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Net;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace SkyWave
{

    [Serializable]
    [XmlType("Message")]
    [DataContract(Namespace = "", Name = "Message")]
    public class CommonMessage
    {
        [XmlAttribute("Name")]
        [DataMember(Name = "Name", Order = 0, IsRequired = false, EmitDefaultValue = false)]
        [DefaultValue(null)]
        public string Name { get; set; }

        [XmlAttribute("SIN")]
        [DataMember(Name = "SIN", Order = 1, IsRequired = true)]
        public int SIN { get; set; }

        [XmlAttribute("MIN")]
        [DataMember(Name = "MIN", Order = 2, IsRequired = true)]
        public int MIN { get; set; }

        [XmlAttribute("IsForward")]
        [DataMember(Name = "IsForward", Order = 3, IsRequired = false, EmitDefaultValue = false)]
        [DefaultValue(null)]
        public string IsForward { get; set; }

        [DataMember(Name = "Fields", Order = 4, IsRequired = false, EmitDefaultValue = false)]
        [DefaultValue(null)]
        public CommonMessageFieldList Fields { get; set; }

        public CommonMessage()
        {
            Fields = new CommonMessageFieldList();
        }
    }

    public class CommonMessageFieldList : List<CommonMessageField>
    {
    }

    [Serializable]
    [XmlType("Field")]
    [DataContract(Namespace = "", Name = "Field")]  
    public class CommonMessageField
    {
        [XmlAttribute("Name")]
        [DataMember(Name = "Name", Order = 0, IsRequired = true)]
        [DefaultValue(null)]
        public string Name { get; set; }

        [XmlAttribute("Value")]
        [DataMember(Name = "Value", Order = 1, IsRequired = false, EmitDefaultValue = false)]
        [DefaultValue(null)]
        public string Value { get; set; }

        [XmlAttribute("Type")]
        [DataMember(Name = "Type", Order = 2, IsRequired = false, EmitDefaultValue = false)]
        [DefaultValue(null)]
        public string Type { get; set; }

        [DataMember(Name = "Elements", Order = 3, IsRequired = false, EmitDefaultValue = false)]
        [DefaultValue(null)]
        public CommonMessageElementList Elements { get; set; }

        [DataMember(Name = "Message", Order = 4, IsRequired = false, EmitDefaultValue = false)]
        [DefaultValue(null)]
        public CommonMessage Message { get; set; }
    }

    public class CommonMessageElementList : List<CommonMessageElement>
    {
    }

    [XmlType("Element")]
    [DataContract(Namespace = "", Name = "Element")]  // REST web service related
    public class CommonMessageElement
    {
        [XmlAttribute("Index")]
        [DataMember(Name = "Index", Order = 0, IsRequired = true)]
        [DefaultValue(-1)]
        public int Index { get; set; }

        [DataMember(Name = "Fields", Order = 1, IsRequired = false, EmitDefaultValue = false)]
        [DefaultValue(null)]
        public CommonMessageFieldList Fields { get; set; }

        public CommonMessageElement()
        {
            Fields = new CommonMessageFieldList();
        }
    }

}
