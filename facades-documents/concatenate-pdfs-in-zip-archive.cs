using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class ZipPdfConcatenator
{
    static void Main()
    {
        const string zipPath = "input.zip";          // Path to the source zip archive
        const string mergedFileName = "merged.pdf";  // Name for the concatenated PDF inside the zip

        // Verify that the zip archive exists before trying to open it
        if (!File.Exists(zipPath))
        {
            Console.WriteLine($"Zip file '{zipPath}' not found. Operation aborted.");
            return;
        }

        // Open the zip archive in Update mode so we can read and modify its entries
        using (FileStream zipFileStream = new FileStream(zipPath, FileMode.Open, FileAccess.ReadWrite))
        using (ZipArchive archive = new ZipArchive(zipFileStream, ZipArchiveMode.Update, leaveOpen: true))
        {
            // Collect all PDF entries from the archive
            List<MemoryStream> pdfStreams = new List<MemoryStream>();
            foreach (ZipArchiveEntry entry in archive.Entries)
            {
                if (entry.FullName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
                {
                    // Copy the entry content into a memory stream (PdfFileEditor works with seekable streams)
                    using (Stream entryStream = entry.Open())
                    {
                        MemoryStream ms = new MemoryStream();
                        entryStream.CopyTo(ms);
                        ms.Position = 0;               // Reset position for reading
                        pdfStreams.Add(ms);
                    }
                }
            }

            if (pdfStreams.Count == 0)
            {
                Console.WriteLine("No PDF files found in the zip archive.");
                return;
            }

            // Prepare the output stream that will hold the concatenated PDF
            using (MemoryStream resultStream = new MemoryStream())
            {
                // Use PdfFileEditor to concatenate the PDFs
                PdfFileEditor editor = new PdfFileEditor
                {
                    // Close the source streams automatically after concatenation
                    CloseConcatenatedStreams = true
                };

                // Concatenate all PDF streams into the result stream
                editor.Concatenate(pdfStreams.ToArray(), resultStream);
                resultStream.Position = 0; // Ensure the stream is ready for reading

                // Remove any existing entry with the same name (if present)
                ZipArchiveEntry? existing = archive.GetEntry(mergedFileName);
                if (existing != null)
                {
                    existing.Delete();
                }

                // Create a new entry for the merged PDF and write the result
                ZipArchiveEntry mergedEntry = archive.CreateEntry(mergedFileName, CompressionLevel.Optimal);
                using (Stream mergedEntryStream = mergedEntry.Open())
                {
                    resultStream.CopyTo(mergedEntryStream);
                }
            }

            // Dispose all temporary PDF streams (they were not closed automatically because we set CloseConcatenatedStreams = true)
            foreach (MemoryStream ms in pdfStreams)
            {
                ms.Dispose();
            }
        }

        Console.WriteLine("PDF files concatenated and saved back into the zip archive.");
    }
}
