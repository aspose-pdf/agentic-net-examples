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

        // Load the PDF document with deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Verify the document has at least seven pages (1‑based indexing)
            if (doc.Pages.Count < 7)
            {
                Console.Error.WriteLine("The document contains fewer than 7 pages.");
                return;
            }

            // Access page 7
            Page page7 = doc.Pages[7];

            // Set the page size to Letter (8.5 x 11 inches) using point dimensions (1 inch = 72 points)
            page7.SetPageSize(612, 792); // 8.5*72 = 612, 11*72 = 792

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page 7 resized to Letter and saved as '{outputPath}'.");
    }
}