using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

// Minimal stub to mimic ASP.NET Core HttpResponse in a console app
namespace StubHttp
{
    public class HttpResponse
    {
        // Initialise to an empty string to satisfy the non‑nullable warning.
        public string ContentType { get; set; } = string.Empty;
        public Dictionary<string, string> Headers { get; } = new Dictionary<string, string>();
        private readonly MemoryStream _output = new MemoryStream();
        public Stream Body => _output;
        // Helper for testing / demo purposes
        public byte[] GetResponseBytes() => _output.ToArray();
    }
}

class Program
{
    static void Main()
    {
        // ---------------------------------------------------------------------
        // 1️⃣ Create a sample PDF that the example can work with.
        // ---------------------------------------------------------------------
        const string pdfPath = "sample.pdf";
        using (var seed = new Document())
        {
            // Add a blank page (required for the stamp operation).
            seed.Pages.Add();
            // Optionally add some text so the PDF is not completely empty.
            var paragraph = new Aspose.Pdf.Text.TextFragment("Original PDF – placeholder text.");
            seed.Pages[1].Paragraphs.Add(paragraph);
            seed.Save(pdfPath);
        }

        // ---------------------------------------------------------------------
        // 2️⃣ Create a tiny PNG image (1×1 pixel) that will be stamped onto the PDF.
        // ---------------------------------------------------------------------
        const string imagePath = "logo.png";
        // Base64‑encoded 1×1 white PNG.
        const string pngBase64 = "iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAQAAAC1HAwCAAAAC0lEQVR42mP8/x8AAwMCAO+XK6cAAAAASUVORK5CYII=";
        File.WriteAllBytes(imagePath, Convert.FromBase64String(pngBase64));

        // ---------------------------------------------------------------------
        // 3️⃣ Run the handler that adds the image and writes the result to the
        //    simulated HttpResponse.
        // ---------------------------------------------------------------------
        var response = new StubHttp.HttpResponse();
        var handler = new PdfImageHandler();
        handler.AddImageAndSend(response, pdfPath, imagePath);

        // Demonstrate the result by writing the response bytes to a file.
        File.WriteAllBytes("modified.pdf", response.GetResponseBytes());
        Console.WriteLine("PDF with inserted image saved as 'modified.pdf'.");
    }
}

public class PdfImageHandler
{
    /// <summary>
    /// Adds an image to the first page of a PDF and writes the result to the provided HttpResponse stub.
    /// </summary>
    public void AddImageAndSend(StubHttp.HttpResponse response, string pdfPath, string imagePath)
    {
        // Validate input files – defensive programming for the sandbox.
        if (!File.Exists(pdfPath))
            throw new FileNotFoundException($"PDF not found: {pdfPath}");
        if (!File.Exists(imagePath))
            throw new FileNotFoundException($"Image not found: {imagePath}");

        // Load the PDF using the high‑level Document API.
        var document = new Document(pdfPath);

        // Define the rectangle where the image will be placed (lower‑left X/Y, upper‑right X/Y).
        double llx = 100, lly = 500, urx = 300, ury = 700;
        double width = urx - llx;
        double height = ury - lly;

        // Create an ImageStamp and configure its size and position.
        var imageStamp = new ImageStamp(imagePath)
        {
            Width = width,
            Height = height,
            LeftMargin = llx,
            BottomMargin = lly
        };

        // Add the stamp to the first page.
        document.Pages[1].AddStamp(imageStamp);

        // Save the modified PDF into a memory stream.
        using (var pdfStream = new MemoryStream())
        {
            document.Save(pdfStream, SaveFormat.Pdf);
            pdfStream.Position = 0; // Reset for reading.

            // Set response headers (simulated).
            response.ContentType = "application/pdf";
            response.Headers["Content-Disposition"] = "attachment; filename=\"modified.pdf\"";

            // Write the PDF bytes to the response body.
            pdfStream.CopyTo(response.Body);
        }
    }
}
