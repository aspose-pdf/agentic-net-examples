using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string imagePath = "background.jpg";
        const string outputPdf = "result.pdf";

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image not found: {imagePath}");
            return;
        }

        // Load the image once into a byte array – this avoids repeated disk I/O.
        byte[] imgData = File.ReadAllBytes(imagePath);

        // Keep the streams alive until the document is saved.
        List<MemoryStream> openStreams = new List<MemoryStream>();

        // Create a new PDF document.
        using (Document pdfDoc = new Document())
        {
            // ----- First page -------------------------------------------------
            Page firstPage = pdfDoc.Pages.Add();
            // Create a stream from the byte array.
            var imgStream1 = new MemoryStream(imgData);
            openStreams.Add(imgStream1);
            BackgroundArtifact bg1 = new BackgroundArtifact
            {
                BackgroundImage = imgStream1, // <-- use stream directly
                Opacity = 1.0,
                IsBackground = true
            };
            firstPage.Artifacts.Add(bg1);

            // ----- Second page (same background) ----------------------------
            Page secondPage = pdfDoc.Pages.Add();
            var imgStream2 = new MemoryStream(imgData);
            openStreams.Add(imgStream2);
            BackgroundArtifact bg2 = new BackgroundArtifact
            {
                BackgroundImage = imgStream2, // <-- use stream directly
                Opacity = 1.0,
                IsBackground = true
            };
            secondPage.Artifacts.Add(bg2);

            // Save the PDF document.
            pdfDoc.Save(outputPdf);
        }

        // Dispose the streams after the document has been saved.
        foreach (var ms in openStreams)
        {
            ms.Dispose();
        }

        Console.WriteLine($"PDF saved to '{outputPdf}'.");
    }
}
