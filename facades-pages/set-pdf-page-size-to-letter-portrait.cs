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
            // Initialize the facade for page editing
            using (PdfPageEditor editor = new PdfPageEditor(doc))
            {
                // Letter size in points (1 inch = 72 points)
                double width  = 8.5 * 72; // 612 points
                double height = 11  * 72; // 792 points

                // Set the new page size for all pages
                editor.PageSize = new PageSize((float)width, (float)height);
                editor.ApplyChanges(); // Apply changes to the document
            }

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page size set to Letter portrait and saved to '{outputPath}'.");
    }
}