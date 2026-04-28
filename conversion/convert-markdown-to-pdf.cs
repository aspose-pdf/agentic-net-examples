using System;
using System.IO;
using Aspose.Pdf; // Aspose.Pdf namespace contains Document, MdLoadOptions, etc.

class Program
{
    static void Main()
    {
        // Input Markdown file and desired PDF output file.
        const string inputPath  = "input.md";
        const string outputPath = "output.pdf";

        // Verify that the source file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize load options for Markdown conversion.
        MdLoadOptions mdLoadOptions = new MdLoadOptions();

        // Load the Markdown file and convert it to a PDF document.
        // The Document constructor with (string, LoadOptions) handles the conversion.
        using (Document pdfDocument = new Document(inputPath, mdLoadOptions))
        {
            // Save the resulting PDF. No SaveOptions needed because the target format is PDF.
            pdfDocument.Save(outputPath);
        }

        Console.WriteLine($"Markdown file successfully converted to PDF: {outputPath}");
    }
}