using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace PersistentStackVisualization.ParseTest
{
    public static class Pearser
    {
        public static void Run(string content)
        {
            var document = new XDocument(
                new XDeclaration("1.0", "uft-8", "yes"),
                new XElement("content")
                );
            string[] arrayContent = content.Split('\n');
            foreach(string command in arrayContent)
            {
                string[] specificCommand = command.Split();
                if(specificCommand[0] == "push")
                {
                    var commandXml = new XElement("command",
                        new XAttribute("type", "push"),
                        new XAttribute("version", specificCommand[1]),
                        new XAttribute("value", specificCommand[2])
                        );
                    document.Element("content").Add(commandXml);
                    
                }
                if(specificCommand[0] == "pop")
                {
                    var commandXml = new XElement("command",
                        new XAttribute("type", "pop"),
                        new XAttribute("version", specificCommand[1])
                        );
                    document.Element("content").Add(commandXml);
                }
            }
            document.Save("test.xml");
        }
    }
}
