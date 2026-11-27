using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;
using Aspose.Pdf.Devices;

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

        // Substitute missing fonts (e.g., Times New Roman -> Arial)
        FontRepository.Substitutions.Add(
            new SimpleFontSubstitution("Times New Roman", "Arial"));

        // Load the PDF document.
        Document pdfDocument = new Document(inputPdf);

        // Use PdfConverter to set the desired resolution and page range.
        using (PdfConverter converter = new PdfConverter())
        {
            converter.BindPdf(pdfDocument);
            converter.StartPage = 5;          // convert only page 5
            converter.EndPage   = 5;
            converter.Resolution = new Resolution(72); // low DPI (72)
            converter.DoConvert();

            // PdfConverter cannot write GIF directly; use GifDevice on the same page.
            // The device respects the same resolution that was set on the converter.
            using (FileStream outStream = new FileStream(outputGif, FileMode.Create))
            {
                GifDevice gifDevice = new GifDevice(new Resolution(72));
                gifDevice.Process(pdfDocument.Pages[5], outStream);
            }
        }

        Console.WriteLine($"Page 5 has been saved as GIF to '{outputGif}'.");
    }
}
