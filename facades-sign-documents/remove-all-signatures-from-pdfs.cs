using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Determine the root directory: use first argument or current directory
        string rootDir = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();

        if (!Directory.Exists(rootDir))
        {
            Console.Error.WriteLine($"Directory not found: {rootDir}");
            return;
        }

        // Get all PDF files recursively
        string[] pdfFiles = Directory.GetFiles(rootDir, "*.pdf", SearchOption.AllDirectories);
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found.");
            return;
        }

        foreach (string pdfPath in pdfFiles)
        {
            try
            {
                // Create a temporary file path in the same directory
                string tempPath = Path.Combine(
                    Path.GetDirectoryName(pdfPath) ?? "",
                    Path.GetFileNameWithoutExtension(pdfPath) + "_nosig.tmp");

                // Remove all signatures using PdfFileSignature (the correct API)
                using (PdfFileSignature sig = new PdfFileSignature())
                {
                    sig.BindPdf(pdfPath);
                    // The RemoveSignature overload that accepts the output file path
                    sig.RemoveSignature(tempPath);
                }

                // Replace the original file with the signature‑free version
                File.Delete(pdfPath);
                File.Move(tempPath, pdfPath);

                Console.WriteLine($"Signatures removed: {pdfPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to process '{pdfPath}': {ex.Message}");
            }
        }
    }
}
