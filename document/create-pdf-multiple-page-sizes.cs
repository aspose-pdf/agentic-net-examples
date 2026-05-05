using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "multi_section.pdf";

        using (Document doc = new Document())
        {
            // ---------- Section 1: A4 portrait ----------
            Page page1 = doc.Pages.Add();
            page1.SetPageSize(PageSize.A4.Width, PageSize.A4.Height);
            page1.Paragraphs.Add(new TextFragment("Section 1 - A4 Portrait"));

            // ---------- Section 2: A4 landscape ----------
            Page page2 = doc.Pages.Add();
            page2.SetPageSize(PageSize.A4.Height, PageSize.A4.Width);
            page2.Paragraphs.Add(new TextFragment("Section 2 - A4 Landscape"));

            // ---------- Section 3: Letter portrait ----------
            Page page3 = doc.Pages.Add();
            page3.SetPageSize(PageSize.PageLetter.Width, PageSize.PageLetter.Height);
            page3.Paragraphs.Add(new TextFragment("Section 3 - Letter Portrait"));

            // ---------- Section 4: Letter landscape ----------
            Page page4 = doc.Pages.Add();
            page4.SetPageSize(PageSize.PageLetter.Height, PageSize.PageLetter.Width);
            page4.Paragraphs.Add(new TextFragment("Section 4 - Letter Landscape"));

            // Save the document with platform‑aware handling for libgdiplus.
            SaveDocument(doc, outputPath);
        }

        Console.WriteLine($"PDF creation finished. Check '{outputPath}' if saved.");
    }

    private static void SaveDocument(Document doc, string path)
    {
        // Windows always has GDI+; save directly.
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
            Console.WriteLine("Warning: libgdiplus (GDI+) is not available on this platform. " +
                              "The PDF could not be saved.");
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
