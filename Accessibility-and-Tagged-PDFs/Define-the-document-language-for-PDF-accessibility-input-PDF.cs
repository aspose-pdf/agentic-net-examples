using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output PDF file paths
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: program <input.pdf> <output.pdf>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Verify that the source PDF exists
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

            // Define the natural language for the document (e.g., English – United States)
            tagged.SetLanguage("en-US");

            // Optionally, also set the language on the root element
            // tagged.RootElement.Lang = "en-US";

            // Save the updated PDF
            pdfDocument.Save(outputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}