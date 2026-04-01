using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

namespace SetPageBackgroundCmyk
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a new PDF document
            using (Document document = new Document())
            {
                // Add a page
                Page page = document.Pages.Add();

                // Set page background color using CMYK (e.g., 0% cyan, 100% magenta, 0% yellow, 0% black)
                double cyan = 0.0;
                double magenta = 1.0;
                double yellow = 0.0;
                double black = 0.0;
                Aspose.Pdf.Color cmykColor = Aspose.Pdf.Color.FromCmyk(cyan, magenta, yellow, black);
                page.Background = cmykColor;

                // Add a sample text fragment
                TextFragment fragment = new TextFragment("Hello, CMYK Background!");
                page.Paragraphs.Add(fragment);

                // Save the PDF – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
                string outputPath = "output.pdf";
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    document.Save(outputPath);
                    Console.WriteLine($"PDF saved to '{outputPath}'.");
                }
                else
                {
                    try
                    {
                        document.Save(outputPath);
                        Console.WriteLine($"PDF saved to '{outputPath}'.");
                    }
                    catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                    {
                        Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                                          "The PDF was not saved. Install libgdiplus or run on Windows.");
                    }
                }
            }
        }

        // Helper method to detect a nested DllNotFoundException (e.g., missing libgdiplus)
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
