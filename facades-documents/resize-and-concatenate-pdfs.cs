using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // List of input PDF files to process
        string[] inputFiles = { "input1.pdf", "input2.pdf", "input3.pdf" };
        string outputFile = "merged_resized.pdf";

        // Temporary directory for storing resized PDFs
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeResized");
        Directory.CreateDirectory(tempDir);
        List<string> resizedFiles = new List<string>();

        // Resize each PDF to 1024x768 points (1 point = 1/72 inch)
        foreach (string srcPath in inputFiles)
        {
            if (!File.Exists(srcPath))
            {
                Console.Error.WriteLine($"File not found: {srcPath}");
                continue;
            }

            string resizedPath = Path.Combine(tempDir,
                Path.GetFileNameWithoutExtension(srcPath) + "_resized.pdf");

            // Load the document, adjust each page size via PageInfo, then save
            Document doc = new Document(srcPath);
            int pageCount = doc.Pages.Count;
            for (int i = 1; i <= pageCount; i++)
            {
                // Set each page size to 1024x768 points
                doc.Pages[i].PageInfo.Width = 1024;
                doc.Pages[i].PageInfo.Height = 768;
            }
            doc.Save(resizedPath);
            resizedFiles.Add(resizedPath);
        }

        // Concatenate all resized PDFs into a single document using PdfFileEditor
        if (resizedFiles.Count > 0)
        {
            PdfFileEditor fileEditor = new PdfFileEditor();
            fileEditor.Concatenate(resizedFiles.ToArray(), outputFile);
            Console.WriteLine($"Merged resized PDF saved to '{outputFile}'.");
        }
        else
        {
            Console.WriteLine("No PDFs were processed.");
        }

        // Clean up temporary files and directory
        foreach (string tmp in resizedFiles)
        {
            try { File.Delete(tmp); } catch { }
        }
        try { Directory.Delete(tempDir, true); } catch { }
    }
}
