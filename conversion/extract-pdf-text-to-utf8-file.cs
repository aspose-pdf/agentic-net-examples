using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputTxt = "output.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Create a TextAbsorber to extract text
            TextAbsorber absorber = new TextAbsorber();

            // Preserve line breaks by using the Pure formatting mode
            absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);

            // Extract text from all pages
            doc.Pages.Accept(absorber);

            // Get the extracted text
            string extractedText = absorber.Text;

            // Write the text to a UTF‑8 encoded file, preserving line breaks
            File.WriteAllText(outputTxt, extractedText, Encoding.UTF8);
        }

        Console.WriteLine($"Text extracted to '{outputTxt}'.");
    }
}