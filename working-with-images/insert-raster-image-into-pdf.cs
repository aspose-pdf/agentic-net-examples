using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;

class InsertImageExample
{
    static void Main()
    {
        // Paths to the source PDF, the raster image, and the output PDF
        const string pdfPath   = "input.pdf";
        const string imgPath   = "image.jpg";
        const string outPath   = "output.pdf";

        // Desired absolute coordinates (points) on the page
        // lower‑left corner (x1, y1) and upper‑right corner (x2, y2)
        const float llx = 100f;   // lower‑left X
        const float lly = 500f;   // lower‑left Y
        const float urx = 300f;   // upper‑right X
        const float ury = 700f;   // upper‑right Y

        // Ensure the source files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }
        if (!File.Exists(imgPath))
        {
            Console.Error.WriteLine($"Image file not found: {imgPath}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            // Aspose.Pdf uses 1‑based page indexing; here we modify the first page
            Page page = doc.Pages[1];

            // Define the rectangle where the image will be placed
            // Fully qualified type name avoids ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle imgRect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

            // Open the image file as a stream and add it to the page
            using (FileStream imgStream = File.OpenRead(imgPath))
            {
                // This overload centers the image within the rectangle while preserving its aspect ratio
                page.AddImage(imgStream, imgRect);
            }

            // Save the modified document.
            // On non‑Windows platforms Aspose.Pdf may require libgdiplus; guard against that.
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outPath);
                Console.WriteLine($"Image inserted and PDF saved to '{outPath}'.");
            }
            else
            {
                try
                {
                    doc.Save(outPath);
                    Console.WriteLine($"Image inserted and PDF saved to '{outPath}'.");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Saving failed: GDI+ (libgdiplus) is not available on this platform.");
                }
            }
        }
    }

    // Helper to detect a nested DllNotFoundException (libgdiplus missing)
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