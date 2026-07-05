using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string samplePdfPath = "sample.pdf";

        // Verify that the sample PDF exists before attempting to bind.
        if (!File.Exists(samplePdfPath))
        {
            Console.Error.WriteLine($"Sample PDF not found: {samplePdfPath}");
            return;
        }

        try
        {
            // PdfAnnotationEditor implements IDisposable, so wrap it in a using block.
            using (PdfAnnotationEditor annotationEditor = new PdfAnnotationEditor())
            {
                // Bind the editor to the PDF file. This will load the document internally.
                annotationEditor.BindPdf(samplePdfPath);

                // Simple sanity check: ensure the underlying Document is not null and has pages.
                if (annotationEditor.Document != null && annotationEditor.Document.Pages.Count > 0)
                {
                    Console.WriteLine("PdfAnnotationEditor successfully bound to the PDF.");
                }
                else
                {
                    Console.Error.WriteLine("PdfAnnotationEditor bound, but the document appears empty.");
                }

                // No further actions are required for the health check.
                // The using block will automatically close and dispose the editor.
            }
        }
        catch (Exception ex)
        {
            // Any exception indicates a failure to bind the PDF.
            Console.Error.WriteLine($"Error during PdfAnnotationEditor bind: {ex.Message}");
        }
    }
}