using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    // Desired page size in points (1 point = 1/72 inch)
    const double TargetWidth  = 1024; // points
    const double TargetHeight = 768;  // points

    static void Main(string[] args)
    {
        // Input PDF files – can be passed as command‑line arguments or hard‑coded.
        string[] inputFiles = args.Length > 0
            ? args
            : new string[] { "doc1.pdf", "doc2.pdf", "doc3.pdf" };

        const string outputFile = "merged_resized.pdf";

        // List to hold resized PDF documents in memory.
        List<Document> resizedDocs = new List<Document>();

        // Resize each PDF to the target dimensions.
        foreach (string inputPath in inputFiles)
        {
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                continue;
            }

            // Open the source PDF as a read‑only stream.
            using (FileStream srcStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
            {
                // Destination stream will hold the resized PDF.
                MemoryStream dstStream = new MemoryStream();

                // PdfFileEditor provides the ResizeContents operation.
                // It does **not** implement IDisposable, so we do NOT wrap it in a using block.
                PdfFileEditor editor = new PdfFileEditor();
                bool success = editor.ResizeContents(
                    srcStream,          // input stream
                    dstStream,          // output stream
                    null,               // all pages
                    TargetWidth,        // new width in points
                    TargetHeight);      // new height in points

                if (!success)
                {
                    Console.Error.WriteLine($"Failed to resize: {inputPath}");
                    dstStream.Dispose();
                    continue;
                }

                // Reset position so it can be read later.
                dstStream.Position = 0;

                // Load the resized PDF into an Aspose.Pdf.Document for later concatenation.
                Document resizedDoc = new Document(dstStream);
                resizedDocs.Add(resizedDoc);

                // The stream can now be disposed – the Document has its own copy of the data.
                dstStream.Dispose();
            }
        }

        if (resizedDocs.Count == 0)
        {
            Console.Error.WriteLine("No PDFs were resized – aborting concatenation.");
            return;
        }

        // Concatenate all resized PDFs into a single document.
        Document finalDoc = new Document();
        foreach (var doc in resizedDocs)
        {
            // Add all pages from the current document to the final document.
            finalDoc.Pages.Add(doc.Pages);
        }

        // Save the merged PDF.
        finalDoc.Save(outputFile);

        // Dispose all temporary Document objects.
        foreach (var doc in resizedDocs)
            doc.Dispose();

        Console.WriteLine($"Resized and concatenated PDF saved as '{outputFile}'.");
    }
}
