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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF document
        using (Document doc = new Document(inputPath))
        {
            // Initialize the page editor with the loaded document
            PdfPageEditor editor = new PdfPageEditor(doc);

            // Rotate all pages by 90 degrees
            editor.Rotation = 90;

            // Change the page size to A4 (595 x 842 points)
            editor.PageSize = new PageSize(595f, 842f);

            // Apply a zoom factor of 80%
            editor.Zoom = 0.8f;

            // Apply the accumulated changes to the document
            editor.ApplyChanges();

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
