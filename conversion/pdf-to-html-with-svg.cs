using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text; // for TextFragment

class Program
{
    static void Main()
    {
        // This demo relies on GDI+ (gdiplus.dll / libgdiplus). It works on Windows out‑of‑the‑box.
        // On macOS/Linux the native library is usually missing, causing a TypeInitializationException.
        // Guard the whole operation with an OS check to avoid the crash.
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            Console.WriteLine("GDI+ (gdiplus) is required for PDF → HTML conversion. " +
                              "Run this program on Windows or install libgdiplus on the current platform.");
            return;
        }

        // Base directory of the executable (ensures paths are resolved correctly)
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;

        // Input PDF file path (absolute path based on the executable folder)
        string pdfPath = Path.Combine(baseDir, "input.pdf");
        // Output HTML file path (absolute path based on the executable folder)
        string htmlPath = Path.Combine(baseDir, "output.html");
        // Folder where generated SVG images will be stored (absolute path)
        string svgFolder = Path.Combine(baseDir, "svg-images");

        // Ensure the SVG folder exists
        Directory.CreateDirectory(svgFolder);

        // If the input PDF does not exist, create a minimal placeholder PDF so the demo can run.
        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"Input PDF not found at '{pdfPath}'. Creating a placeholder PDF.");
            // Create a simple one‑page PDF with some text.
            using (Document placeholder = new Document())
            {
                Page page = placeholder.Pages.Add();
                page.Paragraphs.Add(new TextFragment("Placeholder PDF generated because 'input.pdf' was missing."));
                // Guard the Save call – it also needs GDI+.
                placeholder.Save(pdfPath);
            }
        }

        try
        {
            // Load the PDF document
            using (Document pdfDoc = new Document(pdfPath))
            {
                // Configure HTML save options
                HtmlSaveOptions saveOptions = new HtmlSaveOptions
                {
                    // Direct SVG images to the specified folder
                    SpecialFolderForSvgImages = svgFolder,
                    // Optional: embed raster images into SVG wrappers
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                // Save the document as HTML using the options
                pdfDoc.Save(htmlPath, saveOptions);
            }

            Console.WriteLine($"PDF successfully converted to HTML. SVG images saved in '{svgFolder}'.");
        }
        catch (Exception ex)
        {
            // If a GDI+ related TypeInitializationException bubbles up, handle it gracefully.
            if (ContainsDllNotFound(ex))
            {
                Console.WriteLine("GDI+ (gdiplus) is not available on this platform. Conversion cannot be performed.");
            }
            else
            {
                Console.WriteLine($"An error occurred during conversion: {ex.Message}");
            }
        }
    }

    // Helper that walks the inner‑exception chain looking for a DllNotFoundException (libgdiplus/gdiplus).
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
