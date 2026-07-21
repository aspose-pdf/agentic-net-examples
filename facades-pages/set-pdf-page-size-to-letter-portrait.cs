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

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a PdfPageEditor facade and bind it to the loaded document
            PdfPageEditor editor = new PdfPageEditor();
            editor.BindPdf(doc);

            // Define Letter portrait dimensions (8.5" x 11") in points (1 inch = 72 points)
            double widthPoints  = 8.5 * 72; // 612 points
            double heightPoints = 11  * 72; // 792 points

            // Set the new page size for all pages
            editor.PageSize = new PageSize((float)widthPoints, (float)heightPoints);

            // Apply the changes to the document
            editor.ApplyChanges();

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page size set to Letter portrait and saved to '{outputPath}'.");
    }
}