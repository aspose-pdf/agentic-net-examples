using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF files to be concatenated
        string[] pdfFiles = { "file1.pdf", "file2.pdf", "file3.pdf" };
        string outputPath = "merged_output.pdf";

        // Verify that all input files exist
        foreach (var file in pdfFiles)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"Input file not found: {file}");
                return;
            }
        }

        // Create the PdfFileEditor instance (does NOT implement IDisposable)
        PdfFileEditor editor = new PdfFileEditor();
        // Close streams automatically after each operation
        editor.CloseConcatenatedStreams = true;

        // ------------------------------------------------------------
        // 1. Concatenate using file paths (string[] overload)
        // ------------------------------------------------------------
        bool resultPath = editor.Concatenate(pdfFiles, outputPath);
        Console.WriteLine($"Concatenation via file paths succeeded: {resultPath}");

        // ------------------------------------------------------------
        // 2. Concatenate using streams (Stream[] overload)
        // ------------------------------------------------------------
        using (FileStream stream1 = new FileStream(pdfFiles[0], FileMode.Open, FileAccess.Read))
        using (FileStream stream2 = new FileStream(pdfFiles[1], FileMode.Open, FileAccess.Read))
        using (FileStream stream3 = new FileStream(pdfFiles[2], FileMode.Open, FileAccess.Read))
        using (FileStream outStream = new FileStream("merged_streams.pdf", FileMode.Create, FileAccess.Write))
        {
            Stream[] inputStreams = { stream1, stream2, stream3 };
            bool resultStreams = editor.Concatenate(inputStreams, outStream);
            Console.WriteLine($"Concatenation via streams succeeded: {resultStreams}");
        }

        // ------------------------------------------------------------
        // 3. Concatenate using Document objects (Document[] overload)
        // ------------------------------------------------------------
        // Load each source PDF into a Document (wrapped in using for deterministic disposal)
        using (Document doc1 = new Document(pdfFiles[0]))
        using (Document doc2 = new Document(pdfFiles[1]))
        using (Document doc3 = new Document(pdfFiles[2]))
        using (Document destination = new Document()) // empty destination document
        {
            Document[] sourceDocs = { doc1, doc2, doc3 };
            bool resultDocs = editor.Concatenate(sourceDocs, destination);
            Console.WriteLine($"Concatenation via Document objects succeeded: {resultDocs}");

            // Save the concatenated result
            destination.Save("merged_documents.pdf");
        }

        Console.WriteLine("All concatenation operations completed.");
    }
}