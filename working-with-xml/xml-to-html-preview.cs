using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the source XML file (must exist in the working directory)
        string xmlPath = "input.xml";

        // Verify that the XML file exists before attempting to read it
        if (!File.Exists(xmlPath))
        {
            Console.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Read the XML content into a string – BindXml expects the XML *content*, not a file path
        string xmlContent = File.ReadAllText(xmlPath);

        // Load the XML content into a PDF document
        using (Document pdfDocument = new Document())
        {
            pdfDocument.BindXml(xmlContent);

            // HtmlSaveOptions are available directly in the Aspose.Pdf namespace
            HtmlSaveOptions saveOptions = new HtmlSaveOptions();

            // Save the PDF document as an HTML file for web preview
            pdfDocument.Save("output.html", saveOptions);
        }

        Console.WriteLine("XML has been converted to HTML successfully.");
    }
}