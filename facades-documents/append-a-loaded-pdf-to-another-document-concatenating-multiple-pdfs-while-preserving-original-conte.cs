using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the primary PDF that will receive the appended content
        string basePdf = "base.pdf";

        // Paths to the PDFs that should be appended to the base PDF
        string[] additionalPdfs = { "append1.pdf", "append2.pdf" };

        // Destination file that will contain the concatenated result
        string outputPdf = "merged.pdf";

        // Verify that all source files exist before proceeding
        if (!File.Exists(basePdf))
        {
            Console.Error.WriteLine($"Base file not found: {basePdf}");
            return;
        }

        foreach (var file in additionalPdfs)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"File not found: {file}");
                return;
            }
        }

        // Create an instance of PdfFileEditor (facade for file‑level operations)
        PdfFileEditor editor = new PdfFileEditor();

        // Preserve outlines and logical structure during concatenation
        editor.CopyOutlines = true;
        editor.CopyLogicalStructure = true;

        // Build a single array that contains the base PDF followed by the PDFs to append
        string[] allFiles = new string[additionalPdfs.Length + 1];
        allFiles[0] = basePdf;
        Array.Copy(additionalPdfs, 0, allFiles, 1, additionalPdfs.Length);

        // Perform concatenation; the method writes the result directly to outputPdf
        bool success = editor.Concatenate(allFiles, outputPdf);

        if (success)
        {
            Console.WriteLine($"PDFs concatenated successfully to '{outputPdf}'.");
        }
        else
        {
            Console.Error.WriteLine("Concatenation failed.");
        }
    }
}