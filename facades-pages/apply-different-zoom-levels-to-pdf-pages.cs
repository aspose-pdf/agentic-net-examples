using System;
using System.IO;
using Aspose.Pdf.Facades; // Facade classes for PDF editing

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPdf  = "input.pdf";
        const string outputPdf = "zoomed_output.pdf";

        // Define a zoom factor for each page (1.0 = 100%)
        // Example: page 1 -> 100%, page 2 -> 150%, page 3 -> 75%
        float[] pageZooms = { 1.0f, 1.5f, 0.75f };

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the number of zoom values matches the number of pages
        // We'll determine the page count after binding the PDF.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Bind the source PDF file to the editor facade
            editor.BindPdf(inputPdf);

            // Get total page count (pages are 1‑based)
            int pageCount = editor.GetPages();

            if (pageZooms.Length != pageCount)
            {
                Console.Error.WriteLine($"Zoom array length ({pageZooms.Length}) does not match page count ({pageCount}).");
                return;
            }

            // Iterate over each page and apply its specific zoom factor
            for (int i = 1; i <= pageCount; i++)
            {
                // Restrict editing to the current page only
                editor.ProcessPages = new int[] { i };

                // Set the zoom coefficient for this page
                editor.Zoom = pageZooms[i - 1];

                // Apply the change to the bound document
                editor.ApplyChanges();
            }

            // Save the modified PDF to the output path
            editor.Save(outputPdf);
            // Close the facade (optional, as using will dispose it)
            editor.Close();
        }

        Console.WriteLine($"Zoomed PDF saved to '{outputPdf}'.");
    }
}