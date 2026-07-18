using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string samplePdfPath = "sample.pdf";

        // Verify the sample PDF exists before attempting to bind.
        if (!File.Exists(samplePdfPath))
        {
            Console.Error.WriteLine($"Health check failed: file not found – {samplePdfPath}");
            return;
        }

        try
        {
            // Create the PdfAnnotationEditor facade.
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                // Bind the PDF file to the editor. This is the health‑check operation.
                editor.BindPdf(samplePdfPath);

                // If we reach this point, binding succeeded.
                Console.WriteLine("Health check passed: PdfAnnotationEditor bound to the PDF successfully.");
            }
        }
        catch (Exception ex)
        {
            // Any exception indicates a problem with the binding operation.
            Console.Error.WriteLine($"Health check failed: {ex.GetType().Name} – {ex.Message}");
        }
    }
}