using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string zipPath = "input.zip";
        const string mergedEntryName = "merged.pdf";

        // Ensure the ZIP archive exists – create an empty one if it does not.
        if (!File.Exists(zipPath))
        {
            using (FileStream createStream = new FileStream(zipPath, FileMode.Create))
            using (ZipArchive createArchive = new ZipArchive(createStream, ZipArchiveMode.Create))
            {
                // No initial entries are required.
            }
        }

        // Open the ZIP archive for update.
        using (FileStream zipFileStream = new FileStream(zipPath, FileMode.Open, FileAccess.ReadWrite))
        using (ZipArchive zipArchive = new ZipArchive(zipFileStream, ZipArchiveMode.Update))
        {
            // Collect PDF streams from the archive.
            List<MemoryStream> pdfMemoryStreams = new List<MemoryStream>();
            foreach (ZipArchiveEntry entry in zipArchive.Entries)
            {
                if (entry.Name.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
                {
                    using (Stream entryStream = entry.Open())
                    {
                        MemoryStream tempStream = new MemoryStream();
                        entryStream.CopyTo(tempStream);
                        tempStream.Position = 0;
                        pdfMemoryStreams.Add(tempStream);
                    }
                }
            }

            if (pdfMemoryStreams.Count == 0)
            {
                Console.WriteLine("No PDF files found in the ZIP archive.");
                // Clean up the temporary streams before exiting.
                foreach (MemoryStream ms in pdfMemoryStreams) ms.Dispose();
                return;
            }

            // Concatenate PDFs.
            using (MemoryStream outputStream = new MemoryStream())
            {
                PdfFileEditor pdfEditor = new PdfFileEditor();
                // The Concatenate overload that accepts streams is used.
                pdfEditor.Concatenate(pdfMemoryStreams.ToArray(), outputStream);
                outputStream.Position = 0;

                // Remove existing merged entry if it exists.
                ZipArchiveEntry existingEntry = zipArchive.GetEntry(mergedEntryName);
                existingEntry?.Delete();

                // Create new entry for merged PDF.
                ZipArchiveEntry mergedEntry = zipArchive.CreateEntry(mergedEntryName);
                using (Stream mergedStream = mergedEntry.Open())
                {
                    outputStream.CopyTo(mergedStream);
                }
            }

            // Dispose temporary PDF streams.
            foreach (MemoryStream ms in pdfMemoryStreams)
            {
                ms.Dispose();
            }
        }

        Console.WriteLine("PDF files concatenated and saved back into the ZIP archive.");
    }
}
