using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";          // Original PDF
        const string tempPath   = "temp_landscape.pdf"; // Intermediate rotated PDF
        const string outputPath = "booklet.pdf";        // Final booklet

        // Verify the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // ------------------------------------------------------------
        // Step 1: Rotate all pages to landscape orientation
        // ------------------------------------------------------------
        // Load the source PDF inside a using block (document disposal rule)
        using (Document doc = new Document(inputPath))
        {
            // Create a PdfPageEditor facade and bind the loaded document
            PdfPageEditor editor = new PdfPageEditor();
            editor.BindPdf(doc);

            // Set the desired page size (A4) – rotation will make it landscape
            editor.PageSize = PageSize.A4;

            // Rotate every page 90 degrees clockwise to achieve landscape layout
            editor.Rotation = 90;

            // Apply the changes and save to a temporary file
            editor.ApplyChanges();
            editor.Save(tempPath);
            editor.Close(); // Dispose the facade resources
        }

        // ------------------------------------------------------------
        // Step 2: Generate a booklet from the rotated PDF
        // ------------------------------------------------------------
        PdfFileEditor pfe = new PdfFileEditor();
        bool success = pfe.MakeBooklet(tempPath, outputPath);

        if (success)
        {
            Console.WriteLine($"Booklet created successfully: {outputPath}");
        }
        else
        {
            Console.Error.WriteLine("Failed to create booklet.");
        }
    }
}