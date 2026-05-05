using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // for TextFragment

class Program
{
    static void Main()
    {
        const string outputPdfPath = "output.pdf";

        // Create a simple PDF in memory so we don't depend on an external file.
        using (Document pdfDoc = CreateSamplePdf())
        {
            // Example attachment content stored in a memory stream.
            byte[] attachmentBytes = System.Text.Encoding.UTF8.GetBytes("This is the attachment content.");
            using (MemoryStream attachmentStream = new MemoryStream(attachmentBytes))
            {
                // Initialise the content editor with the in‑memory document.
                PdfContentEditor editor = new PdfContentEditor(pdfDoc);

                // Add the memory‑stream attachment (no visual annotation).
                // Parameters: stream, attachment name, description.
                editor.AddDocumentAttachment(attachmentStream, "attachment.txt", "Sample attachment from memory stream");

                // Save the modified PDF – guarded for platforms without libgdiplus.
                SavePdf(editor, outputPdfPath);
            }
        }

        Console.WriteLine($"PDF processing completed. Check '{outputPdfPath}'.");
    }

    private static void SavePdf(PdfContentEditor editor, string path)
    {
        // On Windows the native GDI+ library is always present.
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            editor.Save(path);
            Console.WriteLine($"PDF saved to '{path}'.");
            return;
        }

        // On macOS / Linux libgdiplus may be missing – catch the TypeInitializationException that wraps a DllNotFoundException.
        try
        {
            editor.Save(path);
            Console.WriteLine($"PDF saved to '{path}'.");
        }
        catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
        {
            Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                              "The PDF was created, but operations that require GDI+ were skipped.");
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

    private static Document CreateSamplePdf()
    {
        // Build a minimal PDF document entirely in memory.
        Document doc = new Document();
        Page page = doc.Pages.Add();
        page.Paragraphs.Add(new TextFragment("Sample PDF created in memory."));
        return doc;
    }
}
