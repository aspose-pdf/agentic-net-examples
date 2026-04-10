using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf;
using System.Drawing;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string resizedPath = "resized.pdf";

        // Desired dimensions in points (1 point = 1/72 inch). Assuming 72 DPI, pixels == points.
        const double targetWidth = 1024; // points
        const double targetHeight = 768; // points

        // ---------------------------------------------------------------------
        // Ensure a source PDF exists – create a simple one if it does not.
        // ---------------------------------------------------------------------
        if (!File.Exists(inputPath))
        {
            Document sampleDoc = new Document();
            Page page = sampleDoc.Pages.Add();
            page.PageInfo.Width = targetWidth;   // make the original size similar to target for demo
            page.PageInfo.Height = targetHeight;
            // Add a simple paragraph so the page is not empty.
            page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment("Sample PDF for resize demo"));
            sampleDoc.Save(inputPath);
            Console.WriteLine($"Created placeholder PDF at '{inputPath}'.");
        }

        // ---------------------------------------------------------------------
        // Resize PDF contents using the stream overload.
        // ---------------------------------------------------------------------
        using (FileStream srcStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        using (FileStream destStream = new FileStream(resizedPath, FileMode.Create, FileAccess.Write))
        {
            PdfFileEditor fileEditor = new PdfFileEditor();
            // null => all pages; newWidth and newHeight are in default space units (points)
            bool success = fileEditor.ResizeContents(srcStream, destStream, null, targetWidth, targetHeight);
            if (!success)
            {
                Console.Error.WriteLine("Resize operation failed.");
                return;
            }
        }

        // ---------------------------------------------------------------------
        // Verify visual fidelity by rendering the first page to an image and
        // checking its size.
        // ---------------------------------------------------------------------
        using (FileStream resizedStream = new FileStream(resizedPath, FileMode.Open, FileAccess.Read))
        using (MemoryStream imageStream = new MemoryStream())
        {
            PdfConverter converter = new PdfConverter();
            converter.BindPdf(resizedStream);
            converter.DoConvert();

            // Extract first page as PNG (dimensions are derived from the PDF page size)
#pragma warning disable CA1416 // Validate platform compatibility – System.Drawing is Windows‑specific
            converter.GetNextImage(imageStream, ImageFormat.Png);
#pragma warning restore CA1416 // Validate platform compatibility

            // Reset the stream position before loading the image.
            imageStream.Position = 0;

            // Load the image to inspect its dimensions.
            using (System.Drawing.Image img = System.Drawing.Image.FromStream(imageStream))
            {
#pragma warning disable CA1416 // Validate platform compatibility – System.Drawing is Windows‑specific
                Console.WriteLine($"Rendered image size: {img.Width}x{img.Height} pixels");
                if (Math.Abs(img.Width - targetWidth) < 1 && Math.Abs(img.Height - targetHeight) < 1)
                {
                    Console.WriteLine("Visual fidelity verified: image dimensions match target size.");
                }
                else
                {
                    Console.WriteLine("Warning: rendered image dimensions do not match target size.");
                }
#pragma warning restore CA1416 // Validate platform compatibility
            }
        }
    }
}
