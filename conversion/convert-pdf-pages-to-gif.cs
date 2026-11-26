using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "GifPages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // GifDevice does NOT implement IDisposable; create it once with a Resolution.
            var gifDevice = new GifDevice(new Resolution(150)); // default DPI can be changed as needed

            // Iterate pages using 1‑based indexing (Aspose.Pdf uses 1‑based page numbers)
            for (int i = 1; i <= pdfDoc.Pages.Count; i++)
            {
                string gifPath = Path.Combine(outputFolder, $"Page_{i}.gif");

                // Create a file stream for the GIF output
                using (FileStream gifStream = new FileStream(gifPath, FileMode.Create, FileAccess.Write))
                {
                    // Process the page and write the GIF image to the stream
                    gifDevice.Process(pdfDoc.Pages[i], gifStream);
                }

                Console.WriteLine($"Page {i} saved as GIF: {gifPath}");
            }
        }

        Console.WriteLine("PDF to GIF conversion completed.");
    }
}
