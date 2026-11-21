using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Only needed for PdfFileInfo if used elsewhere

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_pdfa.pdf";

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (full load is required for conversion to PDF/A)
        Document pdfDoc = new Document(inputPath);

        // Convert the document to PDF/A‑1b. The PdfFormat enum is always present in Aspose.Pdf.
        // ConvertErrorAction.Delete removes pages that cannot be converted.
        pdfDoc.Convert(outputPath, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);

        Console.WriteLine($"PDF/A‑1b compliant file saved to '{outputPath}'.");
    }
}
