using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;   // Provides ITaggedContent interface

class Program
{
    static void Main(string[] args)
    {
        // Paths for the source PDF and the output PDF
        const string inputPath = "input.pdf";
        const string outputPath = "output_accessible.pdf";

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input PDF not found at '{inputPath}'.");
            return;
        }

        // Load the PDF document
        Document pdfDocument = new Document(inputPath);

        // Obtain the tagged content interface; if the document is not already tagged,
        // Aspose.Pdf will create a minimal structure automatically.
        ITaggedContent tagged = pdfDocument.TaggedContent;
        if (tagged == null)
        {
            Console.Error.WriteLine("Error: Unable to obtain tagged content from the PDF.");
            return;
        }

        // Define the natural language of the document (e.g., English - United States)
        tagged.SetLanguage("en-US");

        // Optionally set a title for the PDF (helps screen readers)
        tagged.SetTitle("Accessible PDF");

        // Prepare the tagged structure before saving
        tagged.PreSave();

        // Save the modified PDF (uses the document-save rule)
        pdfDocument.Save(outputPath);

        Console.WriteLine($"Accessible PDF saved to '{outputPath}'.");
    }
}