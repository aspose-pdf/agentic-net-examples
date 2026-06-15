using System;
using System.IO;
using System.IO.Compression;
using Aspose.Pdf.Facades; // PdfFileEditor resides here

class ZipPdfConcatenator
{
    static void Main()
    {
        const string zipPath = "input.zip";          // Path to the zip archive
        const string mergedFileName = "merged.pdf";  // Name for the concatenated PDF inside the zip

        if (!File.Exists(zipPath))
        {
            Console.Error.WriteLine($"Zip file not found: {zipPath}");
            return;
        }

        // Open the zip archive in Update mode so we can read and write entries
        using (FileStream zipFileStream = new FileStream(zipPath, FileMode.Open, FileAccess.ReadWrite))
        using (ZipArchive archive = new ZipArchive(zipFileStream, ZipArchiveMode.Update))
        {
            // Gather all PDF entries
            var pdfEntries = new System.Collections.Generic.List<ZipArchiveEntry>();
            foreach (var entry in archive.Entries)
            {
                if (entry.FullName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
                    pdfEntries.Add(entry);
            }

            if (pdfEntries.Count == 0)
            {
                Console.WriteLine("No PDF files found in the archive.");
                return;
            }

            // Open streams for each PDF entry
            var inputStreams = new System.Collections.Generic.List<Stream>();
            foreach (var entry in pdfEntries)
            {
                // Open the entry stream for reading
                Stream entryStream = entry.Open(); // returns a readable stream
                inputStreams.Add(entryStream);
            }

            // Prepare the output stream that will hold the concatenated PDF
            using (MemoryStream outputStream = new MemoryStream())
            {
                // Use PdfFileEditor to concatenate the PDFs
                PdfFileEditor editor = new PdfFileEditor();
                editor.CloseConcatenatedStreams = true; // close input streams after operation
                editor.Concatenate(inputStreams.ToArray(), outputStream);

                // Ensure the output stream is positioned at the beginning before writing
                outputStream.Position = 0;

                // Remove existing merged entry if it exists
                var existingMerged = archive.GetEntry(mergedFileName);
                existingMerged?.Delete();

                // Create a new entry for the merged PDF
                ZipArchiveEntry mergedEntry = archive.CreateEntry(mergedFileName, CompressionLevel.Optimal);
                using (Stream mergedEntryStream = mergedEntry.Open())
                {
                    outputStream.CopyTo(mergedEntryStream);
                }
            }

            // At this point, input streams have been closed by the editor (CloseConcatenatedStreams = true)
        }

        Console.WriteLine("PDF files concatenated and saved back into the zip archive.");
    }
}