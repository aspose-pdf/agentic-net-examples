using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string originalPath = "original.pdf";
        const string sanitizedPath = "sanitized.pdf";

        // -------------------------------------------------
        // Step 1: Create a PDF with headings and regular text
        // -------------------------------------------------
        // Keep the document alive for the sanitization step – do NOT dispose it before we are done.
        Document originalDoc = new Document();
        {
            // Add a single page
            Page page = originalDoc.Pages.Add();

            // Heading level 1 (auto-numbered)
            Heading heading1 = new Heading(1)
            {
                Text = "Chapter 1",
                Level = 1,
                IsAutoSequence = true // enable automatic numbering
            };
            page.Paragraphs.Add(heading1);

            // Heading level 2 (auto-numbered)
            Heading heading2 = new Heading(2)
            {
                Text = "Section 1.1",
                Level = 2,
                IsAutoSequence = true
            };
            page.Paragraphs.Add(heading2);

            // Some regular (non‑heading) content
            TextFragment regular = new TextFragment("This is regular paragraph text that will be removed in the sanitization step.");
            page.Paragraphs.Add(regular);
        }

        // Save the original document only on platforms where GDI+ is guaranteed (Windows).
        // On Linux/macOS we skip the file write but keep the in‑memory Document for further processing.
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            SavePdf(originalDoc, originalPath);
        }
        else
        {
            Console.WriteLine("Info: Skipping original PDF file save on non‑Windows platform – using in‑memory document for sanitization.");
        }

        // -------------------------------------------------
        // Step 2: Sanitize the PDF, preserving only headings
        // -------------------------------------------------
        using (Document sanitizedDoc = new Document())
        {
            // Add a page to hold the retained headings
            Page sanitizedPage = sanitizedDoc.Pages.Add();

            // Iterate over all pages and paragraphs of the original (in‑memory) document
            foreach (Page srcPage in originalDoc.Pages)
            {
                foreach (var paragraph in srcPage.Paragraphs)
                {
                    // Keep only Heading objects; discard everything else
                    if (paragraph is Heading srcHeading)
                    {
                        // Clone the heading to avoid modifying the source document
                        Heading clonedHeading = (Heading)srcHeading.Clone();
                        sanitizedPage.Paragraphs.Add(clonedHeading);
                    }
                }
            }

            // Save the sanitized PDF (headings only, numbering preserved)
            SavePdf(sanitizedDoc, sanitizedPath);
        }

        // Dispose the original document now that we are finished with it.
        originalDoc.Dispose();

        Console.WriteLine($"Original PDF {(RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "saved" : "kept in memory")} to '{originalPath}'.");
        Console.WriteLine($"Sanitized PDF saved to '{sanitizedPath}'.");
    }

    /// <summary>
    /// Saves a PDF document while handling the missing libgdiplus/GDI+ scenario on non‑Windows platforms.
    /// </summary>
    private static void SavePdf(Document doc, string path)
    {
        // On Windows the native GDI+ library is always present.
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            doc.Save(path);
            return;
        }

        // On macOS / Linux we attempt to save and gracefully handle the missing libgdiplus.
        try
        {
            doc.Save(path);
        }
        catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
        {
            Console.WriteLine($"Warning: GDI+ (libgdiplus) is not available on this platform. PDF was not saved to '{path}'.");
            // Swallow the exception – the caller can decide whether this is critical.
        }
    }

    /// <summary>
    /// Walks the exception chain to determine whether a DllNotFoundException is present.
    /// </summary>
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
