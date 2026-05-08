using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Create a TextFragment with the desired text
            TextFragment fragment = new TextFragment("Sample Text");

            // Set character spacing on the TextState (positive value increases gaps)
            fragment.TextState.CharacterSpacing = 2.5f;

            // Optional styling
            fragment.TextState.Font = FontRepository.FindFont("Helvetica");
            fragment.TextState.FontSize = 20;
            fragment.TextState.ForegroundColor = Aspose.Pdf.Color.DarkGreen;

            // Add the fragment to the page
            page.Paragraphs.Add(fragment);

            // === Save the PDF document with GDI+ guard ===
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                // Windows has native GDI+ – safe to save directly
                doc.Save(outputPath);
            }
            else
            {
                // On macOS / Linux libgdiplus may be missing – handle gracefully
                try
                {
                    doc.Save(outputPath);
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF was not saved.");
                }
            }
        }

        Console.WriteLine($"Program finished. Output path: '{outputPath}'.");
    }

    // Helper to walk the inner‑exception chain and detect a missing native library
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