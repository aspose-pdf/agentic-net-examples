using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "slideshow.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF to the PdfPageEditor facade.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);

            // Total number of pages in the document.
            int pageCount = editor.GetPages();

            // Set a display duration (in seconds) for each page.
            // Here each page is set to 5 seconds; adjust as needed.
            for (int i = 1; i <= pageCount; i++)
            {
                // Restrict changes to the current page.
                editor.ProcessPages = new int[] { i };

                // Duration for the current page.
                editor.DisplayDuration = 5; // seconds

                // Apply the change to the bound document.
                editor.ApplyChanges();
            }

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Slideshow PDF saved to '{outputPath}'.");
    }
}