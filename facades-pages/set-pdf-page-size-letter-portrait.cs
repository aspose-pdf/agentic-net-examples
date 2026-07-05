using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_letter.pdf";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Initialize the PdfPageEditor facade (also disposable)
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                // Bind the loaded document to the editor
                editor.BindPdf(doc);

                // Define Letter portrait dimensions in points (1 inch = 72 points)
                double width = 8.5 * 72; // 612 points
                double height = 11 * 72; // 792 points

                // Set the custom page size for all pages
                editor.PageSize = new PageSize((float)width, (float)height);

                // Apply the changes to the document
                editor.ApplyChanges();

                // Save the modified document (lifecycle rule: use Document.Save)
                doc.Save(outputPath);
            }
        }

        Console.WriteLine($"Page size set to Letter portrait and saved to '{outputPath}'.");
    }
}