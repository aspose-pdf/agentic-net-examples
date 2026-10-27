using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF files for the booklet
        string[] inputFiles = { "chapter1.pdf", "chapter2.pdf", "chapter3.pdf" };
        // Output booklet PDF
        string outputFile = "booklet.pdf";
        // Log file for audit
        string logPath = "booklet_creation_log.txt";

        // Open log file
        using (StreamWriter log = new StreamWriter(logPath, false))
        {
            log.WriteLine($"[{DateTime.Now}] Booklet creation started.");

            // Verify input files exist
            List<string> existingFiles = new List<string>();
            foreach (string file in inputFiles)
            {
                if (File.Exists(file))
                {
                    existingFiles.Add(file);
                    log.WriteLine($"[{DateTime.Now}] Found input file: {file}");
                }
                else
                {
                    log.WriteLine($"[{DateTime.Now}] Missing input file: {file}");
                }
            }

            if (existingFiles.Count == 0)
            {
                log.WriteLine($"[{DateTime.Now}] No input files found. Aborting.");
                return;
            }

            // ---------- Merge PDFs ----------
            // PdfFileEditor (Facades) does NOT implement IDisposable – do NOT wrap in using
            PdfFileEditor editor = new PdfFileEditor();

            // Create a temporary file that will hold the merged result
            string tempMerged = "temp_merged.pdf";
            File.Copy(existingFiles[0], tempMerged, true);
            log.WriteLine($"[{DateTime.Now}] Initialized temporary merged file with: {existingFiles[0]}");

            // Append remaining PDFs to the temporary merged file
            for (int i = 1; i < existingFiles.Count; i++)
            {
                string src = existingFiles[i];
                editor.Concatenate(tempMerged, src, tempMerged);
                log.WriteLine($"[{DateTime.Now}] Appended {src} to temporary merged file.");
            }

            // ---------- Reorder pages for booklet ----------
            // Example reordering: reverse page order (replace with real imposition logic as needed)
            using (Document doc = new Document(tempMerged))
            {
                int pageCount = doc.Pages.Count;
                log.WriteLine($"[{DateTime.Now}] Reordering pages for booklet. Total pages: {pageCount}");

                // New document to hold reordered pages
                Document reordered = new Document();

                for (int i = pageCount; i >= 1; i--)
                {
                    reordered.Pages.Add(doc.Pages[i]);
                    log.WriteLine($"[{DateTime.Now}] Added page {i} to reordered document.");
                }

                // Save the final booklet PDF
                reordered.Save(outputFile);
                log.WriteLine($"[{DateTime.Now}] Booklet saved to {outputFile}");
            }

            // Clean up temporary file
            if (File.Exists(tempMerged))
            {
                File.Delete(tempMerged);
                log.WriteLine($"[{DateTime.Now}] Deleted temporary file {tempMerged}");
            }

            log.WriteLine($"[{DateTime.Now}] Booklet creation completed.");
        }
    }
}