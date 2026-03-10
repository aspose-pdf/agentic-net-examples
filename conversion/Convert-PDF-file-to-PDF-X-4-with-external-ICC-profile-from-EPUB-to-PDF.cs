using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input EPUB file, output PDF/X-4 file and external ICC profile path
        const string epubPath = "input.epub";
        const string outputPdfPath = "output_pdfx4.pdf";
        const string iccProfilePath = "sRGB.icc";

        // Verify that source files exist
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

        // Load the EPUB document (EPUB -> PDF) using EpubLoadOptions
        using (Document doc = new Document(epubPath, new EpubLoadOptions()))
        {
            // Prepare conversion options for PDF/X-4
            PdfFormatConversionOptions convertOptions = new PdfFormatConversionOptions(PdfFormat.PDF_X_4);
            // Assign external ICC profile (used as OutputIntent)
            convertOptions.IccProfileFileName = iccProfilePath;

            // Convert the in‑memory PDF to PDF/X-4 using the specified ICC profile
            doc.Convert(convertOptions);

            // Save the resulting PDF/X-4 document
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"EPUB successfully converted to PDF/X-4: {outputPdfPath}");
    }
}