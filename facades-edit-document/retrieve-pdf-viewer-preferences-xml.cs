using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Xml.Linq;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputXml = "viewer_preferences.xml";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF using PdfContentEditor (facade API)
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPdf);

        // Retrieve the viewer preference flags as an integer
        int prefValue = editor.GetViewerPreference();

        // Build a simple XML document representing the preferences
        XDocument xmlDoc = new XDocument(
            new XElement("ViewerPreferences",
                new XElement("Value", prefValue)
            )
        );

        // Save the XML file (standard .NET XML saving)
        xmlDoc.Save(outputXml);

        Console.WriteLine($"Viewer preferences saved to '{outputXml}'.");
    }
}