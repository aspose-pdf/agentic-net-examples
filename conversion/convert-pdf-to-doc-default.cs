using System;
using System.IO;
using Aspose.Pdf;               // Core PDF classes and DocSaveOptions are here

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.doc";

        // Verify source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPath))
            {
                // Default options for DOC conversion (basic text extraction)
                DocSaveOptions docOptions = new DocSaveOptions();

                // Save as DOC using explicit SaveOptions (required for non‑PDF formats)
                pdfDoc.Save(outputPath, docOptions);
            }

            Console.WriteLine($"PDF successfully converted to DOC: '{outputPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}