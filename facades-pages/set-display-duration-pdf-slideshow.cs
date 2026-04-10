using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "slideshow.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Use PdfPageEditor facade to edit page properties.
        // The facade implements IDisposable, so wrap it in a using block.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the source PDF.
            editor.BindPdf(inputPdf);

            // Set the display duration (in seconds) for all pages.
            // This controls how long each page is shown during a presentation.
            editor.DisplayDuration = 5; // 5 seconds per page

            // Apply the changes to the document.
            editor.ApplyChanges();

            // Save the modified PDF.
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Slideshow PDF saved to '{outputPdf}'.");
    }
}