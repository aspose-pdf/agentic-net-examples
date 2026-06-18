using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

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

                // Example operation: rotate all pages by 90 degrees
                editor.Rotation = 90;

                // Apply the changes to the document
                editor.ApplyChanges();

                // Save the edited PDF to a new file
                editor.Save(outputPath);

                // Explicitly close the facade (Dispose will also be called by using)
                editor.Close();
            }
        }

        Console.WriteLine($"Edited PDF saved to '{outputPath}'.");
    }
}