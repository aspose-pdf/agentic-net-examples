using System;
using System.IO;
using Aspose.Pdf.Facades;

class RemoveAllSignatures
{
    static void Main(string[] args)
    {
        // Expect a directory path as the first argument; if not provided, use current directory.
        string rootDir = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();

        if (!Directory.Exists(rootDir))
        {
            Console.Error.WriteLine($"Directory not found: {rootDir}");
            return;
        }

        // Get all PDF files recursively.
        string[] pdfFiles = Directory.GetFiles(rootDir, "*.pdf", SearchOption.AllDirectories);

        foreach (string inputPath in pdfFiles)
        {
            try
            {
                // Output file: same name with "_nosig" suffix before extension.
                string dir = Path.GetDirectoryName(inputPath);
                string nameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
                string outputPath = Path.Combine(dir, $"{nameWithoutExt}_nosig.pdf");

                // Use PdfFileSignature facade to bind, remove signatures, and save.
                using (PdfFileSignature pdfSign = new PdfFileSignature())
                {
                    pdfSign.BindPdf(inputPath);          // Load the PDF.
                    pdfSign.RemoveSignatures();          // Remove all signatures.
                    pdfSign.Save(outputPath);            // Save the result.
                }

                Console.WriteLine($"Processed: '{inputPath}' → '{outputPath}'");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }
    }
}