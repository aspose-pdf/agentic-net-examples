using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF files to process
        string[] sourceFiles = { "doc1.pdf", "doc2.pdf", "doc3.pdf" };
        // Final merged output
        const string outputFile = "merged_resized.pdf";

        // Temporary folder for intermediate resized PDFs
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeResizeTemp");
        Directory.CreateDirectory(tempDir);

        // Array to hold paths of resized PDFs
        string[] resizedFiles = new string[sourceFiles.Length];

        // Facade that provides resize and concatenate operations
        PdfFileEditor editor = new PdfFileEditor();

        // Resize each source PDF to 1024x768
        for (int i = 0; i < sourceFiles.Length; i++)
        {
            string srcPath = sourceFiles[i];
            if (!File.Exists(srcPath))
            {
                Console.Error.WriteLine($"Source file not found: {srcPath}");
                return;
            }

            // Destination path for the resized PDF
            string resizedPath = Path.Combine(tempDir, $"resized_{i}.pdf");

            // Resize all pages (null pageNumbers means all pages) to the target dimensions
            // Parameters: inputFile, outputFile, pageNumbers, newWidth, newHeight
            editor.ResizeContents(srcPath, resizedPath, null, 1024, 768);

            resizedFiles[i] = resizedPath;
        }

        // Concatenate all resized PDFs into a single document
        // Parameters: inputFiles[], outputFile
        editor.Concatenate(resizedFiles, outputFile);

        // Clean up temporary files
        foreach (string file in resizedFiles)
        {
            try { File.Delete(file); } catch { /* ignore cleanup errors */ }
        }
        try { Directory.Delete(tempDir, true); } catch { /* ignore cleanup errors */ }

        Console.WriteLine($"Merged PDF saved to '{outputFile}'.");
    }
}