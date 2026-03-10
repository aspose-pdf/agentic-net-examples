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
        // 1. Enable System.Drawing support on non‑Windows platforms.
        //    This switch *must* be set **before** any Aspose.Pdf types are
        //    touched (including the static constructors that initialise
        //    System.Drawing).  It tells the System.Drawing.Common library
        //    to fall back to the "Unix" implementation that works with
        //    libgdiplus.
        // ------------------------------------------------------------
        AppContext.SetSwitch("System.Drawing.EnableUnixSupport", true);
        // The following switch is required for .NET 7+ where the legacy
        // GDI+ code path is disabled by default.
        AppContext.SetSwitch("System.Drawing.UseLegacyGDIPlus", true);

        // On Linux/macOS the native libgdiplus library must be present.
        // Attempt to load it explicitly so that a clearer error is shown
        // if it is missing.
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            try
            {
                NativeLibrary.Load("libgdiplus");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(
                    "Unable to load libgdiplus – required for System.Drawing on non‑Windows platforms.\n" +
                    ex.Message);
                return;
            }
        }

        const string inputPdfPath = "input.pdf";   // source PDF
        const string outputEpubPath = "output.epub"; // target EPUB

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // ------------------------------------------------------------
        // 2. Load the PDF document.
        // ------------------------------------------------------------
        using (Document doc = new Document(inputPdfPath))
        {
            // --------------------------------------------------------
            // 3. Add a WatermarkAnnotation to every page.
            //    The WatermarkAnnotation class internally uses
            //    System.Drawing, therefore the switches above are
            //    essential on Linux/macOS.
            // --------------------------------------------------------
            foreach (Page page in doc.Pages)
            {
                // Rectangle where the watermark will be placed.
                // (llx, lly, urx, ury) – lower‑left and upper‑right corners.
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 400, 550);

                WatermarkAnnotation watermark = new WatermarkAnnotation(page, rect)
                {
                    Contents = "CONFIDENTIAL",
                    Color = Aspose.Pdf.Color.Red,
                    Opacity = 0.5,
                    // ZIndex < 0 places the annotation behind the page content.
                    ZIndex = -1
                };

                page.Annotations.Add(watermark);
            }

            // --------------------------------------------------------
            // 4. Configure EPUB save options.
            // --------------------------------------------------------
            EpubSaveOptions epubOptions = new EpubSaveOptions
            {
                Title = "PDF with Watermark"
            };

            // --------------------------------------------------------
            // 5. Save the document as EPUB.
            // --------------------------------------------------------
            doc.Save(outputEpubPath, epubOptions);
        }

        Console.WriteLine($"Watermarked EPUB saved to '{outputEpubPath}'.");
    }
}
