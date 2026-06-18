using System;
using System.IO;
using Aspose.Pdf.Facades;

namespace AsposePdfApi
{
    class Program
    {
        static void Main()
        {
            // Paths to the source (protected) PDF and the destination PDF
            const string inputPath = "input.pdf";
            const string outputPath = "output.pdf";

            // Existing owner password of the source PDF
            const string currentOwnerPassword = "oldOwnerPassword";

            // New owner password to set (user password and privileges remain unchanged)
            const string newOwnerPassword = "newOwnerPassword";

            // Verify that the input file exists
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"Input file not found: {inputPath}");
                return;
            }

            try
            {
                // Initialize PdfFileSecurity with source and destination files
                using (PdfFileSecurity security = new PdfFileSecurity(inputPath, outputPath))
                {
                    // Change only the owner password; pass null for newUserPassword to keep the existing user password unchanged.
                    bool success = security.ChangePassword(currentOwnerPassword, null, newOwnerPassword);

                    if (success)
                    {
                        Console.WriteLine($"Owner password updated successfully. Output saved to '{outputPath}'.");
                    }
                    else
                    {
                        Console.Error.WriteLine("Failed to change the owner password.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
