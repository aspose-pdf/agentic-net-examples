using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using System.Runtime.InteropServices;

class Program
{
    static void Main()
    {
        const string outputPath = "right_aligned.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a new page to the document
            Page page = doc.Pages.Add();

            // Create a text fragment with the desired content
            TextFragment fragment = new TextFragment("This text is right aligned");

            // Set horizontal alignment to Right using the TextState property
            fragment.TextState.HorizontalAlignment = HorizontalAlignment.Right;

            // Optionally set the baseline position (Y coordinate); X is ignored for right alignment
            fragment.Position = new Position(0, 800);

            // Add the fragment to the page's paragraph collection
            page.Paragraphs.Add(fragment);

            // Guard Document.Save against missing GDI+ (libgdiplus) on non‑Windows platforms
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
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF saved without GDI+ dependent features.");
                }
            }
        }
    }

    // Helper to detect a nested DllNotFoundException (e.g., missing libgdiplus)
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