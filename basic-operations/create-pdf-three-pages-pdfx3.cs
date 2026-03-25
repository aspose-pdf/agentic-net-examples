using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string outputPath = "output_pdfx3.pdf";
        const string logPath = "conversion.log";

        using (Document doc = new Document())
        {
            // Add three blank pages
            doc.Pages.Add();
            doc.Pages.Add();
            doc.Pages.Add();

            // Convert the document to PDF/X-3 compliance – this operation may also require GDI+.
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                // Safe on Windows – GDI+ is always present.
                doc.Convert(logPath, PdfFormat.PDF_X_3, ConvertErrorAction.Delete);
            }
            else
            {
                // On non‑Windows platforms try the conversion but gracefully handle missing libgdiplus.
                try
                {
                    doc.Convert(logPath, PdfFormat.PDF_X_3, ConvertErrorAction.Delete);
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available – PDF/X‑3 conversion was skipped.");
                }
            }

            // Save the PDF/X-3 document – guard against missing GDI+ on macOS/Linux.
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF/X-3 file saved to '{outputPath}'.");
            }
            else
            {
                try
                {
                    doc.Save(outputPath);
                    Console.WriteLine($"PDF/X-3 file saved to '{outputPath}'.");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available – PDF saving was skipped.");
                }
            }
        }
    }

    // Helper that walks the inner‑exception chain looking for a DllNotFoundException (e.g., libgdiplus).
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
