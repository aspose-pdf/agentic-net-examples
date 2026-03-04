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

        // Load the PDF document.
        using (Document doc = new Document(inputPath))
        {
            // Initialize the PdfPageEditor facade.
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                // Bind the loaded document to the editor.
                editor.BindPdf(doc);

                // Specify that only page 1 will be edited.
                editor.ProcessPages = new int[] { 1 };

                // Rotate page 1 by 90 degrees.
                editor.Rotation = 90;

                // Set a zoom factor (1.0 = 100%).
                editor.Zoom = 1.5f;

                // Change the page size to A4 (595 x 842 points).
                editor.PageSize = new PageSize(595, 842);

                // Apply all changes to the document.
                editor.ApplyChanges();

                // Save the modified PDF.
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}