using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document with deterministic disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // Verify that page 21 exists (Aspose.Pdf uses 1‑based indexing)
            if (pdfDoc.Pages.Count < 21)
            {
                Console.Error.WriteLine("The PDF does not contain a page 21.");
                return;
            }

            // Get page 21
            Page page = pdfDoc.Pages[21];

            // Configure TextDevice to extract formatted text in Pure mode
            var extractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);

            // TextDevice does NOT implement IDisposable, so do NOT use a using block
            TextDevice textDevice = new TextDevice(extractionOptions);

            // Extract the text. The device can write directly to a stream; we capture it in memory.
            using (MemoryStream ms = new MemoryStream())
            {
                textDevice.Process(page, ms);
                string formattedText = Encoding.UTF8.GetString(ms.ToArray());

                // Write the extracted text to a plain‑text file
                File.WriteAllText(outputPath, formattedText);
            }
        }

        Console.WriteLine($"Formatted text from page 21 saved to '{outputPath}'.");
    }
}