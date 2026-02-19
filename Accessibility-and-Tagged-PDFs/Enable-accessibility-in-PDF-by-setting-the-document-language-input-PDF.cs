using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;

class Program
{
    static void Main(string[] args)
    {
        // Input and output PDF file paths
        string inputPath = "input.pdf";
        string outputPath = "output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing PDF document
            Document pdfDocument = new Document(inputPath);

            // Obtain the tagged content interface
            ITaggedContent tagged = pdfDocument.TaggedContent;

            // Set the natural language for the document (e.g., English - United States)
            tagged.SetLanguage("en-US");

            // Save the updated PDF
            pdfDocument.Save(outputPath);
            Console.WriteLine($"PDF saved with language set to en-US: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}