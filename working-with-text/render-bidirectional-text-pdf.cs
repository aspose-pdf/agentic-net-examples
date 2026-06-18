using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "bidi_output.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Create a TextFragment with mixed LTR and RTL Unicode characters
            // Arabic word "مرحبا" (Hello) followed by English "World"
            // Use Unicode directional marks (RLE ... PDF) to force RTL rendering
            string bidiText = "\u202Bمرحبا World\u202C";

            TextFragment fragment = new TextFragment(bidiText);

            // Position the fragment (X = 50, Y = 700). The Position property is mutable.
            fragment.Position = new Position(50, 700);

            // Configure the TextState for bidirectional rendering and styling.
            // In recent Aspose.Pdf versions the Bidi flag is exposed as IsBidirectional.
            // If the property does not exist, the Unicode directional marks are sufficient.
            fragment.TextState.Font = FontRepository.FindFont("Arial"); // Font that supports Arabic glyphs
            fragment.TextState.FontSize = 24;
            fragment.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
            // Enable explicit bidi processing when the API provides the property.
            // This line is safe – it will be ignored if the property is not present.
            try
            {
                // Reflection is used to avoid compile‑time errors on versions where the property is absent.
                var prop = typeof(TextState).GetProperty("IsBidirectional");
                if (prop != null && prop.CanWrite)
                {
                    prop.SetValue(fragment.TextState, true);
                }
            }
            catch { /* ignore any reflection issues */ }

            // Add the fragment to the page
            page.Paragraphs.Add(fragment);

            // Save the document – guard against missing GDI+ on non‑Windows platforms
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
                    Console.WriteLine("GDI+ (libgdiplus) is not available on this platform. PDF was not saved.");
                }
            }
        }
    }

    // Helper to detect missing native GDI+ library
    static bool ContainsDllNotFound(Exception ex)
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
