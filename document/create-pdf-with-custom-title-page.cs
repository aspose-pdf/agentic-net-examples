using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Output file path
        const string outputPath = "TitlePage.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Set a document title (metadata)
            doc.SetTitle("Custom PDF with Title Page");

            // Optional: set a background color for all pages
            doc.Background = Aspose.Pdf.Color.LightGray;

            // Add a single page that will serve as the title page
            Page titlePage = doc.Pages.Add();

            // Create a text fragment for the title
            TextFragment titleFragment = new TextFragment("My Awesome Title");

            // Set custom font, size and color using TextState
            titleFragment.TextState.Font = FontRepository.FindFont("Helvetica");
            titleFragment.TextState.FontSize = 36;
            titleFragment.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;

            // Position the title on the page (coordinates are in points)
            titleFragment.Position = new Position(100, 700);

            // Add the text fragment to the page's paragraph collection
            titlePage.Paragraphs.Add(titleFragment);

            // Save the document – guard against missing libgdiplus on non‑Windows platforms
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
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF saved without rendering‑dependent features.");
                }
            }
        }

        Console.WriteLine($"PDF created (or attempted) at '{Path.GetFullPath(outputPath)}'.");
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