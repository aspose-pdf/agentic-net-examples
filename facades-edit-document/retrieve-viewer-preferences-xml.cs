using System;
using System.IO;
using System.Xml;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        string inputPdf = "input.pdf";
        string outputXml = "viewer_prefs.xml";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine("Input PDF not found: " + inputPdf);
            return;
        }

        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPdf);
        int prefValue = editor.GetViewerPreference();

        // Serialize the viewer preference value to XML
        XmlWriterSettings settings = new XmlWriterSettings();
        settings.Indent = true;
        using (XmlWriter writer = XmlWriter.Create(outputXml, settings))
        {
            writer.WriteStartDocument();
            writer.WriteStartElement("ViewerPreferences");
            writer.WriteAttributeString("value", prefValue.ToString());
            writer.WriteEndElement();
            writer.WriteEndDocument();
        }

        Console.WriteLine("Viewer preferences saved to " + outputXml);
    }
}
