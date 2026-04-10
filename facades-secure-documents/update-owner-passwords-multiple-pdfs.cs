using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF files
        string[] inputFiles = { "input1.pdf", "input2.pdf", "input3.pdf" };
        // Corresponding output files (owner password will be updated)
        string[] outputFiles = { "output1.pdf", "output2.pdf", "output3.pdf" };
        // Original owner passwords for each file
        string[] oldOwnerPasswords = { "oldOwner1", "oldOwner2", "oldOwner3" };
        // New owner passwords to set
        string[] newOwnerPasswords = { "newOwner1", "newOwner2", "newOwner3" };

        for (int i = 0; i < inputFiles.Length; i++)
        {
            if (!File.Exists(inputFiles[i]))
            {
                Console.Error.WriteLine($"File not found: {inputFiles[i]}");
                continue;
            }

            // PdfFileSecurity implements IDisposable, so use a using block
            using (PdfFileSecurity security = new PdfFileSecurity())
            {
                // Load the source PDF
                security.BindPdf(inputFiles[i]);

                // Change only the owner password.
                // Pass null for newUserPassword to keep the existing user password unchanged.
                bool success = security.ChangePassword(oldOwnerPasswords[i], null, newOwnerPasswords[i]);

                if (!success)
                {
                    Console.Error.WriteLine($"Failed to change owner password for {inputFiles[i]}");
                    continue;
                }

                // Save the updated PDF to the output path
                security.Save(outputFiles[i]);
            }

            Console.WriteLine($"Owner password updated: '{inputFiles[i]}' → '{outputFiles[i]}'");
        }
    }
}