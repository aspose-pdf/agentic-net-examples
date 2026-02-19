using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input PDF path and output PDF path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.pdf> <output.pdf>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Verify that the input file exists
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

            // Define the natural language for the PDF (e.g., English - United States)
            tagged.SetLanguage("en-US");

            // Optionally set a title for accessibility purposes
            // tagged.SetTitle("Sample PDF with Language Defined");

            // Save the modified PDF
            pdfDocument.Save(outputPath);
            Console.WriteLine($"PDF saved successfully with language set: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}