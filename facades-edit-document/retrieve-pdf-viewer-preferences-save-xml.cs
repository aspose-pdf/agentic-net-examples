using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputXmlPath = "viewer_preferences.xml";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Load the PDF using PdfContentEditor (facade API)
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPdfPath);

        // Retrieve the viewer preferences as an integer flag set
        int prefValue = editor.GetViewerPreference();

        // Serialize the preferences to a simple XML structure
        XDocument xmlDoc = new XDocument(
            new XElement("ViewerPreferences",
                new XElement("PreferenceValue", prefValue)
            )
        );

        // Save the XML configuration file
        xmlDoc.Save(outputXmlPath);

        Console.WriteLine($"Viewer preferences saved to '{outputXmlPath}'.");
    }
}