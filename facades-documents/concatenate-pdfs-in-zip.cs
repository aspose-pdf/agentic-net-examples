using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string zipPath = "input.zip";               // source zip containing PDFs
        const string outputZipPath = "output.zip";        // zip to write result
        const string mergedPdfName = "merged.pdf";        // name of concatenated PDF inside zip

        if (!File.Exists(zipPath))
        {
            Console.Error.WriteLine($"Zip not found: {zipPath}");
            return;
        }

        // Temporary folder for extracted PDFs and the merged result
        string tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(tempDir);
        var pdfFiles = new List<string>();

        try
        {
            // Extract all PDF entries from the source zip to temporary files
            using (FileStream zipStream = new FileStream(zipPath, FileMode.Open, FileAccess.Read))
            using (ZipArchive archive = new ZipArchive(zipStream, ZipArchiveMode.Read))
            {
                foreach (var entry in archive.Entries)
                {
                    if (Path.GetExtension(entry.FullName).Equals(".pdf", StringComparison.OrdinalIgnoreCase))
                    {
                        string tempPdfPath = Path.Combine(tempDir, entry.Name);
                        using (Stream entryStream = entry.Open())
                        using (FileStream tempFile = new FileStream(tempPdfPath, FileMode.Create, FileAccess.Write))
                        {
                            entryStream.CopyTo(tempFile);
                        }
                        pdfFiles.Add(tempPdfPath);
                    }
                }
            }

            if (pdfFiles.Count == 0)
            {
                Console.Error.WriteLine("No PDF files found in the zip archive.");
                return;
            }

            // Concatenate PDFs using PdfFileEditor (no using – it does not implement IDisposable)
            PdfFileEditor editor = new PdfFileEditor();
            string mergedTempPath = Path.Combine(tempDir, mergedPdfName);
            editor.Concatenate(pdfFiles.ToArray(), mergedTempPath);

            // Create the output zip and copy original non-PDF entries, then add the merged PDF
            using (FileStream outZipStream = new FileStream(outputZipPath, FileMode.Create))
            using (ZipArchive outArchive = new ZipArchive(outZipStream, ZipArchiveMode.Update))
            {
                // Copy all original entries except the PDFs (to avoid duplication)
                using (FileStream inZipStream = new FileStream(zipPath, FileMode.Open, FileAccess.Read))
                using (ZipArchive inArchive = new ZipArchive(inZipStream, ZipArchiveMode.Read))
                {
                    foreach (var entry in inArchive.Entries)
                    {
                        if (Path.GetExtension(entry.FullName).Equals(".pdf", StringComparison.OrdinalIgnoreCase))
                            continue; // skip original PDFs

                        var newEntry = outArchive.CreateEntry(entry.FullName, CompressionLevel.Optimal);
                        using (Stream src = entry.Open())
                        using (Stream dst = newEntry.Open())
                        {
                            src.CopyTo(dst);
                        }
                    }
                }

                // Add the concatenated PDF as a new entry
                var mergedEntry = outArchive.CreateEntry(mergedPdfName, CompressionLevel.Optimal);
                using (Stream mergedEntryStream = mergedEntry.Open())
                using (FileStream mergedFile = new FileStream(mergedTempPath, FileMode.Open, FileAccess.Read))
                {
                    mergedFile.CopyTo(mergedEntryStream);
                }
            }

            Console.WriteLine($"Successfully created '{outputZipPath}' with merged PDF '{mergedPdfName}'.");
        }
        finally
        {
            // Clean up temporary files and directory
            foreach (var file in pdfFiles)
            {
                try { File.Delete(file); } catch { }
            }
            try { File.Delete(Path.Combine(tempDir, mergedPdfName)); } catch { }
            try { Directory.Delete(tempDir, true); } catch { }
        }
    }
}