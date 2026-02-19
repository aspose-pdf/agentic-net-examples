using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged; // Provides ITaggedContent for accessibility features

class Program
{
    static void Main(string[] args)
    {
        // Paths to the input and output PDF files
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the input PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing PDF document
            Document pdfDocument = new Document(inputPath);

            // Set the document title to improve accessibility.
            // Prefer the tagged-content API if available; otherwise fall back to standard metadata.
            if (pdfDocument.TaggedContent != null)
            {
                pdfDocument.TaggedContent.SetTitle("Sample Document Title");
            }
            else
            {
                pdfDocument.Info.Title = "Sample Document Title";
            }

            // Save the modified PDF (uses the provided document-save rule)
            pdfDocument.Save(outputPath);

            Console.WriteLine($"PDF saved with title set: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}