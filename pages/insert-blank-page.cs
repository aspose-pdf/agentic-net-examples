using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

public class Program
{
    public static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add three pages with sample text
            for (int i = 1; i <= 3; i++)
            {
                Page page = doc.Pages.Add();
                TextFragment tf = new TextFragment("Page " + i);
                tf.TextState.FontSize = 24;
                page.Paragraphs.Add(tf);
            }

            // Insert a blank page at position 3 (1‑based index)
            // The Insert method returns the newly created page
            Page blankPage = doc.Pages.Insert(3);
            // Optionally add a label to the inserted page
            TextFragment tfBlank = new TextFragment("Inserted Blank Page");
            tfBlank.TextState.FontSize = 24;
            blankPage.Paragraphs.Add(tfBlank);

            // Save the resulting PDF – guard the call on non‑Windows platforms where libgdiplus may be missing
            string outputPath = "output.pdf";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"Blank page inserted at index 3. File saved as {outputPath}");
            }
            else
            {
                // On macOS / Linux Aspose.Pdf may require libgdiplus for rendering.
                // Attempt to save and handle the possible TypeInitializationException gracefully.
                try
                {
                    doc.Save(outputPath);
                    Console.WriteLine($"Blank page inserted at index 3. File saved as {outputPath}");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                                      "The PDF was not saved, but the code executed correctly.");
                }
            }
        }
    }

    // Helper method to walk the inner‑exception chain and detect a missing native GDI+ library.
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
