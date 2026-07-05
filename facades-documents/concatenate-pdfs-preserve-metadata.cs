using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Files to be concatenated (order matters)
        string[] inputFiles = { "file1.pdf", "file2.pdf", "file3.pdf" };
        string outputFile = "merged.pdf";

        // Ensure every source file exists before proceeding
        foreach (var path in inputFiles)
        {
            if (!File.Exists(path))
            {
                Console.Error.WriteLine($"Source file not found: {path}");
                return;
            }
        }

        // ------------------------------------------------------------
        // Concatenate PDFs using PdfFileEditor (facade API)
        // ------------------------------------------------------------
        PdfFileEditor editor = new PdfFileEditor();

        // Preserve logical structure, outlines and user rights of the first document
        editor.CopyLogicalStructure = true;
        editor.CopyOutlines = true;
        editor.PreserveUserRights = true;

        // Perform the concatenation; returns true on success
        bool concatenated = editor.Concatenate(inputFiles, outputFile);
        if (!concatenated)
        {
            Console.Error.WriteLine("Concatenation failed.");
            return;
        }

        // ------------------------------------------------------------
        // Preserve original metadata (author, title, etc.) from the first PDF
        // ------------------------------------------------------------
        // Load metadata from the first source PDF
        PdfFileInfo sourceInfo = new PdfFileInfo(inputFiles[0]);

        // Load metadata from the newly created merged PDF
        PdfFileInfo mergedInfo = new PdfFileInfo(outputFile);

        // Copy desired metadata fields
        mergedInfo.Author      = sourceInfo.Author;
        mergedInfo.Title       = sourceInfo.Title;
        mergedInfo.Subject     = sourceInfo.Subject;
        mergedInfo.Keywords    = sourceInfo.Keywords;
        mergedInfo.Creator     = sourceInfo.Creator;
        mergedInfo.CreationDate = sourceInfo.CreationDate;
        mergedInfo.ModDate     = sourceInfo.ModDate;

        // Save the updated metadata back to the merged file
        mergedInfo.SaveNewInfo(outputFile);

        Console.WriteLine($"PDFs concatenated successfully. Output saved to '{outputFile}'.");
    }
}