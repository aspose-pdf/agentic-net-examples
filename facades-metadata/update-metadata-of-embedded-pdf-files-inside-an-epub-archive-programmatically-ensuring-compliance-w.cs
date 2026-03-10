using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using Aspose.Pdf.Facades; // Facade classes for PDF metadata handling

class Program
{
    static void Main()
    {
        const string inputEpubPath  = "input.epub";
        const string outputEpubPath = "output.epub";

        // Verify that the source EPUB exists before attempting any work.
        if (!File.Exists(inputEpubPath))
        {
            Console.Error.WriteLine($"Error: Input EPUB file '{inputEpubPath}' was not found.");
            return;
        }

        // Ensure the output directory exists.
        string outputDirectory = Path.GetDirectoryName(Path.GetFullPath(outputEpubPath));
        if (!string.IsNullOrEmpty(outputDirectory) && !Directory.Exists(outputDirectory))
        {
            Directory.CreateDirectory(outputDirectory);
        }

        // Make a fresh copy of the input EPUB to work on.
        File.Copy(inputEpubPath, outputEpubPath, overwrite: true);

        // Open the EPUB (ZIP archive) in Update mode so we can replace entries.
        using (FileStream epubStream = new FileStream(outputEpubPath, FileMode.Open, FileAccess.ReadWrite))
        using (ZipArchive archive = new ZipArchive(epubStream, ZipArchiveMode.Update, leaveOpen: true))
        {
            // Find all embedded PDF files (case‑insensitive).
            var pdfEntries = archive.Entries
                                   .Where(e => e.FullName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
                                   .ToList(); // ToList to avoid collection‑modification issues.

            foreach (var entry in pdfEntries)
            {
                // Load the original PDF into a memory stream.
                using (MemoryStream originalPdf = new MemoryStream())
                {
                    using (Stream entryStream = entry.Open())
                    {
                        entryStream.CopyTo(originalPdf);
                    }
                    originalPdf.Position = 0;

                    // Read and modify PDF metadata via the Facade class.
                    PdfFileInfo pdfInfo = new PdfFileInfo(originalPdf);
                    pdfInfo.Title  = "Updated PDF Title";   // Adjust as required for EPUB compliance.
                    pdfInfo.Author = "Updated PDF Author";  // Adjust as required for EPUB compliance.

                    // Save the modified PDF into a new memory stream.
                    using (MemoryStream updatedPdf = new MemoryStream())
                    {
                        pdfInfo.Save(updatedPdf); // Facade Save to stream.
                        updatedPdf.Position = 0;

                        // Replace the existing entry with the updated PDF.
                        // Delete the old entry first.
                        string entryName = entry.FullName; // Preserve the name before deletion.
                        entry.Delete();

                        ZipArchiveEntry newEntry = archive.CreateEntry(entryName, CompressionLevel.Optimal);
                        using (Stream newEntryStream = newEntry.Open())
                        {
                            updatedPdf.CopyTo(newEntryStream);
                        }
                    }
                }
            }
        }

        Console.WriteLine($"EPUB processing complete. Updated file saved as '{outputEpubPath}'.");
    }
}
