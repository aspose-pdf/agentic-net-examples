using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputTxt = "output.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdf))
            {
                // TextAbsorber extracts text from the document
                TextAbsorber absorber = new TextAbsorber
                {
                    // Extract plain text without formatting tags
                    ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure)
                };

                // Apply the absorber to all pages
                pdfDoc.Pages.Accept(absorber);

                // Concatenated text from all pages
                string allText = absorber.Text ?? string.Empty;

                // Write the result to a .txt file
                File.WriteAllText(outputTxt, allText);
            }

            Console.WriteLine($"Text extracted to '{outputTxt}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}