using System;
using System.Globalization;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF files to be concatenated
        string[] inputFiles = { "file1.pdf", "file2.pdf", "file3.pdf" };
        // Output merged PDF file
        const string outputFile = "merged.pdf";

        // Verify that all input files exist
        foreach (string path in inputFiles)
        {
            if (!File.Exists(path))
            {
                Console.Error.WriteLine($"Input file not found: {path}");
                return;
            }
        }

        // -----------------------------------------------------------------
        // Preserve original metadata (author, title, etc.) from the first PDF
        // -----------------------------------------------------------------
        PdfFileInfo firstInfo = new PdfFileInfo(inputFiles[0]);
        string author   = firstInfo.Author;
        string title    = firstInfo.Title;
        string subject  = firstInfo.Subject;
        string keywords = firstInfo.Keywords;
        // PdfFileInfo stores dates as PDF‑date formatted strings (e.g., "yyyyMMddHHmmss")
        string creationDateStr = firstInfo.CreationDate;
        string modDateStr      = firstInfo.ModDate;

        // --------------------------------------------------------------
        // Concatenate PDFs using PdfFileEditor (facade API, not IDisposable)
        // --------------------------------------------------------------
        PdfFileEditor editor = new PdfFileEditor();

        // Preserve outlines and logical structure during concatenation
        editor.CopyOutlines = true;
        editor.CopyLogicalStructure = true;

        // Preserve user rights from the first document (optional)
        editor.PreserveUserRights = true;

        // Perform concatenation; returns true on success
        bool concatenated = editor.Concatenate(inputFiles, outputFile);
        if (!concatenated)
        {
            Console.Error.WriteLine("Concatenation failed.");
            return;
        }

        // --------------------------------------------------------------
        // Apply the preserved metadata to the merged PDF
        // --------------------------------------------------------------
        using (Document mergedDoc = new Document(outputFile))
        {
            mergedDoc.Info.Author   = author;
            mergedDoc.Info.Title    = title;
            mergedDoc.Info.Subject  = subject;
            mergedDoc.Info.Keywords = keywords;

            // Convert the PDF‑date strings to DateTime before assigning
            if (!string.IsNullOrEmpty(creationDateStr) &&
                DateTime.TryParseExact(creationDateStr, "yyyyMMddHHmmss", CultureInfo.InvariantCulture,
                                        DateTimeStyles.None, out DateTime creationDt))
            {
                mergedDoc.Info.CreationDate = creationDt;
            }

            if (!string.IsNullOrEmpty(modDateStr) &&
                DateTime.TryParseExact(modDateStr, "yyyyMMddHHmmss", CultureInfo.InvariantCulture,
                                        DateTimeStyles.None, out DateTime modDt))
            {
                mergedDoc.Info.ModDate = modDt;
            }

            // Overwrite the same file with updated metadata
            mergedDoc.Save(outputFile);
        }

        Console.WriteLine($"Successfully concatenated PDFs to '{outputFile}'.");
    }
}
