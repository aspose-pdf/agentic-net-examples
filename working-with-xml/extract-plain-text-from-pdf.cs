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

        // Load the PDF document (generated from XML) inside a using block for proper disposal
        using (Document doc = new Document(pdfPath))
        {
            // Set extraction options to obtain plain text (Pure formatting mode)
            TextExtractionOptions extractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);

            // Create a TextAbsorber with the specified options
            TextAbsorber absorber = new TextAbsorber(extractionOptions);

            // Extract text from all pages of the document
            doc.Pages.Accept(absorber);

            // Retrieve the extracted text
            string extractedText = absorber.Text;

            // Save the plain text to a file for indexing purposes
            File.WriteAllText(outputTxt, extractedText);
            Console.WriteLine($"Text extracted to '{outputTxt}'.");
        }
    }
}