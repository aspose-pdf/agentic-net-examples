using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "source.pdf";

        // Define page ranges (start and end inclusive, 1‑based indexing)
        // Example: pages 1‑3, 5‑7, 10‑10
        int[][] ranges = new int[][]
        {
            new int[] { 1, 3 },
            new int[] { 5, 7 },
            new int[] { 10, 10 }
        };

        // Ensure the source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Use PdfFileEditor (Facades) to split the source PDF into separate streams
        PdfFileEditor editor = new PdfFileEditor();
        MemoryStream[] splitStreams = editor.SplitToBulks(inputPdf, ranges);

        // List to hold the individual Document objects
        List<Document> documents = new List<Document>();

        // Load each stream into a separate Document instance
        foreach (MemoryStream ms in splitStreams)
        {
            // Reset stream position before loading
            ms.Position = 0;

            // Load the PDF from the memory stream
            Document doc = new Document(ms);
            documents.Add(doc);
        }

        // Example usage: save each document to a separate file
        for (int i = 0; i < documents.Count; i++)
        {
            string outputPath = $"output_part_{i + 1}.pdf";

            // Wrap Document in using to ensure proper disposal after saving
            using (Document doc = documents[i])
            {
                doc.Save(outputPath);
            }

            Console.WriteLine($"Saved part {i + 1} to '{outputPath}'.");
        }

        // Cleanup: dispose any remaining streams
        foreach (MemoryStream ms in splitStreams)
        {
            ms.Dispose();
        }
    }
}