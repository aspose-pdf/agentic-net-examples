using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF files to be concatenated
        string[] inputFiles = new string[] { "first.pdf", "second.pdf", "third.pdf" };
        // Output PDF file
        string outputFile = "merged.pdf";

        // Verify that all input files exist
        foreach (string file in inputFiles)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"Input file not found: {file}");
                return;
            }
        }

        // Concatenate PDFs using PdfFileEditor
        PdfFileEditor fileEditor = new PdfFileEditor();
        // Optional: keep outlines and logical structure from source files
        fileEditor.CopyOutlines = true;
        fileEditor.CopyLogicalStructure = true;
        bool success = fileEditor.Concatenate(inputFiles, outputFile);
        if (!success)
        {
            Console.Error.WriteLine("Concatenation failed.");
            return;
        }

        // Read metadata (author and title) from the first source document
        string author;
        string title;
        using (Document firstDoc = new Document(inputFiles[0]))
        {
            author = firstDoc.Info.Author;
            title = firstDoc.Info.Title;
        }

        // Apply the retrieved metadata to the merged document
        using (Document mergedDoc = new Document(outputFile))
        {
            mergedDoc.Info.Author = author;
            mergedDoc.Info.Title = title;
            mergedDoc.Save(outputFile);
        }

        Console.WriteLine($"PDF files concatenated successfully to '{outputFile}'.");
    }
}