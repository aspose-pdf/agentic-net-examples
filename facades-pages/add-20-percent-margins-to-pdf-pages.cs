using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Initialize the facade that provides page editing capabilities
        PdfFileEditor editor = new PdfFileEditor();

        // Add a 20% margin on all four sides for every page (pages = null processes all pages)
        bool result = editor.AddMarginsPct(
            source: inputPath,
            destination: outputPath,
            pages: null,
            leftMargin: 20,   // 20% of page width
            rightMargin: 20,  // 20% of page width
            topMargin: 20,    // 20% of page height
            bottomMargin: 20  // 20% of page height
        );

        // Report the outcome
        if (result)
        {
            Console.WriteLine($"Successfully added 20% margins. Output saved to '{outputPath}'.");
        }
        else
        {
            Console.Error.WriteLine("Failed to add margins to the PDF.");
        }
    }
}