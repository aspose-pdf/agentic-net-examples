using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "heading.pdf";

        // Create a new empty PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a TextFragment that will act as a Level 1 heading
            TextFragment heading = new TextFragment("Level 1 Heading");
            heading.TextState.Font = FontRepository.FindFont("Helvetica");
            heading.TextState.FontSize = 24; // typical size for a heading
            heading.TextState.FontStyle = FontStyles.Bold;
            heading.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

            // Add the heading to the page
            page.Paragraphs.Add(heading);

            // Save the PDF with a guard for platforms that lack GDI+ (libgdiplus)
            SaveDocument(doc, outputPath);
        }

        Console.WriteLine("Program finished.");
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

        // On non‑Windows platforms attempt to save and gracefully handle a missing libgdiplus.
        try
        {
            doc.Save(path);
            Console.WriteLine($"PDF saved to '{path}'.");
        }
        catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
        {
            Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF could not be saved.");
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
