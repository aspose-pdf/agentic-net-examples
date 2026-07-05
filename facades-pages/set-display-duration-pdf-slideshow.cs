using System;
using System.IO;
using Aspose.Pdf;
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

        // Example: set each page to display for a different number of seconds.
        // Here we simply use 5 seconds for odd pages and 10 seconds for even pages.
        try
        {
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                // Bind the source PDF document to the editor.
                editor.BindPdf(inputPdf);

                // Iterate through all pages (1‑based indexing).
                using (Document doc = new Document(inputPdf))
                {
                    int pageCount = doc.Pages.Count;

                    for (int i = 1; i <= pageCount; i++)
                    {
                        // Specify which page to edit.
                        editor.ProcessPages = new int[] { i };

                        // Set the desired display duration (in seconds) for this page.
                        editor.DisplayDuration = (i % 2 == 0) ? 10 : 5;

                        // Apply the change to the current page.
                        editor.ApplyChanges();
                    }
                }

                // Save the modified PDF with the new slide timings.
                editor.Save(outputPdf);
            }

            Console.WriteLine($"Slideshow PDF saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}