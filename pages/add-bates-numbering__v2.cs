using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

namespace AddBatesNumberingExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a sample PDF document
            using (Document doc = new Document())
            {
                // Add a page with some sample text
                Page page = doc.Pages.Add();
                TextFragment fragment = new TextFragment("Sample PDF page content.");
                page.Paragraphs.Add(fragment);

                // Configure and add Bates numbering with alphanumeric prefix
                doc.Pages.AddBatesNumbering(artifact =>
                {
                    artifact.Prefix = "PRJ-";
                    artifact.StartNumber = 100;
                    artifact.NumberOfDigits = 5;
                    artifact.BottomMargin = 20;
                    artifact.ArtifactHorizontalAlignment = HorizontalAlignment.Center;
                    artifact.ArtifactVerticalAlignment = VerticalAlignment.Bottom;
                });

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
                        Console.WriteLine($"PDF saved to '{outputPath}'. (non‑Windows platform, libgdiplus may be required)");
                    }
                    catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                    {
                        Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                                          "The PDF was not saved, but the program ran without crashing.");
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
}
