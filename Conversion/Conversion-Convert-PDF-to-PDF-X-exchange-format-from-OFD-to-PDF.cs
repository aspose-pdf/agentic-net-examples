using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;   // Facades namespace is required for some conversion scenarios

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string ofdPath      = "input.ofd";          // Source OFD file
        const string outputPdfX   = "output_pdfx.pdf";    // Resulting PDF/X file
        const string conversionLog = "conversion_log.txt"; // Optional log file for conversion messages

        // Verify source file exists
        if (!File.Exists(ofdPath))
        {
            Console.Error.WriteLine($"Source file not found: {ofdPath}");
            return;
        }

        try
        {
            // Load the OFD document.  No specific OFD load options are required for the
            // current Aspose.Pdf version – passing null uses the default handling.
            using (Document doc = new Document())
            {
                doc.LoadFrom(ofdPath, null);

                // Prepare conversion options for PDF/X‑4 (you can choose PDF_X_3, PDF_X_4, etc.)
                // ConvertErrorAction.Delete removes objects that cannot be converted.
                var conversionOptions = new PdfFormatConversionOptions(
                                            PdfFormat.PDF_X_4,          // Target PDF/X format
                                            ConvertErrorAction.Delete); // Error handling

                // Optional: write conversion messages to a log file
                conversionOptions.LogFileName = conversionLog;

                // Perform the conversion to PDF/X
                doc.Convert(conversionOptions);

                // Save the converted document as a regular PDF file (the content now complies with PDF/X)
                doc.Save(outputPdfX);
            }

            Console.WriteLine($"OFD successfully converted to PDF/X: {outputPdfX}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}