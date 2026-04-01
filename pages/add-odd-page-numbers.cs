using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

namespace AddOddPageNumbersExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a sample PDF with several pages (evaluation mode allows max 4 pages)
            using (Document doc = new Document())
            {
                // Add 4 blank pages with a simple label (reduced from 5 to stay within evaluation limits)
                for (int i = 0; i < 4; i++)
                {
                    Page page = doc.Pages.Add();
                    TextFragment tf = new TextFragment("Page " + (i + 1));
                    tf.TextState.FontSize = 12;
                    tf.Position = new Position(100, 700);
                    page.Paragraphs.Add(tf);
                }

                // Insert page numbers only on odd-numbered pages
                for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
                {
                    if (pageIndex % 2 == 1) // odd page
                    {
                        Page page = doc.Pages[pageIndex];
                        PageNumberStamp stamp = new PageNumberStamp();
                        stamp.HorizontalAlignment = HorizontalAlignment.Center;
                        stamp.VerticalAlignment = VerticalAlignment.Bottom;
                        stamp.BottomMargin = 20;
                        page.AddStamp(stamp);
                    }
                }

                // Save the resulting PDF – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
                string outputPath = "output.pdf";
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
                        Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF was not saved.");
                    }
                }
            }
        }

        // Helper that walks the inner‑exception chain looking for a DllNotFoundException (e.g., libgdiplus)
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
