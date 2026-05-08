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

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (document disposal handled by using)
        using (Document doc = new Document(inputPath))
        {
            // Create a PdfPageEditor instance and bind the loaded document
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                editor.BindPdf(doc);

                // Example modifications:
                // Rotate all pages 90 degrees
                editor.Rotation = 90;
                // Set zoom to 80%
                editor.Zoom = 0.8f;

                // Apply the changes to the document
                editor.ApplyChanges();

                // Save the edited PDF to a new file
                editor.Save(outputPath);

                // Explicitly close the editor (Dispose will also call Close)
                editor.Close();
            }
        }

        Console.WriteLine($"Edited PDF saved to '{outputPath}'.");
    }
}