using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // for ConvertErrorAction enum

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string dataDir   = @"YOUR_DATA_DIRECTORY";
        string texFile          = Path.Combine(dataDir, "input.tex");
        string pdfEFile         = Path.Combine(dataDir, "output_pdfe.pdf");
        string conversionLog    = Path.Combine(dataDir, "conversion_log.txt");

        // Verify source file exists
        if (!File.Exists(texFile))
        {
            Console.Error.WriteLine($"TeX source not found: {texFile}");
            return;
        }

        // Load TeX file with default options
        TeXLoadOptions texLoadOptions = new TeXLoadOptions();

        // Load the TeX document – this creates a PDF in memory
        using (Document pdfDoc = new Document(texFile, texLoadOptions))
        {
            // Convert the in‑memory PDF to PDF/E format.
            // PdfFormat.PDF_E_1 represents the PDF/E‑1 engineering format.
            // ConvertErrorAction.Delete tells the engine to skip objects it cannot convert.
            pdfDoc.Convert(conversionLog, PdfFormat.PDF_E_1, ConvertErrorAction.Delete);

            // Save the resulting PDF/E document.
            pdfDoc.Save(pdfEFile);
        }

        Console.WriteLine($"TeX converted to PDF/E successfully: {pdfEFile}");
    }
}