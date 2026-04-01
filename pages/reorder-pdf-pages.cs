using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

public class ReorderPdfPages
{
    public static void Main()
    {
        // ---------------------------------------------------------------------
        // 1. Create a sample PDF with three pages (only for demo purposes).
        // ---------------------------------------------------------------------
        using (Document sourceDoc = new Document())
        {
            // Page 1
            Page page1 = sourceDoc.Pages.Add();
            page1.Paragraphs.Add(new TextFragment("This is page 1"));

            // Page 2
            Page page2 = sourceDoc.Pages.Add();
            page2.Paragraphs.Add(new TextFragment("This is page 2"));

            // Page 3
            Page page3 = sourceDoc.Pages.Add();
            page3.Paragraphs.Add(new TextFragment("This is page 3"));

            // -----------------------------------------------------------------
            // 2. Define the new order (1‑based page numbers).
            // -----------------------------------------------------------------
            int[] newOrder = new int[] { 3, 1, 2 };

            // -----------------------------------------------------------------
            // 3. Build a new document with pages in the required order.
            // -----------------------------------------------------------------
            using (Document destDoc = new Document())
            {
                // Remove the default empty page created by the constructor.
                destDoc.Pages.Delete();

                // Add pages to the destination document following the new order.
                foreach (int pageNumber in newOrder)
                {
                    // Aspose.Pdf uses 1‑based indexing for the Pages collection.
                    Page srcPage = sourceDoc.Pages[pageNumber];
                    // The Add method copies the page into the destination document.
                    destDoc.Pages.Add(srcPage);
                }

                // Save the reordered PDF – guard the call for platforms that
                // lack GDI+.
                SaveDocument(destDoc, "output.pdf");
            }
        }
    }

    /// <summary>
    /// Saves a document while handling the GDI+ (libgdiplus) dependency that
    /// Aspose.Pdf has on non‑Windows platforms. On Windows the document is saved
    /// normally; on other OSes we attempt the save inside a try/catch that looks
    /// for a nested DllNotFoundException and, if caught, writes a friendly
    /// message instead of crashing.
    /// </summary>
    private static void SaveDocument(Document doc, string path)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            doc.Save(path);
            Console.WriteLine($"PDF saved to '{path}'.");
            return;
        }

        try
        {
            doc.Save(path);
            Console.WriteLine($"PDF saved to '{path}'.");
        }
        catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
        {
            Console.WriteLine($"Warning: GDI+ (libgdiplus) is not available on this platform. " +
                              $"The PDF could not be saved to '{path}'.");
        }
    }

    /// <summary>
    /// Walks the exception chain to determine whether a DllNotFoundException is present.
    /// </summary>
    private static bool ContainsDllNotFound(Exception ex)
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
