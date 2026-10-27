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
        const string outputPath = "merged.pdf";

        // Verify that all input files exist
        foreach (string file in inputFiles)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"Input file not found: {file}");
                return;
            }
        }

        // Concatenate PDFs using PdfFileEditor (no using – it is not IDisposable)
        PdfFileEditor editor = new PdfFileEditor();
        editor.Concatenate(inputFiles, outputPath);

        // Preserve original metadata (author, title, etc.) from the first source PDF
        // Load the first source document to read its metadata
        using (Document sourceDoc = new Document(inputFiles[0]))
        // Load the concatenated result to apply metadata
        using (Document resultDoc = new Document(outputPath))
        {
            // Copy metadata fields
            resultDoc.Info.Author      = sourceDoc.Info.Author;
            resultDoc.Info.Title       = sourceDoc.Info.Title;
            resultDoc.Info.Subject     = sourceDoc.Info.Subject;
            resultDoc.Info.Keywords    = sourceDoc.Info.Keywords;
            resultDoc.Info.Creator     = sourceDoc.Info.Creator;
            resultDoc.Info.Producer    = sourceDoc.Info.Producer;
            resultDoc.Info.CreationDate    = sourceDoc.Info.CreationDate;
            // Use ModDate instead of the non‑existent ModificationDate
            resultDoc.Info.ModDate = sourceDoc.Info.ModDate;

            // Save the PDF with updated metadata (overwrites the previous file)
            resultDoc.Save(outputPath);
        }

        Console.WriteLine($"PDFs concatenated and metadata preserved: {outputPath}");
    }
}
