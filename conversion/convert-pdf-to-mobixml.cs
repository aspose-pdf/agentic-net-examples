using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.mobi";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle rule: use using for disposal)
            using (Document pdfDoc = new Document(inputPath))
            {
                // Initialize MobiXmlSaveOptions with default settings
                MobiXmlSaveOptions saveOptions = new MobiXmlSaveOptions();

                // Save the document as MobiXML (non‑PDF format requires explicit SaveOptions)
                pdfDoc.Save(outputPath, saveOptions);
            }

            Console.WriteLine($"PDF successfully converted to MobiXML: '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}