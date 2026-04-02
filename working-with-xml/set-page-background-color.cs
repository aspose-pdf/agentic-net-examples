using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Define an XML color string (e.g., hex format)
            string xmlColor = "#FFCC00"; // Orange color

            // Parse the XML color string to an Aspose.Pdf.Color object
            Aspose.Pdf.Color backgroundColor = Aspose.Pdf.Color.Parse(xmlColor);

            // Apply the parsed color as the page background
            page.Background = backgroundColor;

            // Add a text fragment to visualize the background color
            TextFragment text = new TextFragment("Sample page with custom background");
            text.Position = new Position(100, 700);
            page.Paragraphs.Add(text);

            // Save the resulting PDF – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
            string outputPath = "output.pdf";
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
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                                      "The PDF was generated, but saving may be incomplete.");
                }
            }
        }
    }

    // Helper method to walk the inner‑exception chain and detect a missing native GDI+ library
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
