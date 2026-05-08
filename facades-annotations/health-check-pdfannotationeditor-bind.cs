using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string samplePdfPath = "sample.pdf";

        if (!File.Exists(samplePdfPath))
        {
            Console.Error.WriteLine($"Sample PDF not found: {samplePdfPath}");
            return;
        }

        try
        {
            // Load the PDF document using the standard Document class.
            // The Document is wrapped in a using block for deterministic disposal.
            using (Document doc = new Document(samplePdfPath))
            {
                // Create the PdfAnnotationEditor facade.
                // It implements IDisposable via the base SaveableFacade, so we also wrap it in using.
                using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
                {
                    // Bind the editor to the loaded document.
                    // This operation will throw if the PDF cannot be bound.
                    editor.BindPdf(doc);

                    // Simple verification: the Document property should now be non‑null.
                    if (editor.Document != null)
                    {
                        Console.WriteLine("PdfAnnotationEditor successfully bound to the PDF.");
                    }
                    else
                    {
                        Console.Error.WriteLine("Binding succeeded but Document property is null.");
                    }

                    // No further modifications are needed for the health check.
                    // The editor will be closed automatically by the using statement.
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Health check failed: {ex.Message}");
        }
    }
}