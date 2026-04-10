using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF files to be concatenated
        string[] inputFiles = new string[]
        {
            "doc1.pdf",
            "doc2.pdf",
            "doc3.pdf"
        };

        // PDF file that contains a single blank page (must exist)
        const string blankPageFile = "blank.pdf";

        // Desired output file
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

        if (!File.Exists(blankPageFile))
        {
            Console.Error.WriteLine($"Blank page file not found: {blankPageFile}");
            return;
        }

        // PdfFileEditor provides the Concatenate overload that inserts a blank page
        // between two PDFs. We'll apply it iteratively to handle an arbitrary number
        // of input files.
        PdfFileEditor editor = new PdfFileEditor();

        // The first file serves as the initial "current" document.
        string currentDocument = inputFiles[0];

        // Keep track of temporary files so they can be cleaned up later.
        var tempFiles = new System.Collections.Generic.List<string>();

        // Iterate over the remaining input files, concatenating each with the current
        // document and inserting a blank page between them.
        for (int i = 1; i < inputFiles.Length; i++)
        {
            // Create a temporary file name for the result of this iteration.
            string tempResult = Path.ChangeExtension(Path.GetTempFileName(), ".pdf");
            tempFiles.Add(tempResult);

            // Perform the concatenation with a blank page filler.
            bool success = editor.Concatenate(
                currentDocument,          // first input
                inputFiles[i],            // second input
                blankPageFile,            // blank page filler
                tempResult);              // output

            if (!success)
            {
                Console.Error.WriteLine($"Failed to concatenate '{currentDocument}' and '{inputFiles[i]}'.");
                // Cleanup any temporary files created so far.
                CleanupTempFiles(tempFiles);
                return;
            }

            // If the previous "currentDocument" was a temporary file, delete it now.
            if (currentDocument != inputFiles[0] && File.Exists(currentDocument))
            {
                File.Delete(currentDocument);
            }

            // The result becomes the new current document for the next iteration.
            currentDocument = tempResult;
        }

        // Copy the final result to the desired output location.
        File.Copy(currentDocument, outputFile, overwrite: true);

        // Cleanup any remaining temporary files (the final currentDocument may be a temp file).
        CleanupTempFiles(tempFiles);
        if (currentDocument != inputFiles[0] && File.Exists(currentDocument))
        {
            File.Delete(currentDocument);
        }

        Console.WriteLine($"Merged PDF saved to '{outputFile}'.");
    }

    // Helper method to delete temporary files safely.
    private static void CleanupTempFiles(System.Collections.Generic.List<string> tempFiles)
    {
        foreach (string temp in tempFiles)
        {
            try
            {
                if (File.Exists(temp))
                {
                    File.Delete(temp);
                }
            }
            catch
            {
                // Ignored – best‑effort cleanup.
            }
        }
        tempFiles.Clear();
    }
}