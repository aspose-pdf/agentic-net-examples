using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF files to be concatenated
        string[] inputFiles = { "file1.pdf", "file2.pdf", "file3.pdf" };
        // Output file path
        string outputFile = "merged.pdf";

        // Verify that all input files exist
        foreach (var file in inputFiles)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"Input file not found: {file}");
                return;
            }
        }

        // Create PdfFileEditor instance (it does NOT implement IDisposable)
        PdfFileEditor editor = new PdfFileEditor();

        // Preserve outlines and logical structure during concatenation
        editor.CopyOutlines = true;
        editor.CopyLogicalStructure = true;

        // Concatenate the PDFs
        bool concatenated = editor.Concatenate(inputFiles, outputFile);
        if (!concatenated)
        {
            Console.Error.WriteLine("Concatenation failed.");
            return;
        }

        // Preserve original metadata (author, title, etc.) from the first source PDF
        PdfFileInfo sourceInfo = new PdfFileInfo(inputFiles[0]);   // loads metadata of first PDF
        PdfFileInfo resultInfo = new PdfFileInfo(outputFile);     // loads metadata of the concatenated PDF

        // Copy metadata fields
        resultInfo.Author = sourceInfo.Author;
        resultInfo.Title = sourceInfo.Title;
        resultInfo.Subject = sourceInfo.Subject;
        resultInfo.Keywords = sourceInfo.Keywords;
        resultInfo.Creator = sourceInfo.Creator;
        resultInfo.CreationDate = sourceInfo.CreationDate;
        resultInfo.ModDate = sourceInfo.ModDate;

        // Save the updated metadata back to the output file
        resultInfo.SaveNewInfo(outputFile);

        Console.WriteLine($"PDFs concatenated successfully to '{outputFile}'.");
    }
}