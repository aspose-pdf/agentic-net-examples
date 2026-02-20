using System;
using System.IO;
using Aspose.Pdf.Facades;

class PdfMerger
{
    // Merges multiple PDF files into a single PDF.
    // sourceFiles: array of full paths to the PDFs to be merged.
    // outputFile: full path for the resulting merged PDF.
    static void MergePdfs(string[] sourceFiles, string outputFile)
    {
        // Validate input files
        foreach (var file in sourceFiles)
        {
            if (!File.Exists(file))
                throw new FileNotFoundException($"Source PDF not found: {file}");
        }

        // Ensure the output directory exists
        var outDir = Path.GetDirectoryName(Path.GetFullPath(outputFile));
        if (!string.IsNullOrEmpty(outDir) && !Directory.Exists(outDir))
            Directory.CreateDirectory(outDir);

        try
        {
            // PdfFileEditor does NOT implement IDisposable, so we instantiate it directly
            var editor = new PdfFileEditor();

            // Optional: copy outlines and logical structure from source PDFs
            editor.CopyOutlines = true;
            editor.CopyLogicalStructure = true;
            editor.KeepActions = true;
            editor.KeepFieldsUnique = true;

            // Perform concatenation
            editor.Concatenate(sourceFiles, outputFile);
        }
        catch (Exception ex)
        {
            // Wrap the exception to give more context while preserving the original stack trace
            throw new InvalidOperationException($"Failed to merge PDFs into '{outputFile}'.", ex);
        }
    }

    static void Main(string[] args)
    {
        try
        {
            // Example usage:
            // Adjust the file paths as needed.
            string[] filesToMerge = new string[]
            {
                "input1.pdf",
                "input2.pdf",
                "input3.pdf"
            };
            string mergedOutput = "merged_output.pdf";

            MergePdfs(filesToMerge, mergedOutput);
            Console.WriteLine($"PDF files merged successfully into '{mergedOutput}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during PDF merge: {ex.Message}");
        }
    }
}
