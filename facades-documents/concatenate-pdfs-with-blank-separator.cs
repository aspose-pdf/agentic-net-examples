using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class PdfSeparatorConcatenator
{
    /// <summary>
    /// Creates a single‑page blank PDF if it does not already exist.
    /// </summary>
    private static void EnsureBlankPageFile(string blankPagePath)
    {
        if (File.Exists(blankPagePath))
            return;

        // Create a new PDF document with one empty page and save it.
        using (Document blankDoc = new Document())
        {
            // Add an empty page (default size A4).
            blankDoc.Pages.Add();
            blankDoc.Save(blankPagePath);
        }
    }

    /// <summary>
    /// Concatenates the given PDF files inserting a blank separator page between each pair.
    /// </summary>
    /// <param name="sourceFiles">Array of source PDF file paths (must exist).</param>
    /// <param name="outputFile">Path of the resulting concatenated PDF.</param>
    /// <param name="blankPageFile">Path of a single‑page blank PDF used as separator.</param>
    public static void ConcatenateWithSeparators(string[] sourceFiles, string outputFile, string blankPageFile)
    {
        if (sourceFiles == null || sourceFiles.Length == 0)
            throw new ArgumentException("At least one source file must be provided.", nameof(sourceFiles));

        // Ensure the blank page PDF exists.
        EnsureBlankPageFile(blankPageFile);

        // Build the interleaved file list: file1, blank, file2, blank, ..., fileN
        var filesToConcat = new List<string>();
        for (int i = 0; i < sourceFiles.Length; i++)
        {
            string src = sourceFiles[i];
            if (!File.Exists(src))
                throw new FileNotFoundException($"Source file not found: {src}");

            filesToConcat.Add(src);

            // Add a separator after every file except the last one.
            if (i < sourceFiles.Length - 1)
                filesToConcat.Add(blankPageFile);
        }

        // Use PdfFileEditor to concatenate the prepared list.
        PdfFileEditor editor = new PdfFileEditor();
        bool success = editor.Concatenate(filesToConcat.ToArray(), outputFile);

        if (!success)
            throw new InvalidOperationException("PdfFileEditor failed to concatenate the documents.");
    }

    // Example usage.
    static void Main()
    {
        // Input PDFs to be merged.
        string[] inputs = { "chapter1.pdf", "chapter2.pdf", "chapter3.pdf" };

        // Path for the generated blank separator page.
        string blankPagePath = "blankSeparator.pdf";

        // Desired output file.
        string resultPath = "mergedWithSeparators.pdf";

        try
        {
            ConcatenateWithSeparators(inputs, resultPath, blankPagePath);
            Console.WriteLine($"Successfully created '{resultPath}' with blank separator pages.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}