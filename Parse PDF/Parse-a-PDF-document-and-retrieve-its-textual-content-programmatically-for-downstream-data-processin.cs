using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Path to the input PDF file
        const string inputPdfPath = "input.pdf";

        // Optional: path to save the extracted text
        const string outputTextPath = "extracted.txt";

        // Verify that the input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create a TextAbsorber to extract text from the document
            TextAbsorber absorber = new TextAbsorber();

            // Configure extraction options (e.g., pure formatting mode)
            absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);

            // Apply the absorber to all pages of the document
            pdfDoc.Pages.Accept(absorber);

            // Retrieve the extracted text
            string extractedText = absorber.Text;

            // Output the text to the console
            Console.WriteLine("=== Extracted Text ===");
            Console.WriteLine(extractedText);

            // Optionally, write the text to a file for downstream processing
            try
            {
                File.WriteAllText(outputTextPath, extractedText);
                Console.WriteLine($"Extracted text saved to '{outputTextPath}'.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to write extracted text: {ex.Message}");
            }
        }
    }
}