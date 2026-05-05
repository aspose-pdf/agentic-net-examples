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

        // Initialize the PdfFileEditor facade
        PdfFileEditor editor = new PdfFileEditor();

        // Preserve outlines, logical structure and user rights during concatenation
        editor.CopyOutlines = true;
        editor.CopyLogicalStructure = true;
        editor.PreserveUserRights = true;

        // Perform concatenation
        bool concatenated = editor.Concatenate(inputFiles, outputFile);
        if (!concatenated)
        {
            Console.Error.WriteLine("Failed to concatenate PDF files.");
            return;
        }

        // Preserve metadata (author, title, subject, keywords) from the first PDF
        using (PdfFileInfo sourceInfo = new PdfFileInfo(inputFiles[0]))
        using (PdfFileInfo destInfo = new PdfFileInfo(outputFile))
        {
            destInfo.Author   = sourceInfo.Author;
            destInfo.Title    = sourceInfo.Title;
            destInfo.Subject  = sourceInfo.Subject;
            destInfo.Keywords = sourceInfo.Keywords;

            // Save the updated metadata back to the output file
            destInfo.SaveNewInfo(outputFile);
        }

        Console.WriteLine($"PDF files successfully concatenated to '{outputFile}'.");
    }
}