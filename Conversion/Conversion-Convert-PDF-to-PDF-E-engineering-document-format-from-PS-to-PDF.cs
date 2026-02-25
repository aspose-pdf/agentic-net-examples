using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace (contains Document, LoadOptions, SaveOptions, etc.

class Program
{
    static void Main()
    {
        const string psInputPath   = "input.ps";          // Source PostScript file
        const string pdfOutputPath = "output.pdf";        // Destination PDF/E file
        const string logPath       = "conversion.log";   // Optional log for conversion details

        if (!File.Exists(psInputPath))
        {
            Console.Error.WriteLine($"Source file not found: {psInputPath}");
            return;
        }

        try
        {
            // Load the PostScript file.  PsLoadOptions is the specific load options class for PS files.
            // (If the class name differs in your version, replace with the appropriate one.)
            PsLoadOptions loadOptions = new PsLoadOptions();

            using (Document doc = new Document())
            {
                // Load the PS content and convert it to an internal PDF representation.
                doc.LoadFrom(psInputPath, loadOptions);

                // Prepare conversion options for the target PDF/E format.
                // Aspose.Pdf does not expose a dedicated PdfFormat value for PDF/E,
                // so we use PDF_X_3 as the closest engineering PDF format.
                PdfFormatConversionOptions convertOptions =
                    new PdfFormatConversionOptions(logPath, PdfFormat.PDF_X_3, ConvertErrorAction.Delete);

                // Perform the conversion.  The method returns true on success.
                bool converted = doc.Convert(convertOptions);
                if (!converted)
                {
                    Console.Error.WriteLine("Conversion reported failure (see log for details).");
                }

                // Save the resulting PDF/E (or PDF_X_3) document.
                // No SaveOptions are required because we are saving to PDF format.
                doc.Save(pdfOutputPath);
            }

            Console.WriteLine($"Conversion completed. Output saved to '{pdfOutputPath}'.");
        }
        catch (PdfException ex)
        {
            Console.Error.WriteLine($"Aspose.Pdf error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}