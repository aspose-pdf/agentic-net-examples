using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the resulting PDF with added margins
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_margins.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create an instance of PdfFileEditor (facade class)
        PdfFileEditor fileEditor = new PdfFileEditor();

        // Add 20 % margins on all four sides for all pages.
        // Passing null for the pages array processes every page in the document.
        bool result = fileEditor.AddMarginsPct(
            source: inputPath,
            destination: outputPath,
            pages: null,          // null = all pages
            leftMargin: 20,       // 20 % left margin
            rightMargin: 20,      // 20 % right margin
            topMargin: 20,        // 20 % top margin
            bottomMargin: 20);    // 20 % bottom margin

        // Report the outcome
        if (result)
        {
            Console.WriteLine($"Margins added successfully. Output saved to '{outputPath}'.");
        }
        else
        {
            Console.Error.WriteLine("Failed to add margins to the PDF.");
        }
    }
}