using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

#nullable enable

public class AppendPageExample
{
    public static void Main()
    {
        // Create a sample source PDF with one page (in‑memory)
        using (Document sourceDoc = new Document())
        {
            Page sourcePage = sourceDoc.Pages.Add();
            TextFragment sourceText = new TextFragment("Source PDF – First Page");
            sourcePage.Paragraphs.Add(sourceText);

            // Create a sample PDF that contains the page to be appended (also in‑memory)
            using (Document appendDoc = new Document())
            {
                Page appendPage = appendDoc.Pages.Add();
                TextFragment appendText = new TextFragment("Appended PDF – Only Page");
                appendPage.Paragraphs.Add(appendText);

                // Append all pages of appendDoc to sourceDoc
                sourceDoc.Merge(appendDoc);
            }

            // Save the combined document – this is the only file we need to write to disk.
            SaveDocument(sourceDoc, "output.pdf");
        }
    }

    // Centralised save routine that guards against missing GDI+ (libgdiplus) on non‑Windows platforms.
    private static void SaveDocument(Document doc, string path)
    {
        // On Windows the native GDI+ library is always present.
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            doc.Save(path);
            Console.WriteLine($"PDF saved to '{path}'.");
            return;
        }

        // On macOS / Linux we may not have libgdiplus. Attempt to save and handle the specific exception.
        try
        {
            doc.Save(path);
            Console.WriteLine($"PDF saved to '{path}'.");
        }
        catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
        {
            Console.WriteLine($"Warning: GDI+ (libgdiplus) is not available on this platform. Skipping save of '{path}'.");
        }
    }

    // Walks the inner‑exception chain to detect a DllNotFoundException (missing libgdiplus).
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
