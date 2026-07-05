using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // List of PDF files to process
        string[] pdfFiles = { "file1.pdf", "file2.pdf", "file3.pdf" };

        // Original owner password (assumed same for all files)
        string originalOwnerPassword = "oldOwner";

        // New owner password to set
        string newOwnerPassword = "newOwner";

        foreach (string inputPath in pdfFiles)
        {
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                continue;
            }

            // Create output file name with a suffix
            string outputPath = Path.Combine(
                Path.GetDirectoryName(inputPath) ?? string.Empty,
                Path.GetFileNameWithoutExtension(inputPath) + "_updated.pdf");

            // Initialize PdfFileSecurity with input and output files
            using (PdfFileSecurity security = new PdfFileSecurity(inputPath, outputPath))
            {
                // Change only the owner password; pass null for newUserPassword to keep the existing user password
                bool changed = security.ChangePassword(originalOwnerPassword, null, newOwnerPassword);
                if (!changed)
                {
                    Console.Error.WriteLine($"Failed to change password for {inputPath}");
                }
            }

            Console.WriteLine($"Processed: {inputPath} -> {outputPath}");
        }
    }
}