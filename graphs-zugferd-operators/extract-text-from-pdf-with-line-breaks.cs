using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputTxt = "extracted.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Set extraction options to retain line breaks and spacing.
            // The Pure formatting mode keeps a reasonable amount of layout information.
            TextExtractionOptions extractionOptions = new TextExtractionOptions(
                TextExtractionOptions.TextFormattingMode.Pure);

            // Create a TextAbsorber with the configured options
            TextAbsorber absorber = new TextAbsorber(extractionOptions);

            // Extract text from all pages of the document
            doc.Pages.Accept(absorber);

            // Retrieve the extracted text
            string extractedText = absorber.Text;

            // Write the extracted text to a file
            File.WriteAllText(outputTxt, extractedText);
            Console.WriteLine($"Text successfully extracted to '{outputTxt}'.");
        }
    }
}