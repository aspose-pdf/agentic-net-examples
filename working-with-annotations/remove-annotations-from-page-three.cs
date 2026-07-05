using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

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

        // Load the PDF document (using statement ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Verify that the document has at least three pages (pages are 1‑based)
            if (doc.Pages.Count >= 3)
            {
                // Access the third page
                Page thirdPage = doc.Pages[3];

                // Remove all annotations from this page
                thirdPage.Annotations.Clear();
            }
            else
            {
                Console.Error.WriteLine("The document contains fewer than 3 pages.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"All annotations removed from page 3. Saved to '{outputPath}'.");
    }
}