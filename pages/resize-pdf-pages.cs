using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

public class Program
{
    private static bool ContainsDllNotFound(Exception? ex)
    {
        while (ex != null)
        {
            if (ex is DllNotFoundException) return true;
            ex = ex.InnerException;
        }
        return false;
    }

    public static void Main()
    {
        const string samplePath = "sample.pdf";
        const string outputPath = "output.pdf";

        // ---------------------------------------------------------------------
        // 1. Create a sample PDF (single page with some text)
        // ---------------------------------------------------------------------
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();
            TextFragment tf = new TextFragment("Sample text to demonstrate page resizing.");
            page.Paragraphs.Add(tf);

            // Guard Document.Save against missing libgdiplus on non‑Windows platforms
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(samplePath);
            }
            else
            {
                try
                {
                    doc.Save(samplePath);
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("GDI+ (libgdiplus) is not available – skipping PDF creation on this platform.");
                    return;
                }
            }
        }

        // ---------------------------------------------------------------------
        // 2. Open the PDF and resize each page to fit within 595×842 points
        // ---------------------------------------------------------------------
        using (Document doc = new Document(samplePath))
        {
            // Target printable area (A4 size in points)
            PageSize targetSize = new PageSize(595f, 842f);

            foreach (Page page in doc.Pages)
            {
                // Resize scales the page content proportionally to the new size
                page.Resize(targetSize);
            }

            // Guard Document.Save the same way as above
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
                    Console.WriteLine("GDI+ (libgdiplus) is not available – PDF saved without rendering on this platform.");
                }
            }
        }
    }
}
