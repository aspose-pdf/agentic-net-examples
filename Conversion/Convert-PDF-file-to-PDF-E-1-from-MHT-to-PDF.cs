using System;
using System.IO;
using Aspose.Pdf; // Core PDF API (Document, PdfFormat, ConvertErrorAction, etc.)

class Program
{
    static void Main()
    {
        // Paths – adjust as needed.
        const string dataDir   = @"YOUR_DATA_DIRECTORY";
        string mhtPath         = Path.Combine(dataDir, "input.mht");   // source MHT file
        string pdfPath         = Path.Combine(dataDir, "output.pdf");  // target PDF/E‑1 file
        string conversionLog   = Path.Combine(dataDir, "conversion.log"); // optional log

        // Validate input.
        if (!File.Exists(mhtPath))
        {
            Console.Error.WriteLine($"MHT file not found: {mhtPath}");
            return;
        }

        try
        {
            // Load the MHT file into a PDF document using MhtLoadOptions.
            MhtLoadOptions loadOptions = new MhtLoadOptions(); // default options
            using (Document doc = new Document(mhtPath, loadOptions))
            {
                // Convert the document to PDF/E‑1 format.
                // ConvertErrorAction.Delete – skip objects that cannot be converted.
                doc.Convert(conversionLog, PdfFormat.PDF_E_1, ConvertErrorAction.Delete);

                // Save the resulting PDF/E‑1 document.
                doc.Save(pdfPath);
            }

            Console.WriteLine($"MHT successfully converted to PDF/E‑1: {pdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
