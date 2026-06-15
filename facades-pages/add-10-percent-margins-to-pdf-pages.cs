using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source PDF (100 pages) and the destination PDF
        const string inputPath  = "input.pdf";
        const string outputPath = "output_resized.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create an instance of PdfFileEditor (facade class)
        PdfFileEditor fileEditor = new PdfFileEditor();

        // Add 10 % margins on all four sides for every page.
        // Passing null for the pages array means “process all pages”.
        bool result = fileEditor.AddMarginsPct(
            source:      inputPath,
            destination: outputPath,
            pages:       null,   // all pages
            leftMargin:  10,     // 10 % left margin
            rightMargin: 10,     // 10 % right margin
            topMargin:   10,     // 10 % top margin
            bottomMargin:10);    // 10 % bottom margin

        // Report the outcome
        if (result)
            Console.WriteLine($"Successfully resized contents. Output saved to '{outputPath}'.");
        else
            Console.Error.WriteLine("Failed to resize PDF contents.");
    }
}