using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

namespace AddTransparentSeparatorPage
{
    class Program
    {
        static void Main(string[] args)
        {
            const string outputPath = "output.pdf";

            using (Document doc = new Document())
            {
                // First page with sample text
                Page firstPage = doc.Pages.Add();
                firstPage.Paragraphs.Add(new TextFragment("First section"));

                // Insert a transparent separator page between page 1 and page 2
                // Pages are 1‑based, so insert at position 2
                Page separatorPage = doc.Pages.Insert(2);
                separatorPage.Background = Aspose.Pdf.Color.Transparent;

                // Second page with sample text (will be after separator)
                Page secondPage = doc.Pages.Add();
                secondPage.Paragraphs.Add(new TextFragment("Second section"));

                // Save the result – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
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
                        Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                                          "The PDF was created, but saving may be incomplete.");
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
}
