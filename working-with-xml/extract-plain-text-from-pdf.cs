using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";          // PDF generated from XML
        const string outputTextPath = "extracted.txt";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        try
        {
            // Open the PDF inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(pdfPath))
            {
                // TextAbsorber extracts plain text from the document
                TextAbsorber absorber = new TextAbsorber();

                // Configure extraction to return pure text without formatting tags
                absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);

                // Apply the absorber to all pages
                pdfDoc.Pages.Accept(absorber);

                // Retrieve the extracted text
                string plainText = absorber.Text;

                // Optionally write the text to a file for indexing
                File.WriteAllText(outputTextPath, plainText);

                Console.WriteLine($"Text extracted and saved to '{outputTextPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during extraction: {ex.Message}");
        }
    }
}