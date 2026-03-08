using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    // Loads each PDF file into a Document (PdfDocument) and keeps a PdfViewer alive.
    // The PdfViewer is optional but retained to match the original design.
    static void LoadPdfDocuments(IEnumerable<string> pdfFilePaths,
                                 out List<Document> documents,
                                 out List<PdfViewer> viewers)
    {
        documents = new List<Document>();
        viewers   = new List<PdfViewer>();

        foreach (var path in pdfFilePaths)
        {
            if (!File.Exists(path))
                continue; // skip missing files

            // Load the PDF into a Document (keeps the whole file in memory).
            Document doc = new Document(path);
            documents.Add(doc);

            // Create a PdfViewer and bind the same file – this mimics the original
            // behaviour where a viewer was kept alive while the document was used.
            PdfViewer viewer = new PdfViewer();
            viewer.BindPdf(path);
            viewers.Add(viewer);
        }
    }

    static void Main()
    {
        // Example list of PDF files to load
        var pdfFiles = new[] { "file1.pdf", "file2.pdf", "file3.pdf" };

        // Load the PDFs
        LoadPdfDocuments(pdfFiles, out List<Document> docs, out List<PdfViewer> viewers);

        // Demonstrate that the documents are in memory
        for (int i = 0; i < docs.Count; i++)
        {
            Console.WriteLine($"Document {i + 1}: {docs[i].Pages.Count} pages loaded.");
        }

        // Clean up: dispose viewers (which also releases any resources they hold)
        foreach (var viewer in viewers)
        {
            viewer.Close();   // releases resources held by the facade
            viewer.Dispose(); // explicit dispose for safety
        }
    }
}
