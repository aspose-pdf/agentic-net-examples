using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_zoomed.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Create the PdfPageEditor facade and bind the source PDF
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPdf);

            // Retrieve total number of pages (1‑based indexing)
            int pageCount = editor.GetPages();

            // Iterate through each page and assign a specific zoom factor
            for (int i = 1; i <= pageCount; i++)
            {
                // Restrict editing to the current page only
                editor.ProcessPages = new int[] { i };

                // Example zoom logic: 50% for odd pages, 100% for even pages
                editor.Zoom = (i % 2 == 1) ? 0.5f : 1.0f;

                // Apply the zoom change to the selected page
                editor.ApplyChanges();
            }

            // Save the modified document
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Zoomed PDF saved to '{outputPdf}'.");
    }
}