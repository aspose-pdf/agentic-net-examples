using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // List of PDF files to process
        string[] pdfFiles = { "file1.pdf", "file2.pdf", "file3.pdf" };

        // Original owner password (assumed known for all files)
        const string originalOwnerPassword = "oldOwner";

        // New owner password to set
        const string newOwnerPassword = "newOwner";

        foreach (string inputPath in pdfFiles)
        {
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                continue;
            }

            // Build output path – same folder with a suffix to avoid overwriting the original
            string outputPath = Path.Combine(
                Path.GetDirectoryName(inputPath) ?? string.Empty,
                Path.GetFileNameWithoutExtension(inputPath) + "_updated.pdf");

            // PdfFileSecurity handles opening, changing password, and saving the file
            using (PdfFileSecurity security = new PdfFileSecurity(inputPath, outputPath))
            {
                // Pass null for newUserPassword to keep the existing user password unchanged
                bool changed = security.ChangePassword(originalOwnerPassword, null, newOwnerPassword);
                if (!changed)
                {
                    Console.Error.WriteLine($"Failed to change owner password for {inputPath}");
                }
            }

            Console.WriteLine($"Processed: {inputPath} -> {outputPath}");
        }
    }
}