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

        // Load the PDF document with deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Use TextAbsorber (core API) to collect text from the required pages
            TextAbsorber absorber = new TextAbsorber();

            // Extract text from page 16 (1‑based indexing)
            absorber.Visit(doc.Pages[16]);
            // Extract text from page 17
            absorber.Visit(doc.Pages[17]);

            // The TextAbsorber aggregates the extracted text in its Text property
            string mergedText = absorber.Text;

            // Save the merged text to a plain‑text file
            File.WriteAllText(outputPath, mergedText, Encoding.UTF8);
        }

        Console.WriteLine($"Paragraphs from pages 16‑17 saved to '{outputPath}'.");
    }
}
