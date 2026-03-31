using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "letter_portrait.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a new page to the document
            Page page = doc.Pages.Add();

            // Letter size in points (1 inch = 72 points)
            double width = 8.5 * 72; // 612 points
            double height = 11 * 72; // 792 points

            // Set the page size to Letter portrait dimensions
            page.SetPageSize(width, height);

            // Add a sample text fragment to visualize the page
            TextFragment fragment = new TextFragment("Letter size page");
            fragment.Position = new Position(100, height - 100);
            page.Paragraphs.Add(fragment);

            // Save the PDF document with GDI+ guard for non‑Windows platforms
            SaveDocument(doc, outputPath);
        }

        Console.WriteLine("PDF processing completed.");
    }

    private static void SaveDocument(Document doc, string path)
    {
        // On Windows the native GDI+ library is always present.
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            doc.Save(path);
            Console.WriteLine($"PDF saved to '{path}'.");
            return;
        }

        // On macOS / Linux the required libgdiplus may be missing. Attempt to save
        // and gracefully handle the TypeInitializationException that wraps a
        // DllNotFoundException for the missing native library.
        try
        {
            doc.Save(path);
            Console.WriteLine($"PDF saved to '{path}'.");
        }
        catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
        {
            Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                              "The PDF could not be saved using Aspose.Pdf's default renderer.");
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