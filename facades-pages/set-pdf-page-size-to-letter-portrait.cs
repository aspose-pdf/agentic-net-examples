using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_letter.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Bind a PdfPageEditor facade to the document
            using (PdfPageEditor editor = new PdfPageEditor(doc))
            {
                // Letter size in points (1 inch = 72 points)
                float width  = 8.5f * 72f; // 612 points
                float height = 11f  * 72f; // 792 points

                // Set custom page size for all pages
                editor.PageSize = new PageSize(width, height);
                editor.ApplyChanges(); // Apply the size change
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with Letter portrait size to '{outputPath}'.");
    }
}