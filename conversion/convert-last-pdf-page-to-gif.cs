using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";   // source PDF
        const string outputGif = "last_page.gif"; // result GIF file

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF and ensure deterministic disposal of the Document
        using (Document doc = new Document(inputPdf))
        {
            // Aspose.Pdf uses 1‑based indexing for the Pages collection.
            int lastPageNumber = doc.Pages.Count; // 1‑based index of the last page

            // Devices (GifDevice, PngDevice, etc.) do NOT implement IDisposable.
            // Instantiate them directly without a using block.
            Resolution resolution = new Resolution(300);
            GifDevice gifDevice = new GifDevice(resolution);

            // Render the last page to a GIF file using default conversion settings.
            gifDevice.Process(doc.Pages[lastPageNumber], outputGif);
        }

        Console.WriteLine($"Last page saved as GIF: {outputGif}");
    }
}
