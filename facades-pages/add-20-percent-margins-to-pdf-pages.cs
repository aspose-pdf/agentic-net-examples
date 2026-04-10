using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_margin.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // Create an instance of PdfFileEditor (facade class)
        PdfFileEditor editor = new PdfFileEditor();

        // Add 20 % margins on all sides (left, right, top, bottom)
        // The method works with page indexes that start at 1.
        // Passing null processes all pages in the document.
        bool success = editor.AddMarginsPct(
            source:      inputPath,
            destination: outputPath,
            pages:       null,      // all pages
            leftMargin:  20,        // 20 % of page width
            rightMargin: 20,        // 20 % of page width
            topMargin:   20,        // 20 % of page height
            bottomMargin:20);       // 20 % of page height

        if (success)
        {
            Console.WriteLine($"Successfully created '{outputPath}' with 20 % margins.");
        }
        else
        {
            Console.Error.WriteLine("Failed to add margins to the PDF.");
        }
    }
}