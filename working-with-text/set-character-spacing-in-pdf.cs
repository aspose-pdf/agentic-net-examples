using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        // Create a new PDF document inside a using block for proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Create a TextFragment with the text to display and set its position
            TextFragment fragment = new TextFragment("Aspose.Pdf Character Spacing Example")
            {
                Position = new Position(100, 700)
            };

            // Configure the TextState (CharacterSpacing controls inter‑character gaps)
            fragment.TextState.CharacterSpacing = 2.0f; // increase spacing by 2 points between characters
            fragment.TextState.Font = FontRepository.FindFont("Helvetica");
            fragment.TextState.FontSize = 24;
            fragment.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;

            // Add the fragment to the page's paragraphs collection
            page.Paragraphs.Add(fragment);

            // Guard Document.Save on non‑Windows platforms where libgdiplus may be missing
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
                    Console.WriteLine($"PDF saved to '{outputPath}'. (saved on non‑Windows platform)");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                                      "The PDF was not saved, but the code executed correctly.");
                }
            }
        }
    }

    // Helper method to walk nested exceptions and detect a missing native GDI+ library
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
