using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;   // ITaggedContent interface

class Program
{
    static void Main()
    {
        // Paths for input and output PDFs
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        Document pdfDocument = new Document(inputPath);

        // Access the tagged content (accessibility structure) of the document
        ITaggedContent tagged = pdfDocument.TaggedContent;
        if (tagged != null)
        {
            // Define the natural language for the document (e.g., English - United States)
            tagged.SetLanguage("en-US");
        }
        else
        {
            Console.WriteLine("The PDF does not contain tagged content; language cannot be set.");
        }

        // Save the modified PDF
        pdfDocument.Save(outputPath);
        Console.WriteLine($"PDF saved with language set to en-US at: {outputPath}");
    }
}