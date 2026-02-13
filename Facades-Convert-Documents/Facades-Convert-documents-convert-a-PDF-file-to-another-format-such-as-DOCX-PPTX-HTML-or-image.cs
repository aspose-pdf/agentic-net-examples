using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input PDF path and output path with desired extension (e.g., .docx, .pptx, .html, .png)
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: ConvertPdf <input.pdf> <output.ext>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        try
        {
            // Load the existing PDF document
            Document pdfDocument = new Document(inputPath);

            // Save the document to the target format.
            // Aspose.Pdf infers the output format from the file extension.
            pdfDocument.Save(outputPath); // document-save rule

            Console.WriteLine($"Conversion completed successfully: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}