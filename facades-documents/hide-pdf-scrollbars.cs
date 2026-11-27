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
        const string outputPath = "ScrollbarsHidden.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Bind the PDF using PdfContentEditor as required.
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(inputPath);

            // Load the PDF with the high‑level Document API.
            Document pdfDoc = new Document(inputPath);

            // ViewerPreferences were introduced in later Aspose.Pdf versions.
            // Use dynamic access to keep the code compatible with older versions.
            dynamic docDyn = pdfDoc;
            try
            {
                // Hide scrollbars if the property exists.
                docDyn.ViewerPreferences.HideScrollbars = true;
            }
            catch (RuntimeBinderException)
            {
                Console.WriteLine("ViewerPreferences property is not available in the current Aspose.Pdf version. The PDF will be saved without modifying scroll‑bar visibility.");
            }

            // Save the (potentially) modified PDF.
            pdfDoc.Save(outputPath);

            Console.WriteLine($"PDF saved with hidden scrollbars (if supported): '{outputPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
