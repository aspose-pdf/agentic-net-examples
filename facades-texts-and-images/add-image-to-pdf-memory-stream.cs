using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf.Facades;

public class PdfImageService
{
    /// <summary>
    /// Adds an image to the first page of an existing PDF and returns the modified PDF as a <see cref="MemoryStream"/>.
    /// The caller can then write the stream to any destination (file, HTTP response, etc.).
    /// </summary>
    /// <param name="pdfFilePath">Path to the source PDF.</param>
    /// <param name="imageFilePath">Path to the image to be placed on the PDF.</param>
    /// <returns>A <see cref="MemoryStream"/> containing the modified PDF. The stream's position is set to 0.</returns>
    public MemoryStream AddImageAndGetStream(string pdfFilePath, string imageFilePath)
    {
        if (string.IsNullOrEmpty(pdfFilePath))
            throw new ArgumentException("PDF path is required.", nameof(pdfFilePath));
        if (string.IsNullOrEmpty(imageFilePath))
            throw new ArgumentException("Image path is required.", nameof(imageFilePath));

        if (!File.Exists(pdfFilePath))
            throw new FileNotFoundException($"PDF file not found: {pdfFilePath}");
        if (!File.Exists(imageFilePath))
            throw new FileNotFoundException($"Image file not found: {imageFilePath}");

        // The MemoryStream will hold the resulting PDF.
        var outputStream = new MemoryStream();

        // PdfFileMend is a save‑able facade that allows adding images to an existing PDF.
        using (var pdfMend = new PdfFileMend())
        {
            // Bind the existing PDF document.
            pdfMend.BindPdf(pdfFilePath);

            // Add the image to page 1.
            // Parameters: image path, pages array, lower‑left X, lower‑left Y, upper‑right X, upper‑right Y.
            pdfMend.AddImage(imageFilePath, new int[] { 1 }, 100f, 500f, 300f, 700f);

            // Save the modified PDF into the memory stream.
            pdfMend.Save(outputStream);
        }

        // Reset the stream so the caller can read from the beginning.
        outputStream.Position = 0;
        return outputStream;
    }
}

// ------------------------------------------------------------
// Example usage in a console application (no ASP.NET dependencies).
// ------------------------------------------------------------
class Program
{
    static void Main()
    {
        // -----------------------------------------------------------------
        // 1. Create a minimal source PDF (input.pdf) if it does not already exist.
        // -----------------------------------------------------------------
        const string pdfPath = "input.pdf";
        if (!File.Exists(pdfPath))
        {
            // Create a one‑page PDF using Aspose.Pdf.Document.
            var placeholderDoc = new Aspose.Pdf.Document();
            placeholderDoc.Pages.Add();
            placeholderDoc.Save(pdfPath);
        }

        // -----------------------------------------------------------------
        // 2. Create a simple PNG image (logo.png) to be added to the PDF.
        // -----------------------------------------------------------------
        const string imagePath = "logo.png";
        if (!File.Exists(imagePath))
        {
            using (var bmp = new System.Drawing.Bitmap(200, 200))
            {
                using (var gfx = System.Drawing.Graphics.FromImage(bmp))
                {
                    gfx.Clear(System.Drawing.Color.LightBlue);
                    using (var pen = new System.Drawing.Pen(System.Drawing.Color.DarkBlue, 5))
                    {
                        gfx.DrawEllipse(pen, 25, 25, 150, 150);
                    }
                }
                bmp.Save(imagePath, System.Drawing.Imaging.ImageFormat.Png);
            }
        }

        var service = new PdfImageService();
        using (MemoryStream pdfStream = service.AddImageAndGetStream(pdfPath, imagePath))
        {
            // Write the stream to a file – in a real web app you could copy it to HttpResponse.Body.
            File.WriteAllBytes("modified.pdf", pdfStream.ToArray());
            Console.WriteLine("Modified PDF saved as 'modified.pdf'.");
        }
    }
}
