using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_pdfx3.pdf";
        const string iccProfilePath = "CMYK.icc"; // Path to a CMYK ICC profile

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        if (!File.Exists(iccProfilePath))
        {
            Console.Error.WriteLine($"ICC profile not found: {iccProfilePath}");
            return;
        }

        try
        {
            // Load the source PDF inside a using block for deterministic disposal.
            using (Document doc = new Document(inputPath))
            {
                // Prepare conversion options for PDF/X‑3 compliance.
                PdfFormatConversionOptions convOptions = new PdfFormatConversionOptions(PdfFormat.PDF_X_3);

                // Force all colors to CMYK by specifying an ICC profile.
                convOptions.IccProfileFileName = iccProfilePath;
                convOptions.OutputIntent = new OutputIntent(iccProfilePath); // Embed the profile.

                // Convert the document to PDF/X‑3 using the options above.
                doc.Convert(convOptions);

                // Save the converted document as a regular PDF file (it now conforms to PDF/X‑3).
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF/X‑3 compliant file saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}