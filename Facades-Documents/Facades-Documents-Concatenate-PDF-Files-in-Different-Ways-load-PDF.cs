using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF files to be concatenated
        string[] inputFiles = { "file1.pdf", "file2.pdf", "file3.pdf" };
        // Output file for the simple concatenation
        const string simpleOutput = "merged_simple.pdf";
        // Output file for the concatenation with incremental updates
        const string incrementalOutput = "merged_incremental.pdf";

        // Verify that all input files exist before proceeding
        foreach (string file in inputFiles)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"Input file not found: {file}");
                return;
            }
        }

        // -----------------------------------------------------------------
        // 1. Simple concatenation (default behavior)
        // -----------------------------------------------------------------
        try
        {
            var editor = new PdfFileEditor();
            // Concatenate the PDFs in the order defined in inputFiles
            editor.Concatenate(inputFiles, simpleOutput);
            Console.WriteLine($"Simple concatenation completed: {simpleOutput}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during simple concatenation: {ex.Message}");
        }

        // -----------------------------------------------------------------
        // 2. Concatenation with incremental updates (useful for large sets)
        // -----------------------------------------------------------------
        try
        {
            var editor = new PdfFileEditor
            {
                UseDiskBuffer = true,
                IncrementalUpdates = true,
                CloseConcatenatedStreams = true
            };

            // Perform the concatenation with the above settings
            editor.Concatenate(inputFiles, incrementalOutput);
            Console.WriteLine($"Incremental concatenation completed: {incrementalOutput}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during incremental concatenation: {ex.Message}");
        }

        // -----------------------------------------------------------------
        // 3. Concatenation while preserving outlines and logical structure
        // -----------------------------------------------------------------
        const string preserveOutput = "merged_preserve.pdf";
        try
        {
            var editor = new PdfFileEditor
            {
                CopyOutlines = true,
                CopyLogicalStructure = true
            };

            editor.Concatenate(inputFiles, preserveOutput);
            Console.WriteLine($"Concatenation with outlines preserved completed: {preserveOutput}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during outline-preserving concatenation: {ex.Message}");
        }
    }
}
