using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;               // FontRepository and SimpleFontSubstitution
using Aspose.Pdf.Devices;            // BmpDevice and Resolution

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputBmp = "page4.bmp";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document (required for font substitution handling)
        Document pdfDocument = new Document(inputPdf);

        // ------------------------------------------------------------
        // 1. Font substitution – replace missing fonts with alternatives
        // ------------------------------------------------------------
        // Clear any previous substitutions and add a simple substitution.
        FontRepository.Substitutions.Clear();
        // SimpleFontSubstitution maps a missing font name to a replacement.
        FontRepository.Substitutions.Add(new SimpleFontSubstitution("Times New Roman", "Arial"));

        // ------------------------------------------------------------
        // 2. Configure the PdfConverter (required only for the page‑range
        //    and resolution settings – we will still render with BmpDevice).
        // ------------------------------------------------------------
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the already‑loaded Document instance.
            converter.BindPdf(pdfDocument);

            // Convert only page 4.
            converter.StartPage = 4;
            converter.EndPage   = 4;

            // High‑quality rendering: 300 DPI.
            converter.Resolution = new Resolution(300);

            // Perform the conversion – this step prepares the internal
            // rasterisation engine but we will not use the built‑in image
            // exporters (they do not support BMP on non‑Windows platforms).
            converter.DoConvert();
        }

        // ------------------------------------------------------------
        // 3. Render the prepared page to BMP using BmpDevice.
        // ------------------------------------------------------------
        // BmpDevice respects the resolution set on the PdfConverter
        // (if the converter was used) and provides cross‑platform BMP output.
        BmpDevice bmpDevice = new BmpDevice(new Resolution(300));
        // Process the specific page and write the BMP file.
        bmpDevice.Process(pdfDocument.Pages[4], outputBmp);

        Console.WriteLine($"Page 4 has been converted to BMP: {outputBmp}");
    }
}
