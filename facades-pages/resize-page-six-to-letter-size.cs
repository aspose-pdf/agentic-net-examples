using System;
using System.IO;
using Aspose.Pdf;

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
        var document = new Document(inputPath);

        // Verify that page 6 exists.
        if (document.Pages.Count < 6)
        {
            Console.Error.WriteLine("The document has fewer than 6 pages.");
            return;
        }

        // Letter size in points (8.5" x 11" = 612 x 792 points).
        // Set the size of page 6.
        document.Pages[6].SetPageSize(612, 792);

        // Save the modified PDF.
        document.Save(outputPath);

        Console.WriteLine($"Page 6 resized to Letter and saved to '{outputPath}'.");
    }
}
