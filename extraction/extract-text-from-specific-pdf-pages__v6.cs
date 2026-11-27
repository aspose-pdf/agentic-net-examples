using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Prepare a TextAbsorber to collect text
            TextAbsorber absorber = new TextAbsorber();

            // Extract text from page 18 (1‑based indexing)
            doc.Pages[18].Accept(absorber);

            // Extract text from page 19 and append to the same absorber
            doc.Pages[19].Accept(absorber);

            // Write the combined text to the output file
            File.WriteAllText(outputPath, absorber.Text);
        }

        Console.WriteLine($"Extracted text from pages 18 and 19 saved to '{outputPath}'.");
    }
}