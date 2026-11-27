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

        // Load the PDF inside a using block for deterministic disposal.
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // TextAbsorber uses the TextDevice internally.
            // Set extraction mode to Pure to get raw text without formatting.
            TextAbsorber absorber = new TextAbsorber
            {
                ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure)
            };

            // Extract text from all pages.
            pdfDoc.Pages.Accept(absorber);
            string rawText = absorber.Text ?? string.Empty;

            // Remove line breaks and collapse multiple spaces.
            string cleanedText = rawText
                .Replace("\r\n", " ")
                .Replace("\n", " ")
                .Replace("\r", " ")
                .Replace("\t", " ");

            // Optional: collapse consecutive spaces into a single space.
            while (cleanedText.Contains("  "))
                cleanedText = cleanedText.Replace("  ", " ");

            // Write the cleaned text to the output file.
            File.WriteAllText(outputTxtPath, cleanedText);
        }

        Console.WriteLine($"Cleaned text written to '{outputTxtPath}'.");
    }
}