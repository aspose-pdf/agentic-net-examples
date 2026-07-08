using System;
using System.IO;
using Aspose.Pdf; // MdLoadOptions and Document are in this namespace

class Program
{
    static void Main()
    {
        // Paths to the source Markdown file and the destination PDF file
        const string inputPath  = "input.md";
        const string outputPath = "output.pdf";

        // Verify that the Markdown file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize load options specific for Markdown conversion
        MdLoadOptions mdLoadOptions = new MdLoadOptions();

        // Load the Markdown file with the specified options and convert it to PDF
        // The Document constructor automatically performs the conversion
        using (Document pdfDocument = new Document(inputPath, mdLoadOptions))
        {
            // Save the resulting PDF; code blocks are preserved by default
            pdfDocument.Save(outputPath);
        }

        Console.WriteLine($"Markdown file successfully converted to PDF: {outputPath}");
    }
}