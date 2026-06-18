using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePdfPath = "source.pdf";   // PDF to copy XMP metadata from
        const string targetPdfPath = "target.pdf";   // PDF that will receive the metadata
        const string outputPdfPath = "merged.pdf";   // Final merged PDF

        // Validate input files
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdfPath}");
            return;
        }
        if (!File.Exists(targetPdfPath))
        {
            Console.Error.WriteLine($"Target file not found: {targetPdfPath}");
            return;
        }

        try
        {
            // ---------- Extract XMP metadata from the source PDF ----------
            byte[] xmpData;
            using (PdfXmpMetadata xmp = new PdfXmpMetadata())
            {
                xmp.BindPdf(sourcePdfPath);
                xmpData = xmp.GetXmpMetadata(); // returns byte[]
            }

            // ---------- Apply the extracted XMP metadata to the target PDF ----------
            using (Document targetDoc = new Document(targetPdfPath))
            {
                using (MemoryStream ms = new MemoryStream(xmpData))
                {
                    targetDoc.SetXmpMetadata(ms);
                }

                // Save the modified target to a temporary file (required for Facades concatenation)
                string tempTargetPath = Path.Combine(Path.GetTempPath(),
                    $"temp_target_{Guid.NewGuid():N}.pdf");
                targetDoc.Save(tempTargetPath);

                // ---------- Merge the modified target with the source PDF ----------
                PdfFileEditor editor = new PdfFileEditor();
                bool success = editor.Concatenate(tempTargetPath, sourcePdfPath, outputPdfPath);
                if (!success)
                {
                    Console.Error.WriteLine("Merging failed.");
                }

                // Clean up temporary file
                if (File.Exists(tempTargetPath))
                {
                    File.Delete(tempTargetPath);
                }
            }

            Console.WriteLine($"Merged PDF saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}