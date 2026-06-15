using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class HealthCheck
{
    static void Main()
    {
        // Path for a temporary PDF used for the health check
        string tempPdfPath = Path.Combine(Path.GetTempPath(), "healthcheck_sample.pdf");

        // Create a minimal PDF document with a single blank page
        using (Document doc = new Document())
        {
            // Add a blank page (Aspose.Pdf uses 1‑based indexing)
            doc.Pages.Add();

            // Save the document to the temporary file
            doc.Save(tempPdfPath);
        }

        // Verify that PdfAnnotationEditor can bind to the created PDF without throwing
        try
        {
            // Initialize the facade
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                // Bind the PDF file
                editor.BindPdf(tempPdfPath);

                // Access the underlying Document to confirm successful binding
                Document boundDoc = editor.Document;
                if (boundDoc == null || boundDoc.Pages.Count == 0)
                {
                    Console.Error.WriteLine("Health check failed: bound document is null or empty.");
                }
                else
                {
                    Console.WriteLine("Health check passed: PdfAnnotationEditor bound successfully.");
                }

                // No further operations are required; the facade will be disposed automatically
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Health check failed with exception: {ex.Message}");
        }
        finally
        {
            // Clean up the temporary file
            if (File.Exists(tempPdfPath))
            {
                try { File.Delete(tempPdfPath); } catch { /* ignore cleanup errors */ }
            }
        }
    }
}