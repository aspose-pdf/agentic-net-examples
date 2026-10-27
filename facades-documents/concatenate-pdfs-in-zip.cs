using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string zipPath = "input.zip";

        if (!File.Exists(zipPath))
        {
            Console.Error.WriteLine($"Zip file not found: {zipPath}");
            return;
        }

        // Lists to hold temporary PDF file paths
        List<string> tempPdfFiles = new List<string>();
        string mergedTempFile = Path.GetTempFileName();

        try
        {
            // Open the zip archive for update
            using (FileStream zipStream = new FileStream(zipPath, FileMode.Open, FileAccess.ReadWrite))
            using (ZipArchive archive = new ZipArchive(zipStream, ZipArchiveMode.Update))
            {
                // Extract each PDF entry to a temporary file
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    if (!entry.FullName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
                        continue;

                    string tempPath = Path.GetTempFileName();
                    using (Stream entryStream = entry.Open())
                    using (FileStream tempFile = new FileStream(tempPath, FileMode.Create, FileAccess.Write))
                    {
                        entryStream.CopyTo(tempFile);
                    }
                    tempPdfFiles.Add(tempPath);
                }

                if (tempPdfFiles.Count == 0)
                {
                    Console.WriteLine("No PDF files found in the archive.");
                    return;
                }

                // Concatenate PDFs using PdfFileEditor (no using needed)
                PdfFileEditor editor = new PdfFileEditor();
                editor.Concatenate(tempPdfFiles.ToArray(), mergedTempFile);

                // Replace or add the merged PDF entry in the archive
                ZipArchiveEntry existing = archive.GetEntry("merged.pdf");
                existing?.Delete();

                ZipArchiveEntry mergedEntry = archive.CreateEntry("merged.pdf");
                using (Stream entryStream = mergedEntry.Open())
                using (FileStream mergedFile = new FileStream(mergedTempFile, FileMode.Open, FileAccess.Read))
                {
                    mergedFile.CopyTo(entryStream);
                }
            }

            Console.WriteLine("PDF files concatenated and saved back into the zip archive as 'merged.pdf'.");
        }
        finally
        {
            // Clean up temporary files
            foreach (string path in tempPdfFiles)
            {
                try { File.Delete(path); } catch { /* ignore */ }
            }
            try { File.Delete(mergedTempFile); } catch { /* ignore */ }
        }
    }
}