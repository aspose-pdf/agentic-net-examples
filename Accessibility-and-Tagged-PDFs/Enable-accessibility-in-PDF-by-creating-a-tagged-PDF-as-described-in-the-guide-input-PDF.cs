using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;

class Program
{
    static void Main(string[] args)
    {
        // Input and output PDF paths (adjust as needed)
        const string inputPath = "input.pdf";
        const string outputPath = "output_tagged.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        try
        {
            // Load the existing PDF document
            Document pdfDocument = new Document(inputPath);

            // Access the tagged content interface
            ITaggedContent tagged = pdfDocument.TaggedContent;

            // Set document language and title for accessibility
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Accessible PDF Document");

            // Prepare the tagged structure before saving
            tagged.PreSave();

            // Persist any changes made to the tagged content
            tagged.Save();

            // Save the modified PDF (using the provided lifecycle rule)
            pdfDocument.Save(outputPath);

            Console.WriteLine($"Successfully created tagged PDF: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}