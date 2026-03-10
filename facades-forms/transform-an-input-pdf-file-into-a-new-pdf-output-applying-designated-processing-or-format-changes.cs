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

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Initialize the PdfPageEditor facade (a Facades class)
            PdfPageEditor editor = new PdfPageEditor();

            // Bind the loaded document to the editor
            editor.BindPdf(doc);

            // Example transformation: rotate every page by 90 degrees
            editor.Rotation = 90; // valid values: 0, 90, 180, 270

            // Apply the changes to the document
            editor.ApplyChanges();

            // Save the transformed PDF (lifecycle rule: use Save method)
            editor.Save(outputPath);
        }

        Console.WriteLine($"Transformed PDF saved to '{outputPath}'.");
    }
}