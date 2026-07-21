using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the output PDF
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // Create the PdfFileEditor facade
        PdfFileEditor fileEditor = new PdfFileEditor();

        // Add 20 % margins on all sides of every page.
        // Passing null for the pages array processes all pages.
        bool success = fileEditor.AddMarginsPct(
            source:      inputPath,
            destination: outputPath,
            pages:       null,   // all pages
            leftMargin:  20,     // 20 % left margin
            rightMargin: 20,     // 20 % right margin
            topMargin:   20,     // 20 % top margin
            bottomMargin:20);    // 20 % bottom margin

        // Report the result
        if (success)
            Console.WriteLine($"Margins added successfully. Output saved to '{outputPath}'.");
        else
            Console.Error.WriteLine("Failed to add margins to the PDF.");
    }
}