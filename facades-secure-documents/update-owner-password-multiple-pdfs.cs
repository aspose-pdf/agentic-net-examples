using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input: list of PDF files to process
        string[] pdfFiles = new string[]
        {
            @"C:\Docs\file1.pdf",
            @"C:\Docs\file2.pdf",
            @"C:\Docs\file3.pdf"
        };

        // Original passwords (same for all files in this example)
        const string originalUserPassword = "user123";
        const string originalOwnerPassword = "owner123";

        // New owner password to set
        const string newOwnerPassword = "newOwner456";

        foreach (string inputPath in pdfFiles)
        {
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                continue;
            }

            // Build output path (same folder, "_updated" suffix)
            string directory = Path.GetDirectoryName(inputPath);
            string filename  = Path.GetFileNameWithoutExtension(inputPath);
            string extension = Path.GetExtension(inputPath);
            string outputPath = Path.Combine(directory, $"{filename}_updated{extension}");

            try
            {
                // PdfFileSecurity works with input and output file names directly
                using (PdfFileSecurity security = new PdfFileSecurity(inputPath, outputPath))
                {
                    // Change only the owner password; keep the user password unchanged
                    bool success = security.ChangePassword(
                        originalOwnerPassword,   // current owner password
                        originalUserPassword,    // keep existing user password
                        newOwnerPassword);       // new owner password

                    if (!success)
                    {
                        Console.Error.WriteLine($"Failed to change password for: {inputPath}");
                    }
                }

                Console.WriteLine($"Owner password updated: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }
    }
}