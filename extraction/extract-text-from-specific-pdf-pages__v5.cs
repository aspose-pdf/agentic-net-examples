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
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPdfPath))
        {
            // Ensure the document has at least 16 pages.
            if (doc.Pages.Count < 16)
            {
                Console.Error.WriteLine("Document does not contain pages 14‑16.");
                return;
            }

            // TextAbsorber extracts text from pages.
            TextAbsorber absorber = new TextAbsorber();
            // Optional: set extraction options (e.g., pure text without formatting).
            absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);

            // Extract text from pages 14, 15, and 16 (1‑based indexing).
            for (int i = 14; i <= 16; i++)
            {
                doc.Pages[i].Accept(absorber);
            }

            // Combined text from the selected pages.
            string combinedText = absorber.Text ?? string.Empty;

            // Write the result to the output text file.
            File.WriteAllText(outputTxtPath, combinedText);
        }

        Console.WriteLine($"Extracted text saved to '{outputTxtPath}'.");
    }
}