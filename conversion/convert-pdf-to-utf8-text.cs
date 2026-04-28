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
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for proper disposal
            using (Document doc = new Document(inputPdf))
            {
                // Configure text extraction to preserve line breaks (Pure mode keeps basic formatting)
                TextExtractionOptions extractionOptions = new TextExtractionOptions(
                    TextExtractionOptions.TextFormattingMode.Pure);

                // Create a TextAbsorber with the specified options
                TextAbsorber absorber = new TextAbsorber(extractionOptions);

                // Extract text from all pages
                doc.Pages.Accept(absorber);

                // Retrieve the extracted text (contains line breaks)
                string extractedText = absorber.Text;

                // Write the text to a UTF‑8 encoded file
                File.WriteAllText(outputTxt, extractedText, Encoding.UTF8);
            }

            Console.WriteLine($"PDF successfully converted to text: '{outputTxt}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}