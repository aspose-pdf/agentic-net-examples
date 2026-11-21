using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_letter.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Verify the document has at least seven pages (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 7)
            {
                Console.Error.WriteLine("The document contains fewer than 7 pages.");
                return;
            }

            // Retrieve page 7
            Page page7 = doc.Pages[7];

            // Change its size to US Letter (8.5 x 11 inches) – dimensions are in points (1 inch = 72 points)
            page7.SetPageSize(612, 792); // 8.5*72 = 612, 11*72 = 792

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page 7 resized to Letter format and saved as '{outputPath}'.");
    }
}
