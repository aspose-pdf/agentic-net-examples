using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    // Path to the temporary sample PDF used for the health check
    private const string SamplePdfPath = "sample.pdf";
    // Path to the output PDF after binding (can be the same as input for a no‑op save)
    private const string OutputPdfPath = "sample_bound.pdf";

    static void Main()
    {
        bool healthOk = VerifyPdfAnnotationEditorBinding();
        Console.WriteLine($"PdfAnnotationEditor health check: {(healthOk ? "PASS" : "FAIL")}");
    }

    // Returns true if PdfAnnotationEditor can bind to a PDF and save without throwing
    private static bool VerifyPdfAnnotationEditorBinding()
    {
        try
        {
            // Ensure a sample PDF exists – create a minimal one if missing
            if (!File.Exists(SamplePdfPath))
            {
                // Document implements IDisposable; wrap in using per global rule
                using (Document doc = new Document())
                {
                    // Add a blank page (Pages are 1‑based)
                    doc.Pages.Add();
                    doc.Save(SamplePdfPath);
                }
            }

            // PdfAnnotationEditor does NOT implement IDisposable, so do NOT use using
            PdfAnnotationEditor editor = new PdfAnnotationEditor();

            // Bind the editor to the sample PDF
            editor.BindPdf(SamplePdfPath);

            // Perform a no‑op save to verify that the binding succeeded
            // (Save will write the same content back to a new file)
            editor.Save(OutputPdfPath);

            // If we reach this point without exception, the health check passed
            return true;
        }
        catch (Exception ex)
        {
            // Log the error details (in a real scenario you might write to a log file)
            Console.Error.WriteLine($"Health check failed: {ex.Message}");
            return false;
        }
        finally
        {
            // Clean up temporary files if desired
            try { if (File.Exists(OutputPdfPath)) File.Delete(OutputPdfPath); } catch { }
            // Keep the sample PDF for future runs; delete if you prefer:
            // try { if (File.Exists(SamplePdfPath)) File.Delete(SamplePdfPath); } catch { }
        }
    }
}