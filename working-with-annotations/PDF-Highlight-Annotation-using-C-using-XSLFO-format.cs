using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // ------------------------------------------------------------
        // NOTE:
        //   • On non‑Windows platforms Aspose.Pdf relies on System.Drawing
        //     (GDI+) for XSL‑FO rendering.  The required native library is
        //     libgdiplus.  The code below checks the platform and aborts with
        //     a clear message if libgdiplus is missing.
        //   • The switch "System.Drawing.EnableUnixSupport" must be set **before**
        //     any Aspose.Pdf type that touches System.Drawing is instantiated.
        //   • Use System.Drawing.Common version 5.0.2 (or any 5.x) – newer
        //     versions are Windows‑only and will throw a PlatformNotSupportedException.
        // ------------------------------------------------------------
        AppContext.SetSwitch("System.Drawing.EnableUnixSupport", true);

        // Verify that the required native GDI+ implementation is present on *nix.
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            const string libName = "libgdiplus";
            try
            {
                // Attempt to load the native library – this will throw on failure.
                NativeLibrary.Load(libName);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"The native library '{libName}' is required for System.Drawing on this platform.");
                Console.Error.WriteLine("Install it (e.g., 'apt-get install -y libgdiplus' on Debian/Ubuntu) and retry.");
                Console.Error.WriteLine($"Detailed error: {ex.Message}");
                return;
            }
        }

        const string xslFoPath = "input.xslfo";
        const string outputPdf = "output.pdf";

        if (!File.Exists(xslFoPath))
        {
            Console.Error.WriteLine($"XSL-FO file not found: {xslFoPath}");
            return;
        }

        // Load the XSL‑FO file and convert it to a PDF document
        XslFoLoadOptions loadOptions = new XslFoLoadOptions();
        using (Document pdfDoc = new Document(xslFoPath, loadOptions))
        {
            // Ensure at least one page was created
            if (pdfDoc.Pages.Count == 0)
            {
                Console.Error.WriteLine("No pages were generated from the XSL‑FO source.");
                return;
            }

            // Define the rectangle (in points) where the highlight will appear
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 520);

            // Create a highlight annotation on the first page
            HighlightAnnotation highlight = new HighlightAnnotation(pdfDoc.Pages[1], rect)
            {
                Color = Aspose.Pdf.Color.Yellow,   // visual highlight color
                Contents = "Highlighted text"       // optional tooltip text
            };

            // Add the annotation to the page's annotation collection
            pdfDoc.Pages[1].Annotations.Add(highlight);

            // Save the modified PDF
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with highlight annotation saved to '{outputPdf}'.");
    }
}
