using System;
using System.IO;
using Aspose.Pdf.Facades; // PdfFileEditor, PdfFileInfo

class Program
{
    static void Main()
    {
        // Input PDF files to concatenate
        string[] inputFiles = { "file1.pdf", "file2.pdf", "file3.pdf" };
        // Output file path
        string outputFile = "merged.pdf";

        // Verify that all input files exist
        foreach (string file in inputFiles)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"Input file not found: {file}");
                return;
            }
        }

        // -----------------------------------------------------------------
        // Preserve metadata from the first input PDF (author, title, etc.)
        // -----------------------------------------------------------------
        // PdfFileInfo provides access to document metadata without loading the
        // full Document object.
        PdfFileInfo sourceInfo = new PdfFileInfo(inputFiles[0]);

        string author = sourceInfo.Author;
        string title = sourceInfo.Title;
        string subject = sourceInfo.Subject;
        string keywords = sourceInfo.Keywords;
        // CreationDate and ModDate are stored as PDF‑date formatted strings, not DateTime.
        string creationDate = sourceInfo.CreationDate;
        string modDate = sourceInfo.ModDate;

        // -----------------------------------------------------------------
        // Concatenate the PDFs using PdfFileEditor
        // -----------------------------------------------------------------
        PdfFileEditor editor = new PdfFileEditor
        {
            // Optional: close streams after operation (not needed here because we use file paths)
            CloseConcatenatedStreams = true
        };

        // Perform concatenation; returns true if successful
        bool success = editor.Concatenate(inputFiles, outputFile);
        if (!success)
        {
            Console.Error.WriteLine("Concatenation failed.");
            return;
        }

        // -----------------------------------------------------------------
        // Apply the preserved metadata to the merged PDF
        // -----------------------------------------------------------------
        // Load the merged file's info, set the metadata, and save it back.
        PdfFileInfo mergedInfo = new PdfFileInfo(outputFile);

        // Preserve original metadata (fallback to empty strings if null)
        mergedInfo.Author = author ?? string.Empty;
        mergedInfo.Title = title ?? Path.GetFileNameWithoutExtension(outputFile);
        mergedInfo.Subject = subject ?? string.Empty;
        mergedInfo.Keywords = keywords ?? string.Empty;

        if (!string.IsNullOrEmpty(creationDate)) mergedInfo.CreationDate = creationDate;
        if (!string.IsNullOrEmpty(modDate)) mergedInfo.ModDate = modDate;

        // Save the updated metadata back to the same file
        mergedInfo.SaveNewInfo(outputFile);

        Console.WriteLine($"Successfully concatenated PDFs to '{outputFile}' with preserved metadata.");
    }
}
