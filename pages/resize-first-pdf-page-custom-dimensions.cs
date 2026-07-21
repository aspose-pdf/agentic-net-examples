using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_custom_size.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Verify that the document has at least one page
            if (doc.Pages.Count >= 1)
            {
                // Change the size of the first page to 500 × 700 points
                doc.Pages[1].SetPageSize(500, 700);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"First page resized and saved to '{outputPath}'.");
    }
}