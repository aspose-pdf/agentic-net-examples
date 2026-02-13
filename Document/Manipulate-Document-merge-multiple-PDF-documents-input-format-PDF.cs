using System;
using System.IO;
using Aspose.Pdf;

class PdfMerger
{
    /// <summary>
    /// Merges multiple PDF files into a single PDF.
    /// </summary>
    /// <param name="inputFiles">Array of full paths to source PDF files.</param>
    /// <param name="outputFile">Full path for the merged PDF.</param>
    public static void MergePdfFiles(string[] inputFiles, string outputFile)
    {
        if (inputFiles == null || inputFiles.Length == 0)
            throw new ArgumentException("No input files specified.", nameof(inputFiles));

        // Validate that all source files exist
        foreach (var file in inputFiles)
        {
            if (!File.Exists(file))
                throw new FileNotFoundException($"Source PDF not found: {file}");
        }

        // Load the first document which will serve as the base
        Document mergedDocument = new Document(inputFiles[0]);

        // Append pages from the remaining documents
        for (int i = 1; i < inputFiles.Length; i++)
        {
            Document docToAppend = new Document(inputFiles[i]);
            // Add all pages from the current document to the merged document
            mergedDocument.Pages.Add(docToAppend.Pages);
        }

        // Ensure the output directory exists
        string outputDir = Path.GetDirectoryName(Path.GetFullPath(outputFile));
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
            Directory.CreateDirectory(outputDir);

        // Save the merged PDF (uses the document-save rule)
        mergedDocument.Save(outputFile);
    }

    // Example usage
    static void Main()
    {
        try
        {
            string[] sources = new string[]
            {
                "input1.pdf",
                "input2.pdf",
                "input3.pdf"
            };
            string resultPath = "merged_output.pdf";

            MergePdfFiles(sources, resultPath);
            Console.WriteLine($"PDFs merged successfully into '{resultPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during PDF merge: {ex.Message}");
        }
    }
}