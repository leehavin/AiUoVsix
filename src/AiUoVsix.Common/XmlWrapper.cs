using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace AiUoVsix.Common
{

    public class XmlWrapper
    {
        public string FileName { get; set; }

        public XmlDocument XmlDoc { get; private set; }

        public XmlWrapper(string filename)
        {
            //IL_002f: Unknown result type (might be due to invalid IL or missing references)
            //IL_0039: Expected O, but got Unknown
            //IL_003b: Unknown result type (might be due to invalid IL or missing references)
            //IL_0041: Expected O, but got Unknown
            if (!File.Exists(filename))
            {
                throw new FileNotFoundException("xml文件不存在。" + filename);
            }

            FileName = filename;
            XmlDoc = new XmlDocument();
            XmlTextReader val = new XmlTextReader(filename);
            try
            {
                val.Namespaces = false;
                XmlDoc.Load((XmlReader)(object)val);
            }
            finally
            {
                ((IDisposable)val)?.Dispose();
            }
        }

        private XmlWrapper()
        {
        }

        public static XmlWrapper Create(string content)
        {
            //IL_0008: Unknown result type (might be due to invalid IL or missing references)
            //IL_0012: Expected O, but got Unknown
            XmlWrapper xmlWrapper = new XmlWrapper();
            xmlWrapper.XmlDoc = new XmlDocument();
            xmlWrapper.XmlDoc.LoadXml(content);
            return xmlWrapper;
        }

        public override string ToString()
        {
            //IL_000e: Unknown result type (might be due to invalid IL or missing references)
            //IL_0014: Expected O, but got Unknown
            using (MemoryStream memoryStream = new MemoryStream())
            {
                XmlTextWriter val = new XmlTextWriter((Stream)memoryStream, Encoding.UTF8);
                try
                {
                    ((XmlNode)XmlDoc.DocumentElement).WriteContentTo((XmlWriter)(object)val);
                }
                finally
                {
                    ((IDisposable)val)?.Dispose();
                }

                return Encoding.UTF8.GetString(memoryStream.ToArray());
            } 
          
        }

        public void Save(string filename = null)
        {
            //IL_0011: Unknown result type (might be due to invalid IL or missing references)
            //IL_0017: Expected O, but got Unknown
            XmlTextWriter val = new XmlTextWriter(filename ?? FileName, Encoding.UTF8);
            try
            {
                val.Namespaces = false;
                val.Formatting = (Formatting)1;
                ((XmlNode)XmlDoc.DocumentElement).WriteTo((XmlWriter)(object)val);
            }
            finally
            {
                ((IDisposable)val)?.Dispose();
            }
        }

        private XmlElement GetElement(string xpath, bool checkExist = true)
        {
            XmlNode obj = ((XmlNode)XmlDoc.DocumentElement).SelectSingleNode(xpath);
            XmlElement val = (XmlElement)(object)((obj is XmlElement) ? obj : null);
            if (checkExist && val == null)
            {
                throw new Exception("xml节点不存在. xpath: " + xpath);
            }

            return val;
        }

        public void AppendChildNode(string xpath, string nodeName, string innerText = null)
        {
            XmlNode val = XmlDoc.CreateNode("element", nodeName, (string)null);
            val.InnerText = innerText;
            XmlElement element = GetElement(xpath);
            ((XmlNode)element).AppendChild(val);
        }

        public void SetAttribute(string xpath, string attributeName, object attributeValue)
        {
            GetElement(xpath).SetAttribute(attributeName, Convert.ToString(attributeValue));
        }

        public void SetInnerText(string xpath, string text, bool autoApend = true)
        {
            XmlElement element = GetElement(xpath, checkExist: false);
            if (element == null)
            {
                int num = xpath.LastIndexOf('/');
                string xpath2 = xpath.Substring(0, num);
                string nodeName = xpath.Substring(num + 1);
                AppendChildNode(xpath2, nodeName, text);
            }
            else
            {
                ((XmlNode)element).InnerText = text;
            }
        }

        public void DeleteNode(string xpath, string nodeName)
        {
            XmlElement element = GetElement(xpath + "\\" + nodeName);
            XmlNode parentNode = ((XmlNode)element).ParentNode;
            if (parentNode != null)
            {
                parentNode.RemoveChild((XmlNode)(object)element);
            }
        }

        public void DeleteAttribute(string xpath, string attributeName)
        {
            GetElement(xpath).RemoveAttribute(attributeName);
        }

        public XmlNodeList GetNodes(string xpath)
        {
            return ((XmlNode)XmlDoc).SelectNodes(xpath);
        }

        public string GetInnerText(string xpath)
        {
            XmlElement element = GetElement(xpath, checkExist: false);
            return (element != null) ? ((XmlNode)element).InnerText : null;
        }

        public string GetAttributeValue(string xpath, string attributeName)
        {
            XmlElement element = GetElement(xpath, checkExist: false);
            return (element != null) ? element.GetAttribute(attributeName) : null;
        }
    }
}
