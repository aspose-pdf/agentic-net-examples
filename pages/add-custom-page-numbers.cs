using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        // Create a sample PDF document with a few pages
        using (Document doc = new Document())
        {
            // Add three pages with sample text
            for (int i = 0; i < 3; i++)
            {
                Page page = doc.Pages.Add();
                TextFragment tf = new TextFragment($"Sample page {i + 1}");
                tf.Position = new Position(100, 700);
                page.Paragraphs.Add(tf);
            }

            // Create a page number stamp with the custom format "Page X of Y"
            PageNumberStamp pageNumberStamp = new PageNumberStamp("Page # of #");
            pageNumberStamp.HorizontalAlignment = HorizontalAlignment.Center;
            pageNumberStamp.VerticalAlignment = VerticalAlignment.Bottom;
            pageNumberStamp.BottomMargin = 10f;
            pageNumberStamp.TextState.FontSize = 12;

            // Apply the stamp to each page in the document
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(pageNumberStamp);
            }

            // Save the resulting PDF – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
            else
            {
                try
                {
                    doc.Save(outputPath);
                    Console.WriteLine($"PDF saved to '{outputPath}'.");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("GDI+ (libgdiplus) is not available on this platform. PDF was not saved.");
                }
            }
        }
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