using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath   = "input.pdf";
        const string outputPdfPath  = "output_with_barcode_header.pdf";
        const string barcodeImgPath = "barcode.png"; // pre‑generated barcode image

        // Verify files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(barcodeImgPath))
        {
            Console.Error.WriteLine($"Barcode image not found: {barcodeImgPath}");
            return;
        }

        // PdfFileStamp does NOT implement IDisposable – no using block required.
        // Set input and output files via properties (the recommended way).
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.InputFile  = inputPdfPath;
        fileStamp.OutputFile = outputPdfPath;

        // Add the barcode image as a header.
        // The second argument is the top margin (in points) from the top edge of each page.
        // Adjust the margin as needed to position the header correctly.
        fileStamp.AddHeader(barcodeImgPath, topMargin: 20f);

        // Finalize and write the result.
        fileStamp.Close();

        Console.WriteLine($"Header with barcode added. Output saved to '{outputPdfPath}'.");
    }
}