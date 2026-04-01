using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // List of PDF files to process
        string[] inputFiles = new string[] { "input1.pdf", "input2.pdf", "input3.pdf" };
        // Original owner password (same for all files in this example)
        string originalOwnerPassword = "oldOwner";
        // New owner password to set
        string newOwnerPassword = "newOwner";

        foreach (string inputPath in inputFiles)
        {
            if (!File.Exists(inputPath))
            {
                Console.WriteLine($"File not found: {inputPath}");
                continue;
            }

            string outputFileName = Path.GetFileNameWithoutExtension(inputPath) + "_updated.pdf";

            PdfFileSecurity fileSecurity = new PdfFileSecurity();
            fileSecurity.BindPdf(inputPath);
            // Preserve existing user password by passing null for newUserPassword
            bool result = fileSecurity.ChangePassword(originalOwnerPassword, null, newOwnerPassword);
            if (result)
            {
                fileSecurity.Save(outputFileName);
                Console.WriteLine($"Owner password updated: {outputFileName}");
            }
            else
            {
                Console.WriteLine($"Failed to change password for {inputPath}");
            }
        }

        Console.WriteLine("Processing completed.");
    }
}