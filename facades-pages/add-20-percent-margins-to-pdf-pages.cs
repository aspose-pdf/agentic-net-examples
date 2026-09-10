using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source PDF exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create the facade that will modify page contents.
        PdfFileEditor fileEditor = new PdfFileEditor();

        // Add 20 % margins on all four sides of each page.
        // Passing null for the pages array processes every page in the document.
        bool result = fileEditor.AddMarginsPct(
            inputPath,          // source PDF
            outputPath,         // destination PDF
            null,               // all pages
            20,                 // left margin (percent)
            20,                 // right margin (percent)
            20,                 // top margin (percent)
            20);                // bottom margin (percent)

        if (result)
        {
            Console.WriteLine($"Successfully added margins. Output saved to '{outputPath}'.");
        }
        else
        {
            Console.Error.WriteLine("Failed to add margins to the PDF.");
        }
    }
}