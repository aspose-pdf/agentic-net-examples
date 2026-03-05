using System;
using System.IO;
using Aspose.Pdf;                     // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        // Directory that contains the source XPS file.
        const string dataDir = "YOUR_DATA_DIRECTORY";

        // Input XPS file path.
        string xpsPath = Path.Combine(dataDir, "input.xps");

        // Desired output PDF/E-1 file path.
        string pdfPath = Path.Combine(dataDir, "output.pdf");

        // Verify that the source file exists.
        if (!File.Exists(xpsPath))
        {
            Console.Error.WriteLine($"XPS file not found: {xpsPath}");
            return;
        }

        // Load the XPS document using XpsLoadOptions.
        XpsLoadOptions loadOptions = new XpsLoadOptions();

        // Wrap the Document in a using block for deterministic disposal.
        using (Document doc = new Document(xpsPath, loadOptions))
        {
            // Convert the loaded document to PDF/E-1 format.
            // PdfFormat.PDF_E_1 represents the PDF/E‑1 compliance option.
            PdfFormatConversionOptions conversionOptions = new PdfFormatConversionOptions(PdfFormat.PDF_E_1);
            doc.Convert(conversionOptions);

            // Save the converted document as a PDF file (now PDF/E‑1 compliant).
            doc.Save(pdfPath);
        }

        Console.WriteLine($"Conversion completed. PDF/E-1 saved to: {pdfPath}");
    }
}