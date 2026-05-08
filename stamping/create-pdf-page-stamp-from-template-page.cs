using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string targetPdf = "target.pdf";   // PDF that will receive the stamp
        const string outputPdf = "output.pdf";   // Resulting PDF

        // Ensure the target PDF exists – create a blank document with a few pages if it does not.
        if (!File.Exists(targetPdf))
        {
            CreateBlankTargetPdf(targetPdf, 3);
        }

        // Create an in‑memory PDF that will serve as the stamp template.
        Document stampTemplate = CreateStampTemplateDocument("Template Page – Stamp Content");
        // The template contains exactly one page; obtain it.
        Page stampPage = stampTemplate.Pages[1];

        // Create a PdfPageStamp from the selected page.
        PdfPageStamp pageStamp = new PdfPageStamp(stampPage)
        {
            Background = false,               // place stamp on top of page content
            Opacity = 0.5,                    // semi‑transparent
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        };

        // Load the target document where the stamp will be applied.
        using (Document targetDoc = new Document(targetPdf))
        {
            // Apply the stamp to each page of the target document.
            foreach (Page page in targetDoc.Pages)
            {
                page.AddStamp(pageStamp);
            }

            // Save the stamped document safely (handles missing libgdiplus on non‑Windows platforms).
            SafeSave(targetDoc, outputPdf);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPdf}'.");
    }

    // Helper: creates a one‑page PDF in memory with a simple centered text fragment.
    private static Document CreateStampTemplateDocument(string text)
    {
        Document doc = new Document();
        Page page = doc.Pages.Add();
        TextFragment tf = new TextFragment(text)
        {
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            Margin = new MarginInfo(0, 0, 0, 0)
        };
        page.Paragraphs.Add(tf);
        return doc; // No need to save to disk – we keep it in memory.
    }

    // Helper: creates a blank PDF with the specified number of empty pages.
    private static void CreateBlankTargetPdf(string path, int pageCount)
    {
        using (Document doc = new Document())
        {
            for (int i = 0; i < pageCount; i++)
            {
                doc.Pages.Add();
            }
            SafeSave(doc, path);
        }
    }

    // Safely saves a document, guarding against missing GDI+ (libgdiplus) on non‑Windows platforms.
    private static void SafeSave(Document doc, string outputPath)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            doc.Save(outputPath);
            return;
        }

        try
        {
            doc.Save(outputPath);
        }
        catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
        {
            Console.WriteLine($"Warning: GDI+ (libgdiplus) is not available on this platform. Unable to save '{outputPath}'.");
        }
    }

    // Walks the exception chain to detect a DllNotFoundException (e.g., missing libgdiplus).
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
