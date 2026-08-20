using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF (original booklet layout)
        const string inputPath = "input.pdf";
        // Output PDF with added margins
        const string outputPath = "output_booklet.pdf";

        // Pages to which the margin will be applied.
        // Set to null to process all pages, or specify an array like new int[] {1,2,3}
        int[] pages = null;

        // Desired margins: 15 % on each side (left, right, top, bottom)
        const double leftMargin  = 15;
        const double rightMargin = 15;
        const double topMargin   = 15;
        const double bottomMargin= 15;

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // Create the facade object (PdfFileEditor does NOT implement IDisposable)
        PdfFileEditor editor = new PdfFileEditor();

        // Apply the 15 % margins to the selected pages
        bool result = editor.AddMarginsPct(
            inputPath,      // source PDF
            outputPath,     // destination PDF
            pages,          // pages to process (null = all)
            leftMargin,     // left margin in percent
            rightMargin,    // right margin in percent
            topMargin,      // top margin in percent
            bottomMargin); // bottom margin in percent

        // No Close() method exists on PdfFileEditor; resources are released automatically.

        // Inform the user of the outcome
        Console.WriteLine(result
            ? $"Margins added successfully. Output saved to '{outputPath}'."
            : "Failed to add margins to the PDF.");
    }
}
