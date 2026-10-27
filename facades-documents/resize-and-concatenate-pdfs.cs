using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF files to process
        string[] inputPdfs = { "doc1.pdf", "doc2.pdf", "doc3.pdf" };
        // Final merged output
        const string outputPdf = "merged_resized.pdf";

        // Temporary folder for resized PDFs
        string tempDir = Path.Combine(Path.GetTempPath(), "ResizedPdfs_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // List to hold paths of successfully resized PDFs
        var resizedList = new System.Collections.Generic.List<string>();

        for (int i = 0; i < inputPdfs.Length; i++)
        {
            string srcPath = inputPdfs[i];
            if (!File.Exists(srcPath))
            {
                Console.Error.WriteLine($"File not found: {srcPath}");
                continue;
            }

            string resizedPath = Path.Combine(tempDir, $"resized_{i + 1}.pdf");

            // Load the source PDF using Document (not PdfPageEditor)
            Document srcDoc = new Document(srcPath);

            // Resize every page to 1024x768 points (1 point = 1/72 inch)
            foreach (Page page in srcDoc.Pages)
            {
                page.SetPageSize(1024, 768);
            }

            srcDoc.Save(resizedPath);
            resizedList.Add(resizedPath);
        }

        // Concatenate all resized PDFs into a single document using PdfFileEditor (Facades API)
        if (resizedList.Any())
        {
            PdfFileEditor fileEditor = new PdfFileEditor();
            fileEditor.Concatenate(resizedList.ToArray(), outputPdf);
        }
        else
        {
            Console.Error.WriteLine("No PDFs were processed – output not created.");
        }

        // Clean up temporary files
        try { Directory.Delete(tempDir, true); } catch { /* ignore cleanup errors */ }

        Console.WriteLine($"Resized and concatenated PDF saved to '{outputPdf}'.");
    }
}
