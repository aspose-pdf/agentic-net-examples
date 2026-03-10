using System;
using System.IO;
using System.Drawing;                     // Required for Rectangle (System.Drawing.Rectangle)
using Aspose.Pdf;                         // Core PDF types and HtmlSaveOptions
using Aspose.Pdf.Facades;                 // PdfContentEditor for annotation editing

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";          // Existing PDF
        const string outputHtmlPath = "output.html";       // Desired HTML output
        const int    pageNumber    = 1;                    // Page where the link will be placed (1‑based)
        const string url           = "https://www.example.com";

        // Define the clickable rectangle (coordinates are in points)
        // left, top, width, height
        System.Drawing.Rectangle linkRect = new System.Drawing.Rectangle(100, 700, 200, 50);

        // -----------------------------------------------------------------
        // 1. Add a web link annotation using PdfContentEditor (facade API)
        // -----------------------------------------------------------------
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the existing PDF
            editor.BindPdf(inputPdfPath);

            // Create a web link on the specified page.
            // Overload without Color avoids System.Drawing.Color usage.
            editor.CreateWebLink(linkRect, url, pageNumber);

            // Save the modified PDF to a temporary file.
            // This file will be the source for HTML conversion.
            string tempPdfPath = Path.Combine(Path.GetDirectoryName(outputHtmlPath) ?? ".", "temp_modified.pdf");
            editor.Save(tempPdfPath);

            // -------------------------------------------------------------
            // 2. Convert the modified PDF to HTML (requires explicit options)
            // -------------------------------------------------------------
            try
            {
                using (Document pdfDoc = new Document(tempPdfPath))
                {
                    HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                    {
                        // Ensure HTML output is generated (required on non‑Windows platforms)
                        // No additional settings are mandatory for a basic conversion.
                    };

                    pdfDoc.Save(outputHtmlPath, htmlOpts);
                }

                // Clean up the temporary PDF file
                if (File.Exists(tempPdfPath))
                    File.Delete(tempPdfPath);

                Console.WriteLine($"HTML file with link annotation saved to '{outputHtmlPath}'.");
            }
            catch (TypeInitializationException)
            {
                // HTML conversion relies on GDI+ and is Windows‑only.
                Console.WriteLine("HTML conversion requires Windows (GDI+). Skipping HTML export.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error during HTML conversion: {ex.Message}");
            }
        }
    }
}
