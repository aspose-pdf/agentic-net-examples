using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "strikethrough.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page (Aspose.Pdf uses 1‑based page indexing)
            Page page = doc.Pages.Add();

            // Create a TextFragment with the desired text
            TextFragment tf = new TextFragment("This text is struck through")
            {
                Position = new Position(100, 700)
            };

            // Enable strikeout styling via the TextState property
            tf.TextState.StrikeOut = true;

            // Optional: set font, size, and color using Aspose.Pdf types
            tf.TextState.Font = FontRepository.FindFont("Helvetica");
            tf.TextState.FontSize = 14;
            tf.TextState.ForegroundColor = Color.Black;

            // Add the TextFragment to the page's paragraph collection
            page.Paragraphs.Add(tf);

            // Save the document – guard against missing GDI+ on non‑Windows platforms
            SaveDocument(doc, outputPath);
        }

        Console.WriteLine("PDF processing completed.");
    }

    private static void SaveDocument(Document doc, string path)
    {
        // On Windows the native GDI+ library is always present
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            doc.Save(path);
            Console.WriteLine($"PDF saved to '{path}'.");
            return;
        }

        // On macOS / Linux Aspose.Pdf may require libgdiplus – handle gracefully
        try
        {
            doc.Save(path);
            Console.WriteLine($"PDF saved to '{path}'.");
        }
        catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
        {
            Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF was not saved.");
        }
    }

    // Walk the inner‑exception chain to detect a missing native GDI+ library
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