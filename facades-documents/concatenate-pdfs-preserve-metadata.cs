using System;
using System.Globalization;
using System.IO;
using Aspose.Pdf;                     // Core PDF API
using Aspose.Pdf.Facades;            // PdfFileEditor and PdfFileInfo facades

class Program
{
    static void Main()
    {
        // Input PDF files to concatenate
        string[] inputFiles = { "first.pdf", "second.pdf", "third.pdf" };
        // Output file path
        const string outputFile = "merged.pdf";

        // Verify that all input files exist
        foreach (string file in inputFiles)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"Input file not found: {file}");
                return;
            }
        }

        try
        {
            // -----------------------------------------------------------------
            // 1. Preserve metadata from the first document (author, title, etc.)
            // -----------------------------------------------------------------
            // PdfFileInfo provides access to document metadata without loading the
            // full Document object, which is efficient for this purpose.
            PdfFileInfo sourceInfo = new PdfFileInfo(inputFiles[0]);

            // Store needed metadata values
            string author   = sourceInfo.Author;
            string title    = sourceInfo.Title;
            string subject  = sourceInfo.Subject;
            string keywords = sourceInfo.Keywords;

            // PdfFileInfo returns dates as PDF‑date formatted strings (e.g., "yyyyMMddHHmmss").
            // Convert them to DateTime? for later assignment to Document.Info.
            DateTime? creationDate = ParsePdfDate(sourceInfo.CreationDate);
            DateTime? modDate      = ParsePdfDate(sourceInfo.ModDate);

            // ---------------------------------------------------------------
            // 2. Concatenate the PDFs using PdfFileEditor (no using – not IDisposable)
            // ---------------------------------------------------------------
            PdfFileEditor editor = new PdfFileEditor();

            // Optional: preserve outlines and logical structure if required
            editor.CopyOutlines = true;
            editor.CopyLogicalStructure = true;

            // Perform concatenation
            bool success = editor.Concatenate(inputFiles, outputFile);
            if (!success)
            {
                Console.Error.WriteLine("Concatenation failed.");
                return;
            }

            // ---------------------------------------------------------------
            // 3. Apply the preserved metadata to the newly created file
            // ---------------------------------------------------------------
            // Load the merged PDF as a Document to modify its Info dictionary.
            using (Document mergedDoc = new Document(outputFile))
            {
                // Only set metadata if the source had a value (preserve existing otherwise)
                if (!string.IsNullOrEmpty(author))   mergedDoc.Info.Author   = author;
                if (!string.IsNullOrEmpty(title))    mergedDoc.Info.Title    = title;
                if (!string.IsNullOrEmpty(subject))  mergedDoc.Info.Subject  = subject;
                if (!string.IsNullOrEmpty(keywords)) mergedDoc.Info.Keywords = keywords;
                if (creationDate.HasValue) mergedDoc.Info.CreationDate = creationDate.Value;
                if (modDate.HasValue)      mergedDoc.Info.ModDate      = modDate.Value;

                // Save the document back to the same file (overwrites)
                mergedDoc.Save(outputFile);
            }

            Console.WriteLine($"PDFs concatenated successfully. Output saved to '{outputFile}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Parses a PDF‑date formatted string ("yyyyMMddHHmmss") into a nullable DateTime.
    /// Returns null if the input is null, empty, or not in the expected format.
    /// </summary>
    private static DateTime? ParsePdfDate(string pdfDateString)
    {
        if (string.IsNullOrEmpty(pdfDateString))
            return null;

        // Aspose returns dates in the PDF date format without the leading "D:" prefix.
        // Example: "20230818123045" => 2023‑08‑18 12:30:45
        const string pdfDateFormat = "yyyyMMddHHmmss";
        if (DateTime.TryParseExact(pdfDateString, pdfDateFormat, CultureInfo.InvariantCulture,
                                   DateTimeStyles.None, out DateTime parsed))
        {
            return parsed;
        }
        // If parsing fails, fall back to generic parsing as a safety net.
        if (DateTime.TryParse(pdfDateString, out parsed))
            return parsed;
        return null;
    }
}
