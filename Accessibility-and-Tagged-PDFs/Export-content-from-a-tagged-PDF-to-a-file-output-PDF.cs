using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the tagged PDF document
            Document pdfDocument = new Document(inputPath);

            // Access the tagged content interface
            ITaggedContent taggedContent = pdfDocument.TaggedContent;

            // Optional: set language and title for the exported PDF
            taggedContent.SetLanguage("en-US");
            taggedContent.SetTitle("Exported Tagged PDF");

            // Prepare the tagged structure for saving
            taggedContent.PreSave();

            // Save the document (including its tagged content) to a new file
            pdfDocument.Save(outputPath);

            Console.WriteLine($"Tagged PDF exported successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}