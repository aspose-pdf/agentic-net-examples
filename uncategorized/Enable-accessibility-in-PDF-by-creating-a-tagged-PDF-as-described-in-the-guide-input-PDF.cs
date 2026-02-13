using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;

class Program
{
    static void Main(string[] args)
    {
        // Input PDF (must exist)
        const string inputPath = "input.pdf";
        // Output PDF with accessibility (tagged) enabled
        const string outputPath = "output_tagged.pdf";

        // Verify the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        // Load the existing PDF document
        Document pdfDocument = new Document(inputPath);

        // Obtain the tagged‑content interface
        ITaggedContent tagged = pdfDocument.TaggedContent;

        // Set accessibility properties
        tagged.SetLanguage("en-US");          // Natural language of the document
        tagged.SetTitle("Accessible PDF");    // Document title for assistive technologies

        // Prepare the logical structure before saving
        tagged.PreSave();

        // Save the modified PDF (uses the provided lifecycle rule)
        pdfDocument.Save(outputPath);
    }
}