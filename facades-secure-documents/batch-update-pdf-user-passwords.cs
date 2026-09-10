using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing PDF files
        const string inputFolder = @"C:\PdfFolder";
        // Standardized user password to apply to every PDF
        const string newUserPassword = "StandardUserPass";
        // Owner password of the source PDFs (assumed known)
        const string ownerPassword = "owner";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Folder not found: {inputFolder}");
            return;
        }

        // Process each PDF file in the folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            // Build output path (overwrite the original file)
            string outputPath = inputPath; // same file, will be overwritten

            // Initialize PdfFileSecurity with input and output file names
            PdfFileSecurity fileSecurity = new PdfFileSecurity(inputPath, outputPath);

            // Change the user password; new owner password is null (randomly generated)
            bool success = fileSecurity.TryChangePassword(ownerPassword, newUserPassword, null);

            if (success)
                Console.WriteLine($"Password updated for: {Path.GetFileName(inputPath)}");
            else
                Console.Error.WriteLine($"Failed to update password for: {Path.GetFileName(inputPath)}");
        }
    }
}