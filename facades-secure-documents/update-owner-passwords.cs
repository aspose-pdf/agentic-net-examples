using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // List of PDF files to process
        string[] pdfFiles = new string[] { "doc1.pdf", "doc2.pdf", "doc3.pdf" };
        // Original owner password (same for all files in this example)
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

            string outputPath = Path.GetFileNameWithoutExtension(inputPath) + "_updated.pdf";

            // PdfFileSecurity works with input and output file names
            PdfFileSecurity security = new PdfFileSecurity(inputPath, outputPath);
            try
            {
                // Pass null for newUserPassword to keep existing user password unchanged
                bool success = security.ChangePassword(originalOwnerPassword, null, newOwnerPassword);
                if (success)
                {
                    Console.WriteLine($"Owner password updated: {outputPath}");
                }
                else
                {
                    Console.Error.WriteLine($"Failed to change password for {inputPath}");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing {inputPath}: {ex.Message}");
            }
        }
    }
}