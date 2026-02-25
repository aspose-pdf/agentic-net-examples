using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the source OFD file and the resulting PDF/E‑1 file
        const string inputOfdPath  = "input.ofd";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(inputOfdPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputOfdPath}");
            return;
        }

        try
        {
            // Load the OFD document. Aspose.Pdf can detect the format from the file extension,
            // so no explicit OFDLoadOptions class is required.
            using (Document doc = new Document(inputOfdPath))
            {
                // Convert the document to PDF/E‑1.
                // PdfFormat.PDF_E_1 is the enum value representing the PDF/E‑1 standard.
                // ConvertErrorAction.Delete tells the converter to drop objects it cannot convert.
                var conversionOptions = new PdfFormatConversionOptions(PdfFormat.PDF_E_1, ConvertErrorAction.Delete);
                doc.Convert(conversionOptions);

                // Save the converted document. The resulting file complies with PDF/E‑1.
                doc.Save(outputPdfPath);
            }

            Console.WriteLine($"Conversion completed successfully. Output saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}