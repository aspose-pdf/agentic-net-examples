using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

public static class PdfCleaner
{
    // Deletes specified pages from each PDF file.
    // inputFiles: array of source PDF file paths.
    // pagesToDelete: array where each element is an int[] of page numbers to delete for the corresponding file.
    // Returns array of output file paths (same folder with "_cleaned" suffix).
    public static string[] DeletePages(string[] inputFiles, int[][] pagesToDelete)
    {
        if (inputFiles == null) throw new ArgumentNullException(nameof(inputFiles));
        if (pagesToDelete == null) throw new ArgumentNullException(nameof(pagesToDelete));
        if (inputFiles.Length != pagesToDelete.Length)
            throw new ArgumentException("inputFiles and pagesToDelete must have the same length.");

        var outputFiles = new List<string>(inputFiles.Length);
        var editor = new PdfFileEditor();

        for (int i = 0; i < inputFiles.Length; i++)
        {
            string input = inputFiles[i];
            int[] pages = pagesToDelete[i];

            if (!File.Exists(input))
                throw new FileNotFoundException($"Input file not found: {input}");

            // Build output path: same directory, same name with "_cleaned" before extension.
            string dir = Path.GetDirectoryName(input) ?? string.Empty;
            string name = Path.GetFileNameWithoutExtension(input);
            string ext = Path.GetExtension(input);
            string output = Path.Combine(dir, $"{name}_cleaned{ext}");

            // Delete pages and save to output.
            // PdfFileEditor.Delete returns bool indicating success.
            bool success = editor.Delete(input, pages, output);
            if (!success)
                throw new InvalidOperationException($"Failed to delete pages from {input}");

            outputFiles.Add(output);
        }

        return outputFiles.ToArray();
    }
}

public class Program
{
    // Entry point required for a console‑type project.
    public static void Main(string[] args)
    {
        // Optional demo – can be removed or replaced in production code.
        // string[] inputs = { "sample1.pdf", "sample2.pdf" };
        // int[][] pages = { new int[] { 1, 2 }, new int[] { 3 } };
        // var cleaned = PdfCleaner.DeletePages(inputs, pages);
        // Console.WriteLine(string.Join(Environment.NewLine, cleaned));
    }
}