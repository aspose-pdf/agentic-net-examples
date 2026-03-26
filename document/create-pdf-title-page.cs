using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "title-page.pdf";

        using (Document doc = new Document())
        {
            // Set document title metadata
            doc.SetTitle("Custom Title Document");

            // Add a new page for the title
            Page titlePage = doc.Pages.Add();

            // Create a text fragment with custom font and color
            TextFragment titleFragment = new TextFragment("My Document Title");
            titleFragment.TextState.Font = FontRepository.FindFont("Helvetica");
            titleFragment.TextState.FontSize = 36;
            titleFragment.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;

            // Position the text (centered)
            titleFragment.Position = new Position(200, 700);
            titleFragment.HorizontalAlignment = HorizontalAlignment.Center;

            // Add the text fragment to the page
            titlePage.Paragraphs.Add(titleFragment);

            // Save the PDF – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
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
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF was not saved.");
                }
            }
        }
    }

    // Helper that walks the inner‑exception chain looking for a DllNotFoundException
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
