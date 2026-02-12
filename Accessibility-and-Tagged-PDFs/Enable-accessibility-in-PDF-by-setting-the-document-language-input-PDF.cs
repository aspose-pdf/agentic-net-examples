using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;

class Program
{
    static void Main()
    {
        // Paths for input and output PDFs
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

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

            // Set the natural language of the document (e.g., English - United States)
            // This makes the PDF accessible for assistive technologies.
            pdfDocument.TaggedContent.SetLanguage("en-US");

            // Save the modified PDF using the standard save rule
            pdfDocument.Save(outputPath);

            Console.WriteLine($"Successfully saved PDF with language set to 'en-US' at: {outputPath}");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}