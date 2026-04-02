using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a source PDF with four sample pages
        using (Document sourceDoc = new Document())
        {
            for (int i = 1; i <= 4; i++)
            {
                Page page = sourceDoc.Pages.Add();
                TextFragment fragment = new TextFragment("Page " + i);
                page.Paragraphs.Add(fragment);
            }

            // Save the original document (guarded for non‑Windows platforms)
            SaveDocument(sourceDoc, "source.pdf");

            // Create a new document and copy pages in descending order
            using (Document targetDoc = new Document())
            {
                for (int i = sourceDoc.Pages.Count; i >= 1; i--)
                {
                    // Import the page into the target document
                    targetDoc.Pages.Add(sourceDoc.Pages[i]);
                }

                SaveDocument(targetDoc, "reordered.pdf");
            }
        }
    }

    private static void SaveDocument(Document doc, string path)
    {
        // On Windows GDI+ is always present, so we can save directly.
        // On macOS / Linux we may lack libgdiplus – guard the call.
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            doc.Save(path);
            Console.WriteLine($"PDF saved to '{path}'.");
        }
        else
        {
            try
            {
                doc.Save(path);
                Console.WriteLine($"PDF saved to '{path}' (non‑Windows).");
            }
            catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
            {
                Console.WriteLine($"Warning: GDI+ (libgdiplus) is not available on this platform. Skipping save of '{path}'.");
            }
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