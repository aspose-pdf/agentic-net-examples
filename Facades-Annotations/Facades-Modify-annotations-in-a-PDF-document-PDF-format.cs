using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        try
        {
            // Load the document to obtain the actual page count (cross‑platform, no GDI+)
            Document doc = new Document(inputPath);
            int pageCount = doc.Pages.Count;

            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                // Bind the PDF to the editor
                editor.BindPdf(inputPath);

                // Delete all existing annotations
                editor.DeleteAnnotations();

                // Modify annotation author only if the document has pages
                if (pageCount > 0)
                {
                    int start = 1;                                 // first page (1‑based index)
                    int end = Math.Min(2, pageCount);               // ensure we do not exceed page count
                    editor.ModifyAnnotationsAuthor(start, end, "NewAuthor", "NewTitle");
                }

                // Save the modified PDF
                editor.Save(outputPath);
            }

            Console.WriteLine($"Annotations have been processed and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
