using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Determine how many pages to process (up to 3)
            int pagesToProcess = Math.Min(3, doc.Pages.Count);

            // Open the output text file for writing
            using (StreamWriter writer = new StreamWriter(outputPath, false))
            {
                // Iterate over pages 1 through pagesToProcess (1‑based indexing)
                for (int pageNumber = 1; pageNumber <= pagesToProcess; pageNumber++)
                {
                    // Use TextAbsorber to extract the textual content of the page.
                    // TextAbsorber works across all Aspose.Pdf versions and does not rely on a non‑existent Paragraphs collection.
                    TextAbsorber absorber = new TextAbsorber();
                    absorber.Visit(doc.Pages[pageNumber]);

                    // Write the extracted text (which represents the page's paragraphs) to the output file.
                    writer.WriteLine(absorber.Text);
                }
            }
        }

        Console.WriteLine($"Paragraphs from pages 1‑3 have been written to '{outputPath}'.");
    }
}
