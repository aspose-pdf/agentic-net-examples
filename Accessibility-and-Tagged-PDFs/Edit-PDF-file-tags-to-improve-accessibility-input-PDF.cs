using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;   // Provides ITaggedContent interface

class Program
{
    static void Main()
    {
        // Input and output file paths
        const string inputPath = "input.pdf";
        const string outputPath = "output_tagged.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        // Load the existing PDF document
        Document pdfDocument = new Document(inputPath);

        // Access the tagged content API (accessibility features)
        ITaggedContent tagged = pdfDocument.TaggedContent;

        // Set the natural language of the document (helps screen readers)
        tagged.SetLanguage("en-US");

        // Set a descriptive title for the PDF (used by assistive technologies)
        tagged.SetTitle("Accessible PDF Document");

        // Prepare the tagged structure for saving
        tagged.PreSave();

        // Save the modified PDF (uses the provided document-save rule)
        pdfDocument.Save(outputPath);

        Console.WriteLine($"Successfully saved tagged PDF to '{outputPath}'.");
    }
}