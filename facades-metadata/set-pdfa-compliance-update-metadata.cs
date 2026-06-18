using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_pdfa.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF
        using (Document doc = new Document(inputPath))
        {
            // Convert the document to PDF/A‑1B (archival) format.
            // The conversion log is optional; it can be omitted if not needed.
            doc.Convert("conversion_log.xml", PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);

            // Verify that the conversion succeeded.
            Console.WriteLine($"Is PDF/A compliant after conversion: {doc.IsPdfaCompliant}");

            // Save the converted PDF to a temporary file.
            // This file is already PDF/A compliant.
            string tempPath = Path.GetTempFileName();
            doc.Save(tempPath);

            // Use the PdfFileInfo facade to set/modify document metadata.
            // PdfFileInfo works directly on the file, not on the Document object.
            using (PdfFileInfo info = new PdfFileInfo(tempPath))
            {
                info.Title    = "Archived PDF/A Document";
                info.Author   = "Automated System";
                info.Subject  = "PDF/A compliance example";
                info.Keywords = "PDF/A, archival, Aspose.Pdf";

                // Save the updated file (metadata only) to the final output path.
                // The PDF/A compliance flag remains intact because we are only
                // rewriting the document's metadata.
                info.Save(outputPath);
            }

            // Clean up the temporary file.
            File.Delete(tempPath);
        }

        Console.WriteLine($"PDF/A compliant document saved to '{outputPath}'.");
    }
}