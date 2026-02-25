using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF (generated from TeX) and output PDF/X‑4 file paths
        const string inputPdfPath  = "input_from_tex.pdf";
        const string outputPdfPath = "output_pdfx4.pdf";
        // Path to the external ICC profile to be used as OutputIntent
        const string iccProfilePath = "sRGB.icc";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(iccProfilePath))
        {
            Console.Error.WriteLine($"ICC profile not found: {iccProfilePath}");
            return;
        }

        // Load the source PDF and convert it to PDF/X‑4 using the ICC profile
        using (Document doc = new Document(inputPdfPath))
        {
            // Create conversion options for PDF/X‑4
            PdfFormatConversionOptions options = new PdfFormatConversionOptions(PdfFormat.PDF_X_4)
            {
                // Specify the external ICC profile file
                IccProfileFileName = iccProfilePath,
                // Optional: keep original content unchanged
                OptimizeFileSize = false,
                // Define how conversion errors are handled
                ErrorAction = ConvertErrorAction.Delete
            };

            // Perform the conversion; returns true on success
            bool success = doc.Convert(options);
            if (!success)
            {
                Console.Error.WriteLine("Conversion failed – see log for details.");
                return;
            }

            // Save the converted document as PDF/X‑4
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF/X‑4 file saved to '{outputPdfPath}'.");
    }
}