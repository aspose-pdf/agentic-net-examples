using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;

class Program
{
    static void Main(string[] args)
    {
        // Input and output PDF paths
        const string inputPath = "input.pdf";
        const string outputPath = "output_tagged.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        try
        {
            // Load the existing PDF document
            Document pdfDocument = new Document(inputPath);

            // Obtain the tagged‑content interface
            ITaggedContent tagged = pdfDocument.TaggedContent;

            // Optional: set document language and title for accessibility
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Accessible PDF Document");

            // Prepare the logical structure and save it into the PDF
            tagged.PreSave();   // performs necessary pre‑save operations
            tagged.Save();      // writes the tagged content to the document

            // Finally, save the PDF file (uses the document-save rule)
            pdfDocument.Save(outputPath);

            Console.WriteLine($"Tagged PDF saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}