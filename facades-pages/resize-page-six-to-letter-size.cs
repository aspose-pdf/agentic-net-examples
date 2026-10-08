using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document pdfDoc = new Document(inputPath);

        // Ensure the document has at least six pages
        if (pdfDoc.Pages.Count < 6)
        {
            Console.Error.WriteLine("The PDF contains fewer than 6 pages.");
            return;
        }

        // Letter size in points (1 inch = 72 points): 8.5" x 11" => 612 x 792
        pdfDoc.Pages[6].SetPageSize(612, 792);

        // Save the modified PDF
        pdfDoc.Save(outputPath);

        Console.WriteLine($"Page 6 resized to Letter and saved as '{outputPath}'.");
    }
}
