using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // List of PDFs to process: input path, output path, and the new owner password.
        var files = new[]
        {
            new { Input = "input1.pdf", Output = "output1.pdf", NewOwnerPassword = "newOwner1" },
            new { Input = "input2.pdf", Output = "output2.pdf", NewOwnerPassword = "newOwner2" }
            // Add more entries as needed.
        };

        // Original owner password that protects all source PDFs.
        const string originalOwnerPassword = "oldOwnerPassword";

        foreach (var file in files)
        {
            if (!File.Exists(file.Input))
            {
                Console.Error.WriteLine($"Input file not found: {file.Input}");
                continue;
            }

            // Initialize the facade with the source and destination files.
            using (PdfFileSecurity security = new PdfFileSecurity(file.Input, file.Output))
            {
                // Change only the owner password.
                // Passing null for newUserPassword preserves the existing user password.
                bool success = security.ChangePassword(originalOwnerPassword, null, file.NewOwnerPassword);

                if (success)
                {
                    Console.WriteLine($"Owner password updated for '{file.Input}' -> '{file.Output}'.");
                }
                else
                {
                    Console.Error.WriteLine($"Failed to update owner password for '{file.Input}'.");
                }
            }
        }
    }
}