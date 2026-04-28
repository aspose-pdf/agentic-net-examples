using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string outputPath = "output_pdfx3.pdf";
        const string logPath    = "conversion_log.xml";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add three blank pages
            doc.Pages.Add();
            doc.Pages.Add();
            doc.Pages.Add();

            // Convert the document to PDF/X‑3 format – guard against missing GDI+ on non‑Windows platforms
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Convert(logPath, PdfFormat.PDF_X_3, ConvertErrorAction.Delete);
            }
            else
            {
                try
                {
                    doc.Convert(logPath, PdfFormat.PDF_X_3, ConvertErrorAction.Delete);
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available – PDF/X‑3 conversion may be incomplete.");
                }
            }

            // Save the PDF/X‑3 file – also guard for missing GDI+ on non‑Windows platforms
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
            }
            else
            {
                try
                {
                    doc.Save(outputPath);
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available – PDF saving was skipped.");
                }
            }
        }

        Console.WriteLine($"PDF/X‑3 document processing completed. Output path: '{outputPath}'.");
    }

    // Helper that walks the inner‑exception chain to detect a missing native GDI+ library
    private static bool ContainsDllNotFound(Exception? ex)
    {
        while (ex != null)
        {
            if (ex is DllNotFoundException)
                return true;
            ex = ex.InnerException;
        }
        return false;
    }
}
