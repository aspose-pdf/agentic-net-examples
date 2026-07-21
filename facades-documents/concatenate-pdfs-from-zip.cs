using System;
using System.IO;
using System.IO.Compression;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string zipPath = "input.zip";          // Path to the zip containing PDFs
        const string mergedEntryName = "merged.pdf"; // Name of the merged PDF inside the zip

        // Verify the zip file exists
        if (!File.Exists(zipPath))
        {
            Console.Error.WriteLine($"Zip file not found: {zipPath}");
            return;
        }

        // Create temporary files for extracted PDFs and for the merged result
        var tempPdfFiles = new System.Collections.Generic.List<string>();
        string mergedTempFile = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".pdf");

        try
        {
            // Open the zip archive for reading and updating
            using (FileStream zipStream = new FileStream(zipPath, FileMode.Open, FileAccess.ReadWrite))
            using (ZipArchive archive = new ZipArchive(zipStream, ZipArchiveMode.Update))
            {
                // Extract each PDF entry to a temporary file
                foreach (var entry in archive.Entries)
                {
                    // Consider only files with .pdf extension (case-insensitive)
                    if (Path.GetExtension(entry.FullName).Equals(".pdf", StringComparison.OrdinalIgnoreCase))
                    {
                        string tempFile = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".pdf");
                        using (Stream entryStream = entry.Open())
                        using (FileStream tempFs = new FileStream(tempFile, FileMode.Create, FileAccess.Write))
                        {
                            entryStream.CopyTo(tempFs);
                        }
                        tempPdfFiles.Add(tempFile);
                    }
                }

                // Ensure we have at least two PDFs to concatenate
                if (tempPdfFiles.Count < 2)
                {
                    Console.Error.WriteLine("Not enough PDF files in the zip to perform concatenation.");
                    return;
                }

                // Concatenate the extracted PDFs using PdfFileEditor
                PdfFileEditor editor = new PdfFileEditor();
                // Close streams after operation to release file handles
                editor.CloseConcatenatedStreams = true;
                // Use the overload that accepts an array of file paths and an output path
                editor.Concatenate(tempPdfFiles.ToArray(), mergedTempFile);

                // Remove existing merged entry if it already exists
                var existingEntry = archive.GetEntry(mergedEntryName);
                existingEntry?.Delete();

                // Add the merged PDF back into the zip archive
                var mergedEntry = archive.CreateEntry(mergedEntryName, CompressionLevel.Optimal);
                using (Stream mergedEntryStream = mergedEntry.Open())
                using (FileStream mergedFileStream = new FileStream(mergedTempFile, FileMode.Open, FileAccess.Read))
                {
                    mergedFileStream.CopyTo(mergedEntryStream);
                }
            }

            Console.WriteLine($"Merged PDF added to zip as '{mergedEntryName}'.");
        }
        finally
        {
            // Clean up temporary files
            foreach (var tempFile in tempPdfFiles)
            {
                try { File.Delete(tempFile); } catch { /* ignore */ }
            }
            try { File.Delete(mergedTempFile); } catch { /* ignore */ }
        }
    }
}