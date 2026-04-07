using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a sample PDF and add an image
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Create a tiny PNG file (1x1 pixel) if it does not exist
            string imagePath = "sample.png";
            if (!File.Exists(imagePath))
            {
                byte[] pngBytes = Convert.FromBase64String(
                    "iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAQAAAC1HAwCAAAAC0lEQVR42mP8/x8AAwMCAO+X2V8AAAAASUVORK5CYII=");
                File.WriteAllBytes(imagePath, pngBytes);
            }

            // Add the image to the page
            Image image = new Image();
            image.File = imagePath;
            page.Paragraphs.Add(image);

            // Locate the image index inside the page's paragraph collection (zero‑based indexing)
            int imageIndex = -1;
            for (int i = 0; i < page.Paragraphs.Count; i++)
            {
                if (page.Paragraphs[i] is Image)
                {
                    imageIndex = i;
                    break;
                }
            }

            // Build a simple 2‑column table
            Table table = new Table();
            table.ColumnWidths = "100 100";
            Row headerRow = table.Rows.Add();
            headerRow.Cells.Add("Header 1");
            headerRow.Cells.Add("Header 2");
            Row dataRow = table.Rows.Add();
            dataRow.Cells.Add("Cell 1");
            dataRow.Cells.Add("Cell 2");

            // Insert the table before the image if the image was found
            if (imageIndex != -1)
            {
                page.Paragraphs.Insert(imageIndex, table);
            }

            // Save the resulting PDF – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
            string outputPath = "output.pdf";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                // Windows has native GDI+ – safe to call Save directly
                doc.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
            else
            {
                // On macOS / Linux Aspose.Pdf may require libgdiplus for rendering.
                // Attempt to save and handle the possible TypeInitializationException gracefully.
                try
                {
                    doc.Save(outputPath);
                    Console.WriteLine($"PDF saved to '{outputPath}'.");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                                      "The PDF was generated in memory but could not be saved to disk.");
                }
            }
        }
    }

    // Helper that walks the inner‑exception chain to detect a missing native library.
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
