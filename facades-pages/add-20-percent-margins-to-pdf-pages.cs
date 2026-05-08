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

        // Create the facade that will modify the PDF
        PdfFileEditor editor = new PdfFileEditor();

        // Add a 20% margin on all four sides for every page (null pages array = all pages)
        bool success = editor.AddMarginsPct(
            source: inputPath,
            destination: outputPath,
            pages: null,
            leftMargin: 20,
            rightMargin: 20,
            topMargin: 20,
            bottomMargin: 20);

        if (!success)
        {
            Console.Error.WriteLine("Failed to add margins to the PDF.");
        }
        else
        {
            Console.WriteLine($"Margins added successfully. Output saved to '{outputPath}'.");
        }
    }
}