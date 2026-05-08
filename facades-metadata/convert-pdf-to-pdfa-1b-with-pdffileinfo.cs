using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_pdfa.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF, convert it to PDF/A‑1b and then persist via PdfFileInfo.
        using (Document doc = new Document(inputPath))
        {
            // Convert the document to PDF/A‑1b. This sets the internal compliance flag.
            doc.Convert("conversion_log.xml", PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);

            // Optional: display the compliance status after conversion.
            Console.WriteLine($"Is PDF/A compliant after conversion: {doc.IsPdfaCompliant}");

            // Use the PdfFileInfo facade to save the updated document.
            PdfFileInfo fileInfo = new PdfFileInfo(doc);
            // Enforce strict validation (helps ensure PDF/A rules are respected).
            fileInfo.UseStrictValidation = true;
            // Save the PDF/A‑compliant file.
            fileInfo.SaveNewInfo(outputPath);
        }

        Console.WriteLine($"PDF/A‑compliant file saved to '{outputPath}'.");
    }
}