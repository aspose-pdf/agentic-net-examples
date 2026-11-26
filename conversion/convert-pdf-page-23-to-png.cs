using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Text; // Required for FontRepository if needed in future

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPng = "page23.png";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // No explicit DefaultFontName – Aspose.PDF falls back to Times New Roman when a font is missing.
        // If you need to enforce a substitution, you can add a SimpleFontSubstitution like this:
        // FontRepository.Substitutions.Add(new SimpleFontSubstitution("*", "Times New Roman"));

        using (Document pdfDoc = new Document(inputPdf))
        {
            if (pdfDoc.Pages.Count < 23)
            {
                Console.Error.WriteLine("The document has fewer than 23 pages.");
                return;
            }

            Page page = pdfDoc.Pages[23];

            // Create a PNG device with 150 DPI resolution.
            PngDevice pngDevice = new PngDevice(150, 150);

            using (MemoryStream ms = new MemoryStream())
            {
                pngDevice.Process(page, ms);
                File.WriteAllBytes(outputPng, ms.ToArray());
            }
        }

        Console.WriteLine($"Page 23 has been saved as PNG to '{outputPng}'.");
    }
}
