using System;
using System.IO;
using System.Drawing;                     // System.Drawing.Rectangle is required by PdfContentEditor
using Aspose.Pdf;
using Aspose.Pdf.Facades;                // Facade classes for editing PDF content
using Aspose.Pdf;                        // EpubSaveOptions resides here

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // source PDF file
        const string outputEpub = "output.epub"; // destination EPUB file

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // ---------- Add sticky (text) annotation ----------
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                // Bind the existing PDF file to the editor
                editor.BindPdf(inputPdf);

                // Define the annotation rectangle (x, y, width, height) in points.
                // Use the fully‑qualified System.Drawing.Rectangle to avoid ambiguity.
                System.Drawing.Rectangle annotRect = new System.Drawing.Rectangle(100, 500, 200, 100);

                // Create a sticky note (text annotation) on page 1.
                // Parameters: rectangle, contents, title, open (true = displayed), author, pageNumber (1‑based)
                editor.CreateText(annotRect, "This is a sticky note added via Aspose.Pdf.", "Sticky Note", true, "Author", 1);

                // Retrieve the modified Document instance from the editor.
                Document doc = editor.Document;

                // ---------- Convert the annotated PDF to EPUB ----------
                using (doc) // Ensure the Document is disposed properly.
                {
                    // Initialize EPUB save options (required when saving to a non‑PDF format).
                    EpubSaveOptions epubOptions = new EpubSaveOptions
                    {
                        Title = "PDF with Sticky Annotations"
                        // ContentRecognitionMode can be set if needed, e.g.:
                        // ContentRecognitionMode = EpubSaveOptions.RecognitionMode.PdfFlow
                    };

                    // Save the document as EPUB using the explicit options.
                    doc.Save(outputEpub, epubOptions);
                }
            }

            Console.WriteLine($"EPUB with sticky annotations saved to '{outputEpub}'.");
        }
        // GDI+ (System.Drawing) is Windows‑only; handle the platform limitation gracefully.
        catch (TypeInitializationException)
        {
            Console.Error.WriteLine("System.Drawing (GDI+) is unavailable on this platform. Sticky annotation creation skipped.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}