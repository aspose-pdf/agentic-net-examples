using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputTxtPath = "output.txt";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPdfPath))
        {
            // TextAbsorber extracts text from pages.
            TextAbsorber absorber = new TextAbsorber
            {
                // Optional: extract plain text without formatting.
                ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure)
            };

            // Pages are 1‑based in Aspose.Pdf.
            for (int pageNumber = 28; pageNumber <= 29; pageNumber++)
            {
                // Ensure the requested page exists.
                if (pageNumber <= doc.Pages.Count)
                {
                    doc.Pages[pageNumber].Accept(absorber);
                }
                else
                {
                    Console.Error.WriteLine($"Page {pageNumber} does not exist in the document.");
                }
            }

            // Combine extracted text.
            string combinedText = absorber.Text ?? string.Empty;

            // Write the combined result to a text file.
            using (StreamWriter writer = new StreamWriter(outputTxtPath, false))
            {
                writer.Write(combinedText);
            }

            Console.WriteLine($"Extracted text from pages 28‑29 saved to '{outputTxtPath}'.");
        }
    }
}