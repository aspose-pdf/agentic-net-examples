using System;
using System.IO;
using Aspose.Pdf;   // MobiXmlSaveOptions and Document are in this namespace

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the destination MobiXML file.
        const string inputPdfPath  = "input.pdf";
        const string outputMobiPath = "output.mobi";

        // Verify that the input file exists.
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal.
            using (Document pdfDocument = new Document(inputPdfPath))
            {
                // Initialize the save options for MobiXML format (default settings).
                MobiXmlSaveOptions mobiOptions = new MobiXmlSaveOptions();

                // Save the document as MobiXML using the explicit save options.
                pdfDocument.Save(outputMobiPath, mobiOptions);
            }

            Console.WriteLine($"PDF successfully converted to MobiXML: {outputMobiPath}");
        }
        catch (Exception ex)
        {
            // Handle any errors that may occur during loading or saving.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}