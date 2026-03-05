using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // not required but harmless

class Program
{
    static void Main()
    {
        const string cgmPath      = "input.cgm";          // source CGM file
        const string intermediate = "intermediate.pdf";    // PDF generated from CGM
        const string outputPdfX4  = "output_pdfx4.pdf";    // final PDF/X-4 file
        const string logPath      = "conversion.log";     // conversion log

        // Verify source file exists
        if (!File.Exists(cgmPath))
        {
            Console.Error.WriteLine($"Source CGM not found: {cgmPath}");
            return;
        }

        // STEP 1: Load CGM and save as a regular PDF
        using (Document pdfFromCgm = new Document(cgmPath, new CgmLoadOptions()))
        {
            // Save the intermediate PDF (default PDF/A is not applied here)
            pdfFromCgm.Save(intermediate);
        }

        // STEP 2: Load the intermediate PDF and convert to PDF/X-4
        using (Document doc = new Document(intermediate))
        {
            // Convert to PDF/X-4, logging any conversion errors
            // ConvertErrorAction.Delete – objects that cannot be converted are removed
            doc.Convert(logPath, PdfFormat.PDF_X_4, ConvertErrorAction.Delete);

            // Save the final PDF/X-4 compliant document
            doc.Save(outputPdfX4);
        }

        Console.WriteLine($"Conversion completed. PDF/X-4 saved to '{outputPdfX4}'.");
    }
}