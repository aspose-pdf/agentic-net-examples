using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Verify that the document has at least three pages (1‑based indexing)
            if (doc.Pages.Count < 3)
            {
                Console.Error.WriteLine("The document contains fewer than 3 pages.");
                return;
            }

            // Access the third page
            Page thirdPage = doc.Pages[3];

            // Remove all annotations from the third page
            thirdPage.Annotations.Clear();

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"All annotations removed from page 3. Saved to '{outputPath}'.");
    }
}