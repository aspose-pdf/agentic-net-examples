using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main(string[] args)
    {
        // Verify that a PDF path was provided
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: ExtractText <pdf-path>");
            return;
        }

        string pdfPath = args[0];

        // Ensure the file exists before attempting to open it
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: File not found - {pdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(pdfPath))
            {
                // Create a TextAbsorber to extract text from the document
                TextAbsorber absorber = new TextAbsorber();

                // Optional: configure extraction options (pure text without formatting)
                absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);

                // Apply the absorber to all pages
                doc.Pages.Accept(absorber);

                // Output the extracted text to standard output
                Console.Write(absorber.Text);
            }
        }
        catch (Exception ex)
        {
            // Report any unexpected errors
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}