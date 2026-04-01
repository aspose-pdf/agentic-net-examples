using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pngPath = "image.png";
        const string gifPath = "overlay.gif";
        const string basePdf = "base.pdf";
        const string outputPdf = "output.pdf";

        // Create a PDF containing a PNG image
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();
            Aspose.Pdf.Image png = new Aspose.Pdf.Image();
            png.File = pngPath;
            // Position and size the PNG (example values)
            png.FixWidth = 500f;
            png.FixHeight = 700f;
            page.Paragraphs.Add(png);

            // Guard Document.Save on platforms that lack GDI+ (e.g., macOS/Linux without libgdiplus)
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(basePdf);
            }
            else
            {
                Console.WriteLine("Skipping PDF save on non‑Windows platform because GDI+ (libgdiplus) is not available.");
            }
        }

        // If the base PDF was not created (non‑Windows), skip the overlay step.
        if (!File.Exists(basePdf))
        {
            Console.WriteLine($"Base PDF '{basePdf}' not found – overlay step skipped.");
            return;
        }

        // Overlay a semi‑transparent GIF using PdfFileMend and CompositingParameters
        // Use the parameter‑less constructor and bind the PDF explicitly (string overload).
        PdfFileMend mend = new PdfFileMend();
        mend.BindPdf(basePdf);
        using (FileStream gifStream = File.OpenRead(gifPath))
        {
            // Normal blend mode; the GIF's own transparency will be respected
            CompositingParameters compParams = new CompositingParameters(BlendMode.Normal);
            // Add GIF to page 1 at the desired rectangle (x, y, width, height)
            mend.AddImage(gifStream, 1, 100f, 100f, 300f, 300f, compParams);
        }
        // Save the modified PDF to the desired output path.
        mend.Save(outputPdf);
        mend.Close();

        Console.WriteLine("Overlay completed: " + outputPdf);
    }
}
