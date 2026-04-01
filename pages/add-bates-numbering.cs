using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

public class Program
{
    public static void Main()
    {
        // Create a sample PDF document
        using (Document doc = new Document())
        {
            // Add a page
            Page page = doc.Pages.Add();

            // Add some sample text to the page
            TextFragment tf = new TextFragment("Sample PDF for Bates numbering.");
            page.Paragraphs.Add(tf);

            // Configure and add Bates numbering to all pages
            doc.Pages.AddBatesNumbering(artifact =>
            {
                artifact.Prefix = "DOC";
                artifact.Suffix = "-2026";
                artifact.StartNumber = 1;
                artifact.NumberOfDigits = 4; // optional, defines the width of the number
                artifact.StartPage = 1;
                artifact.EndPage = 0; // 0 means no upper limit (all pages)
                artifact.IsBackground = false;
                artifact.ArtifactHorizontalAlignment = Aspose.Pdf.HorizontalAlignment.Center;
                artifact.ArtifactVerticalAlignment = Aspose.Pdf.VerticalAlignment.Bottom;
                artifact.BottomMargin = 20;
            });

            // Save the document – guard against missing GDI+ on non‑Windows platforms
            string outputPath = "output.pdf";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF saved to {outputPath}");
            }
            else
            {
                try
                {
                    doc.Save(outputPath);
                    Console.WriteLine($"PDF saved to {outputPath}");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("GDI+ (libgdiplus) is not available on this platform. PDF not saved.");
                }
            }
        }
    }

    private static bool ContainsDllNotFound(Exception ex)
    {
        Exception? current = ex;
        while (current != null)
        {
            if (current is DllNotFoundException)
                return true;
            current = current.InnerException;
        }
        return false;
    }
}