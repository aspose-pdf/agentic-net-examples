using System;
using System.IO;
using Aspose.Pdf;               // Core API (Document, PdfFormatConversionOptions, OutputIntent, etc.)
using Aspose.Pdf.Facades;      // Included as per task requirement (not directly used here)

class Program
{
    static void Main()
    {
        // Input PDF (already converted from OFD, if needed)
        const string inputPdfPath = "input.pdf";

        // Desired PDF/X‑4 output file
        const string outputPdfx4Path = "output_pdfx4.pdf";

        // External ICC profile to be embedded (must exist on disk)
        const string iccProfilePath = "sRGB.icc";

        // Validate file existence
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(iccProfilePath))
        {
            Console.Error.WriteLine($"ICC profile not found: {iccProfilePath}");
            return;
        }

        // Load the source PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Configure conversion options for PDF/X‑4
            PdfFormatConversionOptions conversionOptions =
                new PdfFormatConversionOptions(PdfFormat.PDF_X_4, ConvertErrorAction.Delete);

            // Attach the external ICC profile
            conversionOptions.IccProfileFileName = iccProfilePath;

            // Define an OutputIntent identifier (any unique string works)
            conversionOptions.OutputIntent = new OutputIntent("CustomICC");

            // Perform the format conversion
            pdfDoc.Convert(conversionOptions);

            // Save the resulting PDF/X‑4 document
            pdfDoc.Save(outputPdfx4Path);
        }

        Console.WriteLine($"PDF/X‑4 file successfully saved to '{outputPdfx4Path}'.");
    }
}