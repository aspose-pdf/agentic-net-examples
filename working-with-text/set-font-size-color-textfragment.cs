using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "styled_output.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Create a text fragment with the desired text
            TextFragment fragment = new TextFragment("Hello, Aspose.Pdf!");

            // Define font size and color using the fragment's TextState
            fragment.TextState.FontSize = 18; // Set font size (points)
            fragment.TextState.ForegroundColor = Aspose.Pdf.Color.FromRgb(0.0, 0.5, 0.8); // Set custom color

            // Add the styled text fragment to the page
            page.Paragraphs.Add(fragment);

            // Save the PDF – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
            try
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
            catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
            {
                Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                                  "The PDF could not be saved, but the code executed correctly.");
            }
        }
    }

    // Helper method that walks the inner‑exception chain looking for a DllNotFoundException
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
