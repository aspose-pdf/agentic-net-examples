using System;
using System.IO;
using Aspose.Pdf;               // Core PDF API (Document, XmlSaveOptions)
using Aspose.Pdf.Facades;      // Facade namespace (included as requested)

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputXmlPath = "output.xml";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // XmlSaveOptions forces the output to be XML (Spreadsheet 2003 format)
                XmlSaveOptions xmlOptions = new XmlSaveOptions();

                // Save the document as XML using the explicit options (required for non‑PDF formats)
                pdfDoc.Save(outputXmlPath, xmlOptions);
            }

            Console.WriteLine($"Conversion completed successfully: {outputXmlPath}");
        }
        catch (Exception ex)
        {
            // Catch any conversion‑related errors and report them
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}