using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a PdfPageEditor facade and bind the loaded document
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                editor.BindPdf(doc);

                // Example modifications:
                // Rotate all pages by 90 degrees
                editor.Rotation = 90;
                // Set zoom to 80%
                editor.Zoom = 0.8f;

                // Apply the configured changes to the bound document
                editor.ApplyChanges();

                // Save the edited PDF to the specified output file
                editor.Save(outputPath);

                // Close the editor to release the bound document and unmanaged resources
                editor.Close();
            } // Dispose of PdfPageEditor (calls Dispose internally)

        } // Dispose of Document

        Console.WriteLine($"Edited PDF saved to '{outputPath}'.");
    }
}