using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing; // for Color

namespace Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a new PDF document
            using (Document doc = new Document())
            {
                // NOTE: In evaluation mode Aspose.PDF allows a maximum of 4 elements in any collection.
                // Therefore we limit the number of pages to 4. A full license removes this restriction.
                int totalPages = 4; // was 6, capped to evaluation‑mode limit
                for (int i = 1; i <= totalPages; i++)
                {
                    Page page = doc.Pages.Add();
                    TextFragment tf = new TextFragment("Page " + i);
                    tf.TextState.FontSize = 24;
                    page.Paragraphs.Add(tf);
                }

                // Set alternating background colors: LightGray for odd pages, White for even pages
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Page page = doc.Pages[i];
                    if ((i % 2) == 1)
                    {
                        // LightGray (approximately 90% gray)
                        page.Background = Color.FromGray(0.9);
                    }
                    else
                    {
                        // White (100% gray)
                        page.Background = Color.FromGray(1.0);
                    }
                }

                // Save the resulting PDF – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
                string outputPath = "output.pdf";
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
                        Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF was not saved.");
                    }
                }
            }
        }

        // Helper method to walk the inner‑exception chain and detect a missing native library
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
