using System;
using System.IO;
using System.Text;
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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // StringBuilder will hold the combined text from the required pages
            StringBuilder combined = new StringBuilder();

            // Pages are 1‑based; extract from pages 25 and 26
            for (int pageNum = 25; pageNum <= 26; pageNum++)
            {
                // Ensure the page exists
                if (pageNum > doc.Pages.Count)
                {
                    Console.Error.WriteLine($"Page {pageNum} does not exist in the document.");
                    continue;
                }

                // Use TextAbsorber (core API) to extract the textual content of the page
                TextAbsorber absorber = new TextAbsorber();
                absorber.Visit(doc.Pages[pageNum]);

                // Append the extracted text (including line breaks) to the builder
                combined.AppendLine(absorber.Text);
            }

            // Write the combined text to the output file
            File.WriteAllText(outputPath, combined.ToString());
        }

        Console.WriteLine($"Text from pages 25‑26 saved to '{outputPath}'.");
    }
}
