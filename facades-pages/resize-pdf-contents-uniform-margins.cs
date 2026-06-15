using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source and destination PDF files
        const string sourcePdf = "input.pdf";
        const string destinationPdf = "output.pdf";

        // Verify that the source file exists
        if (!System.IO.File.Exists(sourcePdf))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdf}");
            return;
        }

        // Create an instance of PdfFileEditor (does NOT implement IDisposable)
        PdfFileEditor fileEditor = new PdfFileEditor();

        // Define uniform margins of 10% on all sides.
        // MarginsPercent creates a ContentsResizeParameters object where the
        // content size is calculated automatically based on the margins.
        PdfFileEditor.ContentsResizeParameters parameters =
            PdfFileEditor.ContentsResizeParameters.MarginsPercent(
                left:   10,   // left margin 10% of page width
                right:  10,   // right margin 10% of page width
                top:    10,   // top margin 10% of page height
                bottom: 10);  // bottom margin 10% of page height

        // Resize the contents of all pages (pages = null) using the defined parameters.
        // The method returns true on success.
        bool result = fileEditor.ResizeContents(
            source: sourcePdf,
            destination: destinationPdf,
            pages: null,          // null processes every page in the document
            parameters: parameters);

        if (result)
        {
            Console.WriteLine($"Contents resized successfully. Output saved to '{destinationPdf}'.");
        }
        else
        {
            Console.Error.WriteLine("Failed to resize contents.");
        }
    }
}