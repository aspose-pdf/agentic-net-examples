using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing; // for ImageStamp
using StubHttp; // <-- stub namespace for HttpResponse

public class PdfImageHandler
{
    // Adds an image to the first page of a PDF and streams the result to the HTTP response.
    public void AddImageAndSend(HttpResponse response, string pdfPath, string imagePath)
    {
        if (!File.Exists(pdfPath))
            throw new FileNotFoundException("PDF file not found.", pdfPath);
        if (!File.Exists(imagePath))
            throw new FileNotFoundException("Image file not found.", imagePath);

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(pdfPath))
        {
            // ------------------------------------------------------------
            // Insert the image on page 1 using ImageStamp for absolute placement.
            // ------------------------------------------------------------
            Page page = doc.Pages[1];

            // ImageStamp provides explicit positioning (XIndent/YIndent) and scaling.
            ImageStamp stamp = new ImageStamp(imagePath)
            {
                // Size – adjust as required.
                Width = 200,
                Height = 200,
                // Position (lower‑left corner) – values are in points (1/72 inch).
                XIndent = 100,
                YIndent = 500,
                // Optional: keep default alignment (None) for absolute placement.
                // HorizontalAlignment = HorizontalAlignment.None,
                // VerticalAlignment = VerticalAlignment.None
            };
            page.AddStamp(stamp);

            // Save the modified PDF into a memory stream.
            using (MemoryStream ms = new MemoryStream())
            {
                doc.Save(ms);
                ms.Position = 0; // Reset stream for reading.

                // Write the PDF bytes to the HTTP response.
                response.Clear();
                response.ContentType = "application/pdf";
                response.AddHeader("Content-Disposition", "attachment; filename=modified.pdf");
                response.AddHeader("Content-Length", ms.Length.ToString());
                ms.CopyTo(response.OutputStream);
                response.Flush();
                response.End();
            }
        }
    }
}

// ---------------------------------------------------------------------------
// Minimal stub for HttpResponse used when the project is not a Web project.
// ---------------------------------------------------------------------------
namespace StubHttp
{
    public class HttpResponse
    {
        // Initialise to a non‑null default to satisfy the non‑nullable warning.
        public string ContentType { get; set; } = string.Empty;
        private readonly MemoryStream _output = new MemoryStream();
        public Stream OutputStream => _output;

        public void AddHeader(string name, string value) { /* no‑op – stub */ }
        public void Clear() => _output.SetLength(0);
        public void Flush() => _output.Flush();
        public void End() => _output.Flush();

        // Helper for unit‑testing – returns the bytes that would be sent to the client.
        public byte[] GetResponseBytes() => _output.ToArray();
    }
}

// ---------------------------------------------------------------------------
// Dummy entry point so the project compiles as a console application.
// ---------------------------------------------------------------------------
public static class Program
{
    public static void Main(string[] args)
    {
        // No operation – the library is intended to be used via PdfImageHandler.
    }
}
