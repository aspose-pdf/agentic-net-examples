using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;

class BatchXfdfImporter
{
    static void Main()
    {
        // Use folders relative to the executable location to avoid hard‑coded paths.
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;
        string inputFolder = Path.Combine(baseDir, "Input");
        string outputFolder = Path.Combine(baseDir, "Output");
        string outputPath = Path.Combine(outputFolder, "merged.pdf");

        // Ensure the required directories exist (creates them if missing).
        Directory.CreateDirectory(inputFolder);
        Directory.CreateDirectory(outputFolder);

        // Collect all PDF files in the input folder.
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);
        if (pdfFiles.Length == 0)
        {
            Console.Error.WriteLine($"No PDF files found in the folder '{inputFolder}'.");
            return;
        }

        // List to hold processed documents.
        List<Document> processedDocs = new List<Document>();

        foreach (string pdfPath in pdfFiles)
        {
            // Determine the corresponding XFDF file (same file name, .xfdf extension).
            string xfdfPath = Path.ChangeExtension(pdfPath, ".xfdf");

            // Load the PDF document.
            Document pdfDoc = new Document(pdfPath);

            // If an XFDF file exists, import its annotations into the PDF.
            if (File.Exists(xfdfPath))
            {
                pdfDoc.ImportAnnotationsFromXfdf(xfdfPath);
            }

            // Keep the document instance for later merging.
            processedDocs.Add(pdfDoc);
        }

        // Merge all processed documents into a single PDF.
        Document mergedDoc = processedDocs[0];
        for (int i = 1; i < processedDocs.Count; i++)
        {
            mergedDoc.Pages.Add(processedDocs[i].Pages);
        }

        // Save the merged PDF.
        mergedDoc.Save(outputPath);

        // Dispose all documents (mergedDoc is the same instance as processedDocs[0]).
        foreach (var doc in processedDocs)
        {
            doc.Dispose();
        }

        Console.WriteLine($"Merged PDF with imported annotations saved to '{outputPath}'.");
    }
}
