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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Verify that the document has at least three pages (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 3)
            {
                Console.Error.WriteLine("The document does not contain page 3.");
                return;
            }

            // Use TextAbsorber with Raw formatting mode to extract text from page 3
            var absorber = new TextAbsorber();
            absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Raw);
            doc.Pages[3].Accept(absorber);
            string extractedText = absorber.Text;

            // Write the extracted text to a plain‑text file using UTF‑8 encoding
            File.WriteAllText(outputPath, extractedText, Encoding.UTF8);
        }

        Console.WriteLine($"Extracted text from page 3 has been saved to '{outputPath}'.");
    }
}
