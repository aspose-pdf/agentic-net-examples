using System;
using System.IO;
using System.Reflection;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputHtmlPath = "output.html";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal.
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // HtmlSaveOptions – rasterize each PDF page to an image.
                // The older ConvertPagesToImages and ImageDpi properties are no longer available.
                // Use RasterImagesSavingMode to force rasterization and, if the ImageDpi property exists in the
                // runtime version, set it via reflection to keep the code compatible with all supported versions.
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    // Rasterize pages as external PNG files referenced via SVG (each page becomes an image).
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsExternalPngFilesReferencedViaSvg,
                    // Optional settings – keep a single HTML file and avoid embedding parts.
                    SplitIntoPages = false,
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.NoEmbedding
                };

                // Set ImageDpi = 150 if the property exists (some newer versions expose it).
                PropertyInfo dpiProp = typeof(HtmlSaveOptions).GetProperty("ImageDpi", BindingFlags.Public | BindingFlags.Instance);
                if (dpiProp != null && dpiProp.CanWrite)
                {
                    dpiProp.SetValue(htmlOpts, 150);
                }

                // HTML conversion uses GDI+ and works only on Windows.
                // Wrap the save call in a try‑catch to handle non‑Windows platforms gracefully.
                try
                {
                    pdfDoc.Save(outputHtmlPath, htmlOpts);
                    Console.WriteLine($"HTML saved to '{outputHtmlPath}'.");
                }
                catch (TypeInitializationException)
                {
                    Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
