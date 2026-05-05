using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Aspose.Pdf requires GDI+ (libgdiplus) on non‑Windows platforms. Guard the whole PDF generation.
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            Console.WriteLine("PDF generation is skipped because GDI+ (libgdiplus) is not available on this platform.");
            return;
        }

        // Collection of HTML fragments to be rendered into a single PDF.
        List<string> htmlFragments = new List<string>
        {
            "<html><body><h1>First Document</h1><p>Hello, world!</p></body></html>",
            "<html><body><h2>Second Document</h2><p>Another paragraph.</p></body></html>"
            // Add more HTML strings as needed.
        };

        // Output PDF file path.
        const string outputPdfPath = "CombinedOutput.pdf";

        // Ensure there is at least one fragment to process.
        if (htmlFragments.Count == 0)
        {
            Console.Error.WriteLine("No HTML fragments provided.");
            return;
        }

        // Create the final PDF document that will hold all pages.
        using (Document resultDocument = new Document())
        {
            // Process each HTML string.
            foreach (string html in htmlFragments)
            {
                // Convert the HTML string to a byte array (UTF‑8 encoding).
                byte[] htmlBytes = Encoding.UTF8.GetBytes(html);

                // Load the HTML from a memory stream with custom rendering options.
                using (MemoryStream htmlStream = new MemoryStream(htmlBytes))
                {
                    // HtmlLoadOptions allows fine‑grained control over the conversion.
                    HtmlLoadOptions loadOptions = new HtmlLoadOptions
                    {
                        // Example custom options – adjust as required.
                        IsEmbedFonts = true,               // Embed fonts into the PDF.
                        IsRenderToSinglePage = false       // Preserve original page layout.
                        // Additional options can be set here, e.g. BasePath, DisableFontLicenseVerifications, etc.
                    };

                    // Load the HTML into a temporary PDF document.
                    using (Document tempDocument = new Document(htmlStream, loadOptions))
                    {
                        // Append all pages from the temporary document to the result document.
                        resultDocument.Pages.Add(tempDocument.Pages);
                    }
                }
            }

            // Save the combined PDF to disk – wrapped in a safe guard for GDI+.
            SaveDocument(resultDocument, outputPdfPath);
        }
    }

    private static void SaveDocument(Document doc, string path)
    {
        // On Windows the native GDI+ library is always present, so we can save directly.
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            doc.Save(path);
            Console.WriteLine($"PDF successfully created at '{path}'.");
            return;
        }

        // On non‑Windows platforms we already prevented PDF generation, but keep a defensive fallback.
        try
        {
            doc.Save(path);
            Console.WriteLine($"PDF successfully created at '{path}'.");
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
