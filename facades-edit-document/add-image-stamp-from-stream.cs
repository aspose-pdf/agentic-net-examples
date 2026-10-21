using System;
using System.IO;
using System.Text;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";

        // ------------------------------------------------------------
        // 1️⃣ Ensure the source PDF exists – create a simple placeholder
        //    if it is missing. This makes the sample self‑contained.
        // ------------------------------------------------------------
        if (!File.Exists(inputPdfPath))
        {
            var placeholder = new Document();
            placeholder.Pages.Add();
            placeholder.Save(inputPdfPath);
        }

        // ------------------------------------------------------------
        // 2️⃣ Prepare an image *in memory* – no file on disk is used.
        //    The example uses a tiny 1×1 red PNG encoded as Base64.
        //    Replace the Base64 string with any image you need.
        // ------------------------------------------------------------
        const string base64Png =
            "iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAQAAAC1HAwCAAAAC0lEQVR42mP8/x8AAwMCAO+XK6cAAAAASUVORK5CYII="; // 1x1 red pixel
        byte[] imageBytes = Convert.FromBase64String(base64Png);
        using var imageStream = new MemoryStream(imageBytes);

        // ------------------------------------------------------------
        // 3️⃣ Create the ImageStamp directly from the stream.
        // ------------------------------------------------------------
        var imgStamp = new ImageStamp(imageStream)
        {
            Background = false,                     // place on top of page content
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            Opacity = 0.7                           // semi‑transparent
        };

        // ------------------------------------------------------------
        // 4️⃣ Load the PDF and apply the stamp to every page.
        // ------------------------------------------------------------
        using var pdfDoc = new Document(inputPdfPath);
        foreach (Page page in pdfDoc.Pages)
        {
            page.AddStamp(imgStamp);
        }

        // ------------------------------------------------------------
        // 5️⃣ Save the modified PDF.
        // ------------------------------------------------------------
        pdfDoc.Save(outputPdfPath);

        Console.WriteLine($"Image stamp applied and saved to '{outputPdfPath}'.");
    }
}
