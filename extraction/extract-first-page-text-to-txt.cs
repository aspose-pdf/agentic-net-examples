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

        // Open the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Set up a TextAbsorber to extract text
            TextAbsorber absorber = new TextAbsorber
            {
                ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure)
            };

            // Extract text from only the first page (1‑based indexing)
            pdfDoc.Pages[1].Accept(absorber);

            // Save the extracted text to a .txt file
            File.WriteAllText(outputTxt, absorber.Text);
        }

        Console.WriteLine($"Text from first page saved to '{outputTxt}'.");
    }
}