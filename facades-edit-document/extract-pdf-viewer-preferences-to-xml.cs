using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputXmlPath = "viewerPreferences.xml";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF with PdfContentEditor (facade)
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(inputPdfPath);

            // Retrieve the viewer preference flags as an integer
            int prefValue = editor.GetViewerPreference();

            // Build a simple XML representation of the preferences
            XDocument xmlDoc = new XDocument(
                new XElement("ViewerPreferences",
                    new XAttribute("Value", prefValue)
                )
            );

            // Save the XML configuration file
            xmlDoc.Save(outputXmlPath);

            Console.WriteLine($"Viewer preferences saved to '{outputXmlPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}