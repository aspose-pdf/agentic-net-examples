using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "slideshow.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Obtain the total number of pages via Document (PdfPageEditor has no GetPageCount)
        int pageCount;
        using (var doc = new Document(inputPath))
        {
            pageCount = doc.Pages.Count;
        }

        // Use PdfPageEditor to set per‑page display duration
        using (var editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);

            for (int i = 1; i <= pageCount; i++)
            {
                // Select the current page
                editor.ProcessPages = new int[] { i };
                // Set the desired duration (seconds)
                editor.DisplayDuration = i * 2;
                // Apply the change to the selected page(s)
                editor.ApplyChanges();
            }

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Slideshow PDF saved to '{outputPath}'.");
    }
}
