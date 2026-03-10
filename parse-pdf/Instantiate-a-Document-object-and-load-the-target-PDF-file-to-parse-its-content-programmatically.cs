using System;
using System.IO;
using Aspose.Pdf;               // Document, Page, etc.
using Aspose.Pdf.Text;          // TextAbsorber for text extraction

class Program
{
    static void Main()
    {
        // Path to the PDF file to be loaded
        const string inputPath = "sample.pdf";

        // Verify that the file exists before attempting to load it
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use a using block to ensure the Document is disposed properly
        using (Document doc = new Document(inputPath))
        {
            // Extract all text from the document using TextAbsorber (recommended API)
            TextAbsorber absorber = new TextAbsorber();
            doc.Pages.Accept(absorber);
            string extractedText = absorber.Text;

            // Output the extracted text (or process it as needed)
            Console.WriteLine("Extracted Text:");
            Console.WriteLine(extractedText);
        }
    }
}