using System;
using System.IO;

class Program
{
    static void Main()
    {
        const string cgmPath = "input.cgm";
        const string outputPdf = "output.pdf";
        const string logPath = "conversion_log.xml";

        if (!File.Exists(cgmPath))
        {
            Console.Error.WriteLine($"File not found: {cgmPath}");
            return;
        }

        try
        {
            // Load the CGM file (CGM is input‑only, so we load it and then convert)
            Aspose.Pdf.CgmLoadOptions loadOptions = new Aspose.Pdf.CgmLoadOptions();
            using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(cgmPath, loadOptions))
            {
                // Convert to PDF/E (implemented as PDF/A‑4E in Aspose.Pdf)
                Aspose.Pdf.PdfFormatConversionOptions convOptions =
                    new Aspose.Pdf.PdfFormatConversionOptions(
                        Aspose.Pdf.PdfFormat.PDF_A_4E,
                        Aspose.Pdf.ConvertErrorAction.Delete);

                // Optional: write conversion log
                convOptions.LogFileName = logPath;

                // Perform the conversion
                doc.Convert(convOptions);

                // Save the resulting PDF/E document
                doc.Save(outputPdf);
            }

            Console.WriteLine($"Converted CGM to PDF/E: {outputPdf}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}