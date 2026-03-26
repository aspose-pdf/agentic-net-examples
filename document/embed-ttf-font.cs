using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "embedded_font.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Locate a TrueType font (e.g., Arial) and mark it for embedding
            Font trueTypeFont = FontRepository.FindFont("Arial");
            trueTypeFont.IsEmbedded = true;

            // Create a text fragment that uses the embedded font
            TextFragment fragment = new TextFragment("Hello, embedded TrueType font!");
            fragment.TextState.Font = trueTypeFont;
            fragment.TextState.FontSize = 24;
            fragment.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;

            // Add the text fragment to the page
            page.Paragraphs.Add(fragment);

            // Ensure standard Type1 fonts are also embedded if they are used
            doc.EmbedStandardFonts = true;

            // Save the PDF document with a platform guard for GDI+ / libgdiplus
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
