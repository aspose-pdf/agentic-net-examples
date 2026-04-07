using System;
using System.IO;
using Aspose.Pdf;               // Document, HtmlSaveOptions, XmlLoadOptions

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string xmlInputPath  = "input.xml";   // XML that will be converted to PDF first
        const string htmlOutputPath = "preview.html";

        // Verify the XML source exists
        if (!File.Exists(xmlInputPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlInputPath}");
            return;
        }

        try
        {
            // Load the XML and create a PDF document in memory
            // XmlLoadOptions is required for XML → PDF conversion
            using (Document pdfDoc = new Document(xmlInputPath, new XmlLoadOptions()))
            {
                // Prepare HTML save options (default settings are fine for a web preview)
                HtmlSaveOptions htmlOptions = new HtmlSaveOptions();

                // Save the PDF content as HTML
                pdfDoc.Save(htmlOutputPath, htmlOptions);
            }

            Console.WriteLine($"HTML preview generated at '{htmlOutputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}