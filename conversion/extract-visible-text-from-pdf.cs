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

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Create a TextAbsorber to extract visible text only
            TextAbsorber absorber = new TextAbsorber
            {
                ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure)
            };

            // Extract text from all pages
            pdfDoc.Pages.Accept(absorber);

            // Save the extracted text to a .txt file
            File.WriteAllText(outputTxt, absorber.Text);
        }

        Console.WriteLine($"Extracted text saved to '{outputTxt}'.");
    }
}