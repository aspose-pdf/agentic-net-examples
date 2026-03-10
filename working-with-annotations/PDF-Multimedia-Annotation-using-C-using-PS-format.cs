using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using System.Drawing; // needed for Rectangle used by PdfContentEditor

// Alias to disambiguate the Rectangle type between System.Drawing and Aspose.Pdf
using DrawingRectangle = System.Drawing.Rectangle;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";               // source PDF
        const string outputPdf = "output_multimedia.pdf"; // result PDF
        const string mediaPath = "sample.mp4";            // video or audio file to embed

        // Verify files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(mediaPath))
        {
            Console.Error.WriteLine($"Media file not found: {mediaPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Define the annotation rectangle (System.Drawing.Rectangle is required by CreateMovie)
            DrawingRectangle rect = new DrawingRectangle(100, 500, 200, 150);

            // Initialize the content editor, bind the document and add the multimedia annotation
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                editor.BindPdf(doc);

                // Add a movie (multimedia) annotation on page 1
                // Parameters: rectangle, path to media file, page number (1‑based)
                editor.CreateMovie(rect, mediaPath, 1);

                // Save the modified document. PS format is input‑only; we save as PDF.
                editor.Save(outputPdf);
            }
        }

        Console.WriteLine($"Multimedia annotation added. Saved to '{outputPdf}'.");
    }
}
