using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePdfPath = "source.pdf";          // PDF to copy XMP from
        const string targetPdfPath = "target.pdf";          // PDF to receive XMP
        const string tempPdfPath   = "target_with_xmp.pdf"; // intermediate file
        const string outputPdfPath = "merged_output.pdf";   // final merged PDF

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
            // ---------- Extract XMP metadata from source PDF ----------
            byte[] xmpData;
            using (PdfXmpMetadata xmp = new PdfXmpMetadata())
            {
                xmp.BindPdf(sourcePdfPath);
                xmpData = xmp.GetXmpMetadata(); // returns raw XML bytes
            }

            // ---------- Apply XMP metadata to target PDF ----------
            using (Document targetDoc = new Document(targetPdfPath))
            using (MemoryStream xmpStream = new MemoryStream(xmpData))
            {
                // SetXmpMetadata expects a stream containing the XML metadata
                targetDoc.SetXmpMetadata(xmpStream);
                // Save to a temporary file that now carries the source XMP metadata
                targetDoc.Save(tempPdfPath);
            }

            // ---------- Merge pages (source + target with XMP) ----------
            // PdfFileEditor does NOT implement IDisposable, so do NOT use a using block.
            var editor = new PdfFileEditor();
            editor.Concatenate(new string[] { sourcePdfPath, tempPdfPath }, outputPdfPath);

            Console.WriteLine($"Merged PDF created at '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            // Clean up the intermediate file
            if (File.Exists(tempPdfPath))
            {
                try { File.Delete(tempPdfPath); } catch { /* ignore */ }
            }
        }
    }
}
