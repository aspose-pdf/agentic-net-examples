using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputHtml = "output.html";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal.
            using (Document doc = new Document(inputPdf))
            {
                // Get the first page (Aspose.Pdf uses 1‑based indexing).
                Page page = doc.Pages[1];

                // Define the rectangle area to be highlighted.
                // Fully qualified type to avoid ambiguity with System.Drawing.
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 520);

                // Create the highlight annotation on the specified page and rectangle.
                HighlightAnnotation highlight = new HighlightAnnotation(page, rect)
                {
                    // Set the highlight color (use Aspose.Pdf.Color for cross‑platform compatibility).
                    Color = Aspose.Pdf.Color.Yellow
                };

                // Add the annotation to the page's annotation collection.
                page.Annotations.Add(highlight);

                // Prepare HTML save options (required for non‑PDF output).
                HtmlSaveOptions htmlOptions = new HtmlSaveOptions();

                // HTML conversion relies on GDI+ and is Windows‑only.
                // Wrap the save call in a try‑catch to handle non‑Windows platforms gracefully.
                try
                {
                    doc.Save(outputHtml, htmlOptions);
                    Console.WriteLine($"HTML file saved to '{outputHtml}'.");
                }
                catch (TypeInitializationException)
                {
                    Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}