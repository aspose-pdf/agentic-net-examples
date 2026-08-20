using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.md";   // Path to the Markdown file
        const string outputPath = "output.pdf"; // Desired PDF output path

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load options for Markdown format
        MdLoadOptions mdOptions = new MdLoadOptions();

        // Load the Markdown file and convert it to PDF.
        // The conversion preserves code blocks and other Markdown formatting by default.
        using (Document pdfDoc = new Document(inputPath, mdOptions))
        {
            // Save the resulting PDF.
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"Markdown file successfully converted to PDF: {outputPath}");
    }
}