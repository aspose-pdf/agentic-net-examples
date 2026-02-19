using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;

class ExportTaggedPdf
{
    static void Main(string[] args)
    {
        // Paths for the source tagged PDF and the exported PDF
        const string inputPath = "input_tagged.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        try
        {
            // Load the tagged PDF document
            Document pdfDocument = new Document(inputPath);

            // Obtain the tagged content interface
            ITaggedContent tagged = pdfDocument.TaggedContent;

            // Optional: set language and title for the exported document
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Exported PDF");

            // Prepare the tagged structure before saving
            tagged.PreSave();

            // Save the document to the desired output file (lifecycle rule)
            pdfDocument.Save(outputPath);

            Console.WriteLine($"Tagged PDF exported successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}