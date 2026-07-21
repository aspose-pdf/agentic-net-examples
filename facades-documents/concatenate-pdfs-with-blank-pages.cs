using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class PdfConcatenator
{
    /// <summary>
    /// Concatenates the specified PDF files and inserts a blank page between each document.
    /// </summary>
    /// <param name="inputFiles">Array of full paths to the source PDF files (order matters).</param>
    /// <param name="outputFile">Full path for the resulting concatenated PDF.</param>
    public static void ConcatenateWithBlankPages(string[] inputFiles, string outputFile)
    {
        if (inputFiles == null || inputFiles.Length == 0)
            throw new ArgumentException("At least one input file must be provided.", nameof(inputFiles));

        foreach (string file in inputFiles)
        {
            if (!File.Exists(file))
                throw new FileNotFoundException($"Input file not found: {file}");
        }

        // Create a temporary single‑page blank PDF.
        string blankPagePath = Path.Combine(Path.GetTempPath(), $"blank_{Guid.NewGuid()}.pdf");
        using (Document blankDoc = new Document())
        {
            // Adding a page creates an empty (white) page.
            blankDoc.Pages.Add();
            blankDoc.Save(blankPagePath);
        }

        // Build the sequence: doc1, blank, doc2, blank, ..., lastDoc (no trailing blank).
        var filesToMerge = new List<string>();
        for (int i = 0; i < inputFiles.Length; i++)
        {
            filesToMerge.Add(inputFiles[i]);
            if (i < inputFiles.Length - 1) // add blank only between documents
                filesToMerge.Add(blankPagePath);
        }

        // Perform concatenation using PdfFileEditor.
        PdfFileEditor editor = new PdfFileEditor();
        editor.Concatenate(filesToMerge.ToArray(), outputFile);

        // Clean up the temporary blank page file.
        try { File.Delete(blankPagePath); } catch { /* ignore cleanup errors */ }
    }

    // Example usage
    static void Main()
    {
        string[] sources = { "doc1.pdf", "doc2.pdf", "doc3.pdf" };
        string result = "merged_with_blanks.pdf";

        try
        {
            ConcatenateWithBlankPages(sources, result);
            Console.WriteLine($"Successfully created: {result}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}