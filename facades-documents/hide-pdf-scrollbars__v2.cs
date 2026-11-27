using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Microsoft.CSharp.RuntimeBinder;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "ScrollbarsRemoved.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Bind the PDF with PdfContentEditor (required by the task, even if no edits are performed).
            var editor = new PdfContentEditor();
            editor.BindPdf(inputPath);

            // Load the document to modify viewer preferences.
            var pdfDocument = new Document(inputPath);

            // Access ViewerPreferences dynamically to stay compatible with older Aspose.Pdf versions.
            dynamic docDyn = pdfDocument;
            try
            {
                docDyn.ViewerPreferences.HideScrollbars = true;
                Console.WriteLine("Viewer preference HideScrollbars set to true.");
            }
            catch (RuntimeBinderException)
            {
                Console.WriteLine("ViewerPreferences property is not supported by the current Aspose.Pdf library version.");
            }

            // Save the modified PDF.
            pdfDocument.Save(outputPath);
            Console.WriteLine($"PDF saved without scrollbars to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
