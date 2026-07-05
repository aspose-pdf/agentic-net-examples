using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string zipPath = "input.zip";          // Path to the zip containing PDF files
        const string mergedFileName = "merged.pdf";  // Name of the concatenated PDF inside the zip

        // Verify that the zip archive exists before attempting to open it
        if (!File.Exists(zipPath))
        {
            Console.WriteLine($"Zip file '{zipPath}' not found.");
            return;
        }

        // Open the zip archive for update (read/write)
        using (FileStream zipStream = new FileStream(zipPath, FileMode.Open, FileAccess.ReadWrite))
        using (ZipArchive archive = new ZipArchive(zipStream, ZipArchiveMode.Update))
        {
            // Collect all PDF entries into memory streams
            List<Stream> pdfStreams = new List<Stream>();
            foreach (ZipArchiveEntry entry in archive.Entries)
            {
                if (entry.Name.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
                {
                    MemoryStream ms = new MemoryStream();
                    using (Stream entryStream = entry.Open())
                    {
                        entryStream.CopyTo(ms);
                    }
                    ms.Position = 0;               // Reset for reading
                    pdfStreams.Add(ms);
                }
            }

            if (pdfStreams.Count == 0)
            {
                Console.WriteLine("No PDF files found in the archive.");
                return;
            }

            // Concatenate PDFs using PdfFileEditor
            using (MemoryStream resultStream = new MemoryStream())
            {
                PdfFileEditor editor = new PdfFileEditor();
                editor.CloseConcatenatedStreams = true; // optional: close source streams after operation
                editor.Concatenate(pdfStreams.ToArray(), resultStream);
                resultStream.Position = 0; // Reset before writing back to zip

                // Remove existing merged entry if it exists (GetEntry may return null)
                ZipArchiveEntry? existing = archive.GetEntry(mergedFileName);
                existing?.Delete();

                // Add the concatenated PDF as a new entry
                ZipArchiveEntry mergedEntry = archive.CreateEntry(mergedFileName);
                using (Stream entryStream = mergedEntry.Open())
                {
                    resultStream.CopyTo(entryStream);
                }
            }

            // Dispose source PDF streams
            foreach (Stream s in pdfStreams)
                s.Dispose();
        }

        Console.WriteLine("PDF files concatenated and saved back into the zip archive.");
    }
}
