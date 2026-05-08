using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    // Standardized user password to apply to all PDFs in the folder
    private const string StandardUserPassword = "StandardPassword123";

    static void Main()
    {
        // Folder containing the PDF files
        const string folderPath = @"C:\PdfFolder";

        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder not found: {folderPath}");
            return;
        }

        // Process each PDF file in the folder
        foreach (string inputFile in Directory.GetFiles(folderPath, "*.pdf"))
        {
            // Create a temporary output file name
            string tempOutputFile = Path.Combine(folderPath, Guid.NewGuid().ToString() + ".pdf");

            try
            {
                // Initialize PdfFileSecurity with input and temporary output files
                using (PdfFileSecurity fileSecurity = new PdfFileSecurity(inputFile, tempOutputFile))
                {
                    // Change the user password.
                    // Owner password is assumed to be empty (no owner password set).
                    // New owner password is left null so a random one will be generated.
                    bool changed = fileSecurity.ChangePassword(
                        ownerPassword: "",               // original owner password (empty)
                        newUserPassword: StandardUserPassword,
                        newOwnerPassword: null);        // generate random owner password

                    if (!changed)
                    {
                        Console.Error.WriteLine($"Failed to change password for: {inputFile}");
                        // Clean up the temp file if it was created
                        if (File.Exists(tempOutputFile))
                            File.Delete(tempOutputFile);
                        continue;
                    }
                }

                // Replace the original file with the newly secured file
                File.Copy(tempOutputFile, inputFile, overwrite: true);
                File.Delete(tempOutputFile);

                Console.WriteLine($"Password updated for: {Path.GetFileName(inputFile)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputFile}': {ex.Message}");
                // Ensure temporary file is removed on error
                if (File.Exists(tempOutputFile))
                    File.Delete(tempOutputFile);
            }
        }
    }
}