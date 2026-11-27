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

        // Load the PDF document with deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Extract text from all pages using TextAbsorber
            TextAbsorber absorber = new TextAbsorber();
            // Optional: configure extraction options if needed
            // absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);
            pdfDoc.Pages.Accept(absorber);
            string allText = absorber.Text;

            // Write the concatenated text to a single .txt file
            File.WriteAllText(outputTxt, allText);
        }

        Console.WriteLine($"Extracted text saved to '{outputTxt}'.");
    }
}