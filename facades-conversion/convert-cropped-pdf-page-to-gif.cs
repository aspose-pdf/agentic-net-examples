using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputGif = "page5.gif";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Define the crop region (example coordinates, adjust as needed)
        double llx = 100; // lower‑left X
        double lly = 100; // lower‑left Y
        double urx = 400; // upper‑right X
        double ury = 400; // upper‑right Y

        // ------------------------------------------------------------
        // 1. Load the PDF document.
        // ------------------------------------------------------------
        Document pdfDoc = new Document(inputPdf);

        // ------------------------------------------------------------
        // 2. Apply the CropBox to page 5.
        // ------------------------------------------------------------
        if (pdfDoc.Pages.Count < 5)
        {
            Console.Error.WriteLine("The PDF does not contain a page 5.");
            return;
        }
        pdfDoc.Pages[5].CropBox = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

        // ------------------------------------------------------------
        // 3. Convert the cropped page to a GIF image.
        // ------------------------------------------------------------
        // PdfConverter is not required for GIF conversion; the GifDevice works directly on a Page object.
        // The CropBox set above will be respected.
        GifDevice gifDevice = new GifDevice(new Resolution(150));

        using (FileStream outStream = new FileStream(outputGif, FileMode.Create, FileAccess.Write))
        {
            // Process page 5 (page numbers are 1‑based).
            gifDevice.Process(pdfDoc.Pages[5], outStream);
        }

        Console.WriteLine($"Page 5 has been saved as GIF to '{outputGif}'.");
    }
}
