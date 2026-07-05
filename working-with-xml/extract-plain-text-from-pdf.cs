using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string outputTxt = "extracted.txt";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        using (Document doc = new Document(pdfPath))
        {
            // Configure extraction to produce plain text
            TextExtractionOptions extractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);
            TextAbsorber absorber = new TextAbsorber(extractionOptions);

            // Extract text from all pages
            doc.Pages.Accept(absorber);

            // Save extracted text
            File.WriteAllText(outputTxt, absorber.Text);
            Console.WriteLine($"Extracted text saved to '{outputTxt}'.");
        }
    }
}