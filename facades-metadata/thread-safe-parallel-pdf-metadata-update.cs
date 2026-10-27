using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Define a collection of PDFs to process.
        // Each tuple contains: input path, output path, and a new title to set.
        var files = new List<(string InputPath, string OutputPath, string NewTitle)>
        {
            ("doc1.pdf", "doc1_out.pdf", "Document One"),
            ("doc2.pdf", "doc2_out.pdf", "Document Two"),
            ("doc3.pdf", "doc3_out.pdf", "Document Three")
        };

        // Process the PDFs in parallel. Each iteration runs on its own thread.
        Parallel.ForEach(files, fileInfo =>
        {
            // Verify that the source file exists before attempting to modify it.
            if (!File.Exists(fileInfo.InputPath))
            {
                Console.Error.WriteLine($"Source file not found: {fileInfo.InputPath}");
                return;
            }

            // Create a separate PdfFileInfo instance for this thread.
            // No shared mutable state is used, guaranteeing thread‑safe operation.
            PdfFileInfo pdfInfo = new PdfFileInfo();

            // Bind the PDF file to the PdfFileInfo object.
            pdfInfo.BindPdf(fileInfo.InputPath);

            // Modify metadata – here we set a new Title.
            pdfInfo.Title = fileInfo.NewTitle;

            // Save the modified PDF to the designated output path.
            pdfInfo.Save(fileInfo.OutputPath);

            Console.WriteLine($"Processed: {fileInfo.InputPath} → {fileInfo.OutputPath}");
        });
    }
}