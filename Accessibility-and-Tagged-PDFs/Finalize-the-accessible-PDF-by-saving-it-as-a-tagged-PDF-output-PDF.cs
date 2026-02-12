using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;

class Program
{
    static void Main(string[] args)
    {
        // Paths for the source PDF and the tagged output PDF
        const string inputPath = "input.pdf";
        const string outputPath = "output_tagged.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing PDF document
            Document pdfDocument = new Document(inputPath);

            // Obtain the tagged‑content interface
            ITaggedContent tagged = pdfDocument.TaggedContent;

            // Set language and title for accessibility
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Accessible PDF Example");

            // Perform any necessary pre‑save processing for tagged content
            tagged.PreSave();

            // Save the document – this will embed the tagging information
            pdfDocument.Save(outputPath);   // document-save rule applied here

            Console.WriteLine($"Tagged PDF successfully saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}