using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;

class Program
{
    static void Main()
    {
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

            // Access the tagged‑content interface
            ITaggedContent tagged = pdfDocument.TaggedContent;

            // Set accessibility metadata
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Accessible PDF");

            // Prepare and save the tagged structure
            tagged.PreSave();
            tagged.Save();

            // Save the modified PDF to a new file
            pdfDocument.Save(outputPath);

            Console.WriteLine($"Successfully created tagged PDF: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}