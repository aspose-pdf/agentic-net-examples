using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string zipPath = "input.zip";          // Path to the zip archive
        const string mergedFileName = "merged.pdf";  // Name of the merged PDF inside the zip

        if (!File.Exists(zipPath))
        {
            Console.Error.WriteLine($"Zip file not found: {zipPath}");
            return;
        }

        // Open the zip archive for update (read/write)
        using (FileStream zipStream = new FileStream(zipPath, FileMode.Open, FileAccess.ReadWrite))
        using (ZipArchive archive = new ZipArchive(zipStream, ZipArchiveMode.Update))
        {
            // Collect streams of all PDF entries in the archive
            List<Stream> pdfStreams = new List<Stream>();
            foreach (ZipArchiveEntry entry in archive.Entries)
            {
                if (entry.FullName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
                {
                    // Open the entry stream for reading and keep it until concatenation finishes
                    Stream entryStream = entry.Open();
                    pdfStreams.Add(entryStream);
                }
            }

            if (pdfStreams.Count == 0)
            {
                Console.WriteLine("No PDF files found in the archive.");
                return;
            }

            // Prepare the output stream that will hold the concatenated PDF
            using (MemoryStream mergedStream = new MemoryStream())
            {
                // Use PdfFileEditor to concatenate the PDF streams
                PdfFileEditor editor = new PdfFileEditor();
                bool success = editor.Concatenate(pdfStreams.ToArray(), mergedStream);
                if (!success)
                {
                    Console.Error.WriteLine("Failed to concatenate PDF files.");
                    // Dispose the input streams before exiting
                    foreach (var s in pdfStreams) s.Dispose();
                    return;
                }

                // Reset position to the beginning before writing to the zip entry
                mergedStream.Position = 0;

                // Remove existing merged entry if it exists
                ZipArchiveEntry? existing = archive.GetEntry(mergedFileName);
                existing?.Delete();

                // Create a new entry for the merged PDF and write the data
                ZipArchiveEntry mergedEntry = archive.CreateEntry(mergedFileName);
                using (Stream entryOut = mergedEntry.Open())
                {
                    mergedStream.CopyTo(entryOut);
                }
            }

            // Dispose all input PDF streams now that they are no longer needed
            foreach (var s in pdfStreams)
            {
                s.Dispose();
            }
        }

        Console.WriteLine("PDF files concatenated and saved back into the zip archive.");
    }
}
