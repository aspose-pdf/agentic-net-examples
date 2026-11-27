using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfAnnotationEditor does not implement IDisposable, so no using block.
        var editor = new PdfAnnotationEditor();

        // Bind the existing PDF file.
        editor.BindPdf(inputPath);

        // Ensure the document has at least 3 pages.
        if (editor.Document.Pages.Count < 3)
        {
            Console.WriteLine("Document has fewer than 3 pages.");
            return;
        }

        // Get page 3 (Aspose.Pdf pages are 1‑based).
        Page page = editor.Document.Pages[3];

        if (page.Annotations == null || page.Annotations.Count == 0)
        {
            Console.WriteLine("No annotations found on page 3.");
        }
        else
        {
            // First annotation on the page (collection is zero‑based).
            Annotation annotation = page.Annotations[0];

            // Set annotation color to green.
            annotation.Color = Color.Green;
            // The Document is already updated; no extra call is required.
        }

        // Save the updated PDF to a new file.
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
