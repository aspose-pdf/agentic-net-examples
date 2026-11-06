using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF files to concatenate
        string[] inputFiles = { "doc1.pdf", "doc2.pdf", "doc3.pdf" };
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

        // Concatenate PDFs using PdfFileEditor (Facades API)
        PdfFileEditor editor = new PdfFileEditor();
        // The Concatenate method accepts an array of source files and the destination file
        editor.Concatenate(inputFiles, outputFile);

        // Preserve original metadata (Author, Title) from the first source PDF
        // Load the first source document to read its metadata
        string firstSource = inputFiles[0];
        string author;
        string title;

        using (Document srcDoc = new Document(firstSource))
        {
            author = srcDoc.Info.Author;
            title  = srcDoc.Info.Title;
        }

        // Load the concatenated document and apply the preserved metadata
        using (Document mergedDoc = new Document(outputFile))
        {
            // If the source metadata is not empty, assign it to the merged document
            if (!string.IsNullOrEmpty(author))
                mergedDoc.Info.Author = author;
            if (!string.IsNullOrEmpty(title))
                mergedDoc.Info.Title = title;

            // Save the document back to the same output path
            mergedDoc.Save(outputFile);
        }

        Console.WriteLine($"PDFs concatenated successfully. Metadata preserved in '{outputFile}'.");
    }
}