using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Output PDF path
        const string outputPath = "centered_text.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Create a text fragment with the desired content
            TextFragment fragment = new TextFragment("This text is centered on the page");

            // Configure the TextState to use center alignment
            fragment.TextState.HorizontalAlignment = HorizontalAlignment.Center;

            // Optionally set other text properties (font size, color, etc.)
            fragment.TextState.FontSize = 14;
            fragment.TextState.Font = FontRepository.FindFont("Helvetica");
            fragment.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;

            // Add the fragment to the page
            page.Paragraphs.Add(fragment);

            // Save the document – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
            try
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
            catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
            {
                Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                                  "The PDF could not be saved.");
            }
        }
    }

    // Helper method to walk the inner‑exception chain and detect a DllNotFoundException
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
