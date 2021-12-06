using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace xmljson1
{
  class XmlViev : Calculator
  {
    private XmlDocument _xml;
    public override string outputText { get; set; }
    public XmlViev(string input) : base(input) { }
    public override Input getInput(string inputText)
    {
      _xml = new XmlDocument();
      _xml.LoadXml(inputText);
      XmlSerializer serializer = new XmlSerializer(typeof(Input));
      var input = new Input();
      using (XmlReader reader = new XmlNodeReader(_xml))
      {
        input = (Input)serializer.Deserialize(reader);
      }
      return input;
    }

    public override void outputToText()
    {
      XmlSerializer serializer = new XmlSerializer(typeof(Output));
      _xml = new XmlDocument();
      using (MemoryStream stream = new MemoryStream()) { 
        XmlWriter writer = XmlWriter.Create(stream, new XmlWriterSettings { OmitXmlDeclaration = true, Indent = true });
        serializer.Serialize(writer, _output);
        stream.Position = 0;
        _xml.Load(stream);
      }
      outputText = _xml.DocumentElement.InnerXml;
    }
  }
}
