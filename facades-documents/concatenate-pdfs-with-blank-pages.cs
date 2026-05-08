using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class PdfConcatenator
{
    /// <summary>
    /// Concatenates the specified PDF files inserting a blank page between each consecutive pair.
    /// </summary>
    /// <param name="inputFiles">Array of source PDF file paths (must contain at least one file).</param>
    /// <param name="outputFile">Path of the resulting concatenated PDF.</param>
    public static void ConcatenateWithBlankPages(string[] inputFiles, string outputFile)
    {
        if (inputFiles == null || inputFiles.Length == 0)
            throw new ArgumentException("At least one input file must be provided.", nameof(inputFiles));

        // Create a temporary single‑page blank PDF.
        string blankPdfPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".pdf");
        using (Document blankDoc = new Document())
        {
            // Add an empty page.
            blankDoc.Pages.Add();
            // Save the blank PDF (no SaveOptions needed – default PDF format).
            blankDoc.Save(blankPdfPath);
        }

        // Build a new list: file1, blank, file2, blank, ..., lastFile (no trailing blank).
        List<string> filesToMerge = new List<string>();
        for (int i = 0; i < inputFiles.Length; i++)
        {
            filesToMerge.Add(inputFiles[i]);
            if (i < inputFiles.Length - 1)
                filesToMerge.Add(blankPdfPath);
        }

        // Perform concatenation using PdfFileEditor.
        PdfFileEditor editor = new PdfFileEditor();
        editor.Concatenate(filesToMerge.ToArray(), outputFile);

        // Clean up the temporary blank PDF.
        try
        {
            if (File.Exists(blankPdfPath))
                File.Delete(blankPdfPath);
        }
        catch
        {
            // If deletion fails, ignore – the file resides in the temp folder.
        }
    }

    // Example usage.
    static void Main()
    {
        string[] sources = { "doc1.pdf", "doc2.pdf", "doc3.pdf" };
        string result = "merged_with_blanks.pdf";

        try
        {
            ConcatenateWithBlankPages(sources, result);
            Console.WriteLine($"Successfully created '{result}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during concatenation: {ex.Message}");
        }
    }
}