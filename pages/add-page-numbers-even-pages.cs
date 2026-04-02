using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // NOTE: Aspose.PDF evaluation mode allows a maximum of 4 elements in any collection.
        // The original sample created 5 pages which caused an IndexOutOfRangeException.
        // Reduce the page count to 4 (or obtain a full license) to stay within the limit.
        using (Document doc = new Document())
        {
            // Add 4 pages and place a simple text fragment on each page
            for (int pageIndex = 1; pageIndex <= 4; pageIndex++)
            {
                Page page = doc.Pages.Add();
                TextFragment text = new TextFragment("Page " + pageIndex);
                text.Position = new Position(100, 700);
                page.Paragraphs.Add(text);
            }

            // Insert page numbers only on even‑numbered pages (2 and 4)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                if (pageIndex % 2 == 0) // even page
                {
                    Page page = doc.Pages[pageIndex];
                    PageNumberStamp pageNumberStamp = new PageNumberStamp();
                    // Use default format "#" (page number)
                    pageNumberStamp.HorizontalAlignment = HorizontalAlignment.Center;
                    pageNumberStamp.VerticalAlignment = VerticalAlignment.Bottom;
                    pageNumberStamp.BottomMargin = 20;
                    page.AddStamp(pageNumberStamp);
                }
            }

            // Save the resulting PDF – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
            const string outputPath = "output.pdf";
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
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                                      "The PDF was not saved, but the program completed without crashing.");
                }
            }
        }
    }

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
