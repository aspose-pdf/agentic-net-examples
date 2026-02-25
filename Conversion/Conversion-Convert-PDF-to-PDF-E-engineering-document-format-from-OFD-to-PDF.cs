using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;   // Facade APIs are required by the task

class Program
{
    static void Main()
    {
        const string inputOfdPath   = "input.ofd";          // source OFD file
        const string outputPdfEPath = "output_pdfe.pdf";    // target PDF/E file
        const string conversionLog  = "conversion.log";    // optional log file

        if (!File.Exists(inputOfdPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputOfdPath}");
            return;
        }

        try
        {
            // Load the OFD document.  Use the concrete load options class for OFD.
            // LoadOptions is abstract – we must instantiate a concrete subclass.
            var ofdLoadOptions = new OfdLoadOptions();   // specific to OFD format
            using (var doc = new Document())
            {
                doc.LoadFrom(inputOfdPath, ofdLoadOptions);

                // Prepare conversion options for PDF/E (engineering PDF).
                // PdfFormat includes PDF/E variants (e.g., PDF_E_1). Adjust if a different enum value is required.
                var convertOptions = new PdfFormatConversionOptions(PdfFormat.PDF_E_1, ConvertErrorAction.Delete)
                {
                    OptimizeFileSize = true,
                    LogFileName      = conversionLog   // optional: store conversion messages
                };

                // Perform the format conversion.
                doc.Convert(convertOptions);

                // Save the resulting PDF/E document.
                doc.Save(outputPdfEPath);
            }

            Console.WriteLine($"Conversion completed. PDF/E saved to '{outputPdfEPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}