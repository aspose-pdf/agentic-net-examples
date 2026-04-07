using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "portfolio.pdf";      // PDF with embedded files
        const string outputPdf = "portfolio_updated.pdf";
        const int    fileIndex = 2;                    // zero‑based index of the file to remove

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Access the collection of embedded files
            EmbeddedFileCollection embeddedFiles = doc.EmbeddedFiles;

            // Validate the requested index
            if (fileIndex < 0 || fileIndex >= embeddedFiles.Count)
            {
                Console.Error.WriteLine($"Invalid index {fileIndex}. Collection contains {embeddedFiles.Count} files.");
                return;
            }

            // Retrieve the name (key) of the embedded file at the given index
            // EmbeddedFileCollection implements IEnumerable; we enumerate until the index is reached
            string fileName = null;
            int current = 0;
            foreach (var entry in embeddedFiles)
            {
                if (current == fileIndex)
                {
                    fileName = entry.Name;   // each entry is an EmbeddedFile object exposing its Name
                    break;
                }
                current++;
            }

            if (fileName == null)
            {
                Console.Error.WriteLine("Failed to locate the embedded file name.");
                return;
            }

            // Delete the embedded file by its name
            embeddedFiles.Delete(fileName);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Embedded file at index {fileIndex} removed. Saved to '{outputPdf}'.");
    }
}