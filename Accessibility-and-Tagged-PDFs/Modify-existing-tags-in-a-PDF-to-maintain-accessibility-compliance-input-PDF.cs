using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;

class Program
{
    static void Main(string[] args)
    {
        // Input and output PDF file paths
        const string inputPath = "input.pdf";
        const string outputPath = "output_tagged.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing PDF document
            Document pdfDocument = new Document(inputPath);

            // Access the tagged content interface
            ITaggedContent tagged = pdfDocument.TaggedContent;

            // Update document language and title for accessibility compliance
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Updated PDF Title");

            // Prepare the tagged structure before saving
            tagged.PreSave();

            // Save changes to the tagged structure
            tagged.Save();

            // Save the modified PDF document (using the provided document-save rule)
            pdfDocument.Save(outputPath);

            Console.WriteLine($"Successfully updated tags and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}