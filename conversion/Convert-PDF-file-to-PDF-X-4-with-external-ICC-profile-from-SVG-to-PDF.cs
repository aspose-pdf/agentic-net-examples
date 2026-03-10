using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string svgPath = "input.svg";
        const string iccProfilePath = "profile.icc";
        const string outputPdfPath = "output_pdfx4.pdf";

        // Verify input files exist
        if (!File.Exists(svgPath))
        {
            Console.Error.WriteLine($"SVG file not found: {svgPath}");
            return;
        }
        if (!File.Exists(iccProfilePath))
        {
            Console.Error.WriteLine($"ICC profile not found: {iccProfilePath}");
            return;
        }

        try
        {
            // Load the SVG file and create an intermediate PDF document
            using (Document pdfDoc = new Document(svgPath, new SvgLoadOptions()))
            {
                // Configure conversion to PDF/X-4 with an external ICC profile
                PdfFormatConversionOptions convOptions = new PdfFormatConversionOptions(PdfFormat.PDF_X_4)
                {
                    IccProfileFileName = iccProfilePath,
                    // Optional: define how conversion errors are handled
                    // ErrorAction = ConvertErrorAction.Delete
                };

                // Perform the conversion to PDF/X-4
                pdfDoc.Convert(convOptions);

                // Save the final PDF/X-4 document
                pdfDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"SVG successfully converted to PDF/X-4: {outputPdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}