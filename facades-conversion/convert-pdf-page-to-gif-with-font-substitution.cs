using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputGif = "page6.gif";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Map missing fonts to substitute fonts before any rendering occurs.
        FontRepository.Substitutions.Add(new SimpleFontSubstitution("TimesNewRomanPSMT", "Arial"));

        // Load the PDF document.
        using (Document doc = new Document(inputPdf))
        {
            // Bind the document to a PdfConverter to force the font substitution to be applied.
            using (PdfConverter converter = new PdfConverter())
            {
                converter.BindPdf(doc);
                // Ensure substitution takes effect before rendering.
                converter.DoConvert();
            }

            // Convert page 6 (1‑based index) to a GIF image using the substituted fonts.
            var resolution = new Resolution(150); // DPI – adjust as needed.
            var gifDevice = new GifDevice(resolution); // Image devices are not IDisposable; instantiate directly.

            // Render the specific page to a GIF file.
            gifDevice.Process(doc.Pages[6], outputGif);
        }

        Console.WriteLine($"Page 6 saved as GIF to '{outputGif}'.");
    }
}
