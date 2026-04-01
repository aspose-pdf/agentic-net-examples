using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

namespace RemovePageMargins
{
    class Program
    {
        static void Main(string[] args)
        {
            const string samplePath = "sample.pdf";
            const string outputPath = "output.pdf";

            // -----------------------------------------------------------------
            // 1. Create a sample PDF (only if the platform can safely call Save())
            // -----------------------------------------------------------------
            using (Document sampleDoc = new Document())
            {
                Page page = sampleDoc.Pages.Add();
                TextFragment fragment = new TextFragment("Sample text with margins");
                page.Paragraphs.Add(fragment);

                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    // On Windows GDI+ is always present, safe to save.
                    sampleDoc.Save(samplePath);
                }
                else
                {
                    // On macOS / Linux the native GDI+ library (libgdiplus) may be missing.
                    // Guard the Save call to avoid a TypeInitializationException.
                    Console.WriteLine("[Info] Skipping sample PDF save – libgdiplus is required on non‑Windows platforms.");
                }
            }

            // -----------------------------------------------------------------
            // 2. Load the PDF and adjust CropBox for each page to remove margins
            // -----------------------------------------------------------------
            // If the sample PDF was not saved (non‑Windows), we cannot load it, so we exit.
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Console.WriteLine("[Info] Execution finished – no PDF was created because the platform lacks GDI+ support.");
                return;
            }

            using (Document doc = new Document(samplePath))
            {
                int pageCount = doc.Pages.Count;
                for (int i = 1; i <= pageCount; i++)
                {
                    Page page = doc.Pages[i];
                    // Calculate the bounding box of the actual content on the page.
                    Rectangle contentBox = page.CalculateContentBBox();
                    // Set the CropBox to the content bounding box – this removes the margins.
                    page.CropBox = contentBox;
                }

                // Save the modified PDF. Guard the call for non‑Windows platforms as a safety net.
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    doc.Save(outputPath);
                    Console.WriteLine($"[Success] PDF saved to '{outputPath}'.");
                }
                else
                {
                    try
                    {
                        doc.Save(outputPath);
                        Console.WriteLine($"[Success] PDF saved to '{outputPath}'.");
                    }
                    catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                    {
                        Console.WriteLine("[Warning] libgdiplus is not available – PDF could not be saved on this platform.");
                    }
                }
            }
        }

        // Helper that walks the inner‑exception chain looking for a DllNotFoundException.
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
}
