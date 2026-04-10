using System;
using System.IO;
using Aspose.Pdf.Facades;

class RemoveAllSignatures
{
    static void Main(string[] args)
    {
        // Expect a directory path as the first argument; if not provided, use the current directory.
        string rootDir = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();

        if (!Directory.Exists(rootDir))
        {
            Console.Error.WriteLine($"Directory not found: {rootDir}");
            return;
        }

        // Process every PDF file in the directory tree.
        foreach (string pdfPath in Directory.EnumerateFiles(rootDir, "*.pdf", SearchOption.AllDirectories))
        {
            try
            {
                // Build an output file name – original name with "_nosig" suffix before the extension.
                string dir = Path.GetDirectoryName(pdfPath);
                string nameWithoutExt = Path.GetFileNameWithoutExtension(pdfPath);
                string outPath = Path.Combine(dir, $"{nameWithoutExt}_nosig.pdf");

                // Use the PdfFileSignature facade to open, modify, and save the PDF.
                using (PdfFileSignature signer = new PdfFileSignature())
                {
                    signer.BindPdf(pdfPath);          // Load the PDF.
                    signer.RemoveSignatures();        // Remove all digital signatures.
                    signer.Save(outPath);             // Save the modified PDF.
                }

                Console.WriteLine($"Processed: {pdfPath} → {outPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}