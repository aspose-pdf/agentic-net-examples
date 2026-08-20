using System;
using System.IO;
using System.Text;
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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create a TextAbsorber to extract text from all pages
            TextAbsorber absorber = new TextAbsorber();

            // Accept the absorber for the entire page collection (1‑based indexing is handled internally)
            pdfDoc.Pages.Accept(absorber);

            // Retrieve the concatenated text from all pages
            string extractedText = absorber.Text ?? string.Empty;

            // Write the extracted text directly to a .txt file using UTF‑8 encoding
            File.WriteAllText(outputTxtPath, extractedText, Encoding.UTF8);

            Console.WriteLine($"Text extracted and saved to '{outputTxtPath}'.");
        }
    }
}
