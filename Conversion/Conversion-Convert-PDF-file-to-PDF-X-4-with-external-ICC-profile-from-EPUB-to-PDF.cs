using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input EPUB file, external ICC profile and output PDF/X‑4 file paths
        const string epubPath      = "input.epub";
        const string iccProfilePath = "profile.icc";
        const string outputPdfPath = "output_pdfx4.pdf";

        // Simple validation
        if (!File.Exists(epubPath))
        {
            Console.Error.WriteLine($"EPUB file not found: {epubPath}");
            return;
        }
        if (!File.Exists(iccProfilePath))
        {
            Console.Error.WriteLine($"ICC profile not found: {iccProfilePath}");
            return;
        }

        // Load the EPUB document – this creates an in‑memory PDF representation
        using (Document doc = new Document(epubPath, new EpubLoadOptions()))
        {
            // Prepare conversion options for PDF/X‑4
            PdfFormatConversionOptions convOptions = new PdfFormatConversionOptions(PdfFormat.PDF_X_4)
            {
                // Path to the external ICC profile to be embedded as OutputIntent
                IccProfileFileName = iccProfilePath,

                // Optional: store conversion log (helps diagnosing issues)
                LogFileName = Path.Combine(Path.GetDirectoryName(outputPdfPath) ?? "", "conversion_log.xml")
            };

            // Perform the conversion to PDF/X‑4 using the specified options
            doc.Convert(convOptions);

            // Save the resulting PDF/X‑4 document
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"EPUB successfully converted to PDF/X‑4: {outputPdfPath}");
    }
}