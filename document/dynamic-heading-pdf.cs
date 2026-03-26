using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        string outputPath = "output.pdf";
        string headingText = $"Report generated on {DateTime.Now:yyyy-MM-dd}";

        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Create a level‑1 heading with dynamic text
            Heading heading = new Heading(1);
            heading.Text = headingText;
            heading.HorizontalAlignment = HorizontalAlignment.Center;
            heading.TextState.FontSize = 20;
            heading.TextState.Font = FontRepository.FindFont("Helvetica");
            heading.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;

            page.Paragraphs.Add(heading);

            // Save the document with a guard for platforms that lack GDI+ (libgdiplus)
            SaveDocument(doc, outputPath);
        }

        Console.WriteLine("Processing finished.");
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

        // On non‑Windows platforms attempt to save and gracefully handle missing libgdiplus.
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
