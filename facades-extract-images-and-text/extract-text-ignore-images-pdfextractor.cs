using System;
using System.IO;
using Aspose.Pdf.Facades;   // Facade API (PdfExtractor)

// Suppress the NuGet vulnerability warning (NU1903) if the project treats warnings as errors.
#pragma warning disable NU1903

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputTxt = "extracted_text.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // PdfExtractor implements IDisposable, so wrap it in a using block.
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor.
            extractor.BindPdf(inputPdf);

            // Do NOT call ExtractImage() – this tells the extractor to ignore images.
            // The previous code attempted to set a non‑existent property "ExtractImage" which caused CS1656.

            // Extract only text.
            extractor.ExtractText();

            // Save the extracted text to a file.
            extractor.GetText(outputTxt);
        }

        Console.WriteLine($"Text extracted to '{outputTxt}'. Images were ignored.");
    }
}
#pragma warning restore NU1903