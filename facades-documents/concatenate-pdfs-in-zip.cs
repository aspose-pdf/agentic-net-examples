using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string zipPath = "input.zip";          // Path to the zip archive
        const string outputEntryName = "merged.pdf"; // Name of the merged PDF inside the zip

        // ------------------------------------------------------------
        // 1️⃣ Ensure the zip archive exists and contains at least two PDFs.
        // ------------------------------------------------------------
        if (!File.Exists(zipPath))
        {
            CreateSampleZip(zipPath);
        }

        // ------------------------------------------------------------
        // 2️⃣ Open the zip archive for reading and updating
        // ------------------------------------------------------------
        using (FileStream zipStream = new FileStream(zipPath, FileMode.Open, FileAccess.ReadWrite))
        using (ZipArchive archive = new ZipArchive(zipStream, ZipArchiveMode.Update))
        {
            // Collect PDF streams from the archive entries
            List<Stream> pdfStreams = new List<Stream>();
            foreach (ZipArchiveEntry entry in archive.Entries)
            {
                if (entry.Name.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
                {
                    // Copy entry content to a memory stream (required for PdfFileEditor)
                    MemoryStream ms = new MemoryStream();
                    using (Stream entryStream = entry.Open())
                    {
                        entryStream.CopyTo(ms);
                    }
                    ms.Position = 0;
                    pdfStreams.Add(ms);
                }
            }

            if (pdfStreams.Count == 0)
            {
                Console.Error.WriteLine("No PDF files found in the zip archive.");
                return;
            }

            // ------------------------------------------------------------
            // 3️⃣ Concatenate PDFs using PdfFileEditor
            // ------------------------------------------------------------
            // NOTE: Do NOT wrap the output MemoryStream in a using block because
            // PdfFileEditor may close the stream when CloseConcatenatedStreams is true.
            // We keep the stream alive, reset its position, write it to the zip,
            // and then dispose it manually.
            MemoryStream outputPdf = new MemoryStream();
            try
            {
                PdfFileEditor editor = new PdfFileEditor();
                // Keep source streams open until we dispose them ourselves.
                editor.CloseConcatenatedStreams = false;
                bool success = editor.Concatenate(pdfStreams.ToArray(), outputPdf);
                if (!success)
                {
                    Console.Error.WriteLine("Failed to concatenate PDF files.");
                    return;
                }

                // Reset position to the beginning before writing back to the zip
                outputPdf.Position = 0;

                // Remove existing entry if it already exists (nullable handling)
                ZipArchiveEntry? existing = archive.GetEntry(outputEntryName);
                existing?.Delete();

                // Create a new entry for the merged PDF and write the data
                ZipArchiveEntry newEntry = archive.CreateEntry(outputEntryName);
                using (Stream entryStream = newEntry.Open())
                {
                    outputPdf.CopyTo(entryStream);
                }
            }
            finally
            {
                // Ensure the output stream is disposed even if an exception occurs.
                outputPdf.Dispose();
            }

            // Dispose all source PDF streams
            foreach (Stream s in pdfStreams)
            {
                s.Dispose();
            }
        }

        // ------------------------------------------------------------
        // 4️⃣ Extract the merged PDF to the file system so the user can see the result.
        // ------------------------------------------------------------
        using (ZipArchive archive = ZipFile.OpenRead(zipPath))
        {
            ZipArchiveEntry? merged = archive.GetEntry(outputEntryName);
            if (merged != null)
            {
                merged.ExtractToFile("merged_output.pdf", overwrite: true);
                Console.WriteLine("Merged PDF extracted to 'merged_output.pdf'.");
            }
        }

        Console.WriteLine("PDF files concatenated and saved back into the zip archive.");
    }

    /// <summary>
    /// Creates a zip file containing two simple PDF documents.
    /// This method makes the example self‑contained – no external files are required.
    /// </summary>
    private static void CreateSampleZip(string zipPath)
    {
        // Create two in‑memory PDFs
        byte[] pdf1 = CreateSamplePdf("First PDF – Hello World!");
        byte[] pdf2 = CreateSamplePdf("Second PDF – Aspose.PDF Demo");

        // Write them into a new zip archive
        using (FileStream fs = new FileStream(zipPath, FileMode.Create))
        using (ZipArchive archive = new ZipArchive(fs, ZipArchiveMode.Create))
        {
            ZipArchiveEntry e1 = archive.CreateEntry("doc1.pdf");
            using (Stream s = e1.Open())
                s.Write(pdf1, 0, pdf1.Length);

            ZipArchiveEntry e2 = archive.CreateEntry("doc2.pdf");
            using (Stream s = e2.Open())
                s.Write(pdf2, 0, pdf2.Length);
        }
    }

    /// <summary>
    /// Generates a minimal PDF containing a single line of text and returns its bytes.
    /// </summary>
    private static byte[] CreateSamplePdf(string text)
    {
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();
            TextFragment tf = new TextFragment(text)
            {
                // Simple formatting – not required for the demo but makes the PDF readable
                Position = new Position(100, 700),
                TextState = { FontSize = 14, FontStyle = FontStyles.Bold }
            };
            page.Paragraphs.Add(tf);

            using (MemoryStream ms = new MemoryStream())
            {
                doc.Save(ms);
                return ms.ToArray();
            }
        }
    }
}
