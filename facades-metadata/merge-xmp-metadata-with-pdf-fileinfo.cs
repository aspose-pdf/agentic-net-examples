using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "merged_metadata.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // -----------------------------------------------------------------
            // 1. Access and modify the standard file information (PdfFileInfo)
            // -----------------------------------------------------------------
            // PdfFileInfo works on the same underlying document instance.
            using (PdfFileInfo fileInfo = new PdfFileInfo(doc))
            {
                // Set or update desired metadata fields
                fileInfo.Title   = "Comprehensive Document Title";
                fileInfo.Author  = "John Doe";
                fileInfo.Subject = "Merged XMP and FileInfo Metadata";
                fileInfo.Keywords = "Aspose.Pdf, XMP, Metadata";

                // Optional: add custom key/value pairs
                fileInfo.SetMetaInfo("CustomField", "CustomValue");

                // -------------------------------------------------------------
                // 2. Save the document while preserving existing XMP metadata
                // -------------------------------------------------------------
                // SaveNewInfoWithXmp writes the updated file‑info entries and
                // merges them with any existing XMP packet, leaving untouched
                // XMP data intact.
                bool success = fileInfo.SaveNewInfoWithXmp(outputPdf);
                Console.WriteLine(success
                    ? $"Metadata merged and saved to '{outputPdf}'."
                    : $"Failed to save merged metadata to '{outputPdf}'.");
            }
        }
    }
}