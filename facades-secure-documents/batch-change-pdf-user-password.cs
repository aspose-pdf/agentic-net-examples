using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Resolve the folder path in a platform‑independent way.
        // This builds an absolute path based on the location of the executable
        // and a sub‑folder named "PdfFolder" that should exist next to the binary.
        string folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PdfFolder");
        const string newUserPassword = "StandardPassword"; // standardized user password
        const string ownerPassword = ""; // original owner password (empty if none)

        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder not found: {folderPath}");
            return;
        }

        string[] pdfFiles = Directory.GetFiles(folderPath, "*.pdf");
        foreach (string inputFile in pdfFiles)
        {
            // Overwrite the original file after changing the password
            string outputFile = inputFile;

            using (PdfFileSecurity security = new PdfFileSecurity())
            {
                // Bind the existing PDF file to the facade
                security.BindPdf(inputFile);

                // Change the user password; keep the owner password unchanged (null generates a random one)
                bool changed = security.ChangePassword(ownerPassword, newUserPassword, null);
                if (!changed)
                {
                    Console.Error.WriteLine($"Failed to change password for {Path.GetFileName(inputFile)}: {security.LastException?.Message}");
                    continue;
                }

                // Save the updated PDF (overwrites the original file)
                security.Save(outputFile);
            }

            Console.WriteLine($"Password updated for {Path.GetFileName(inputFile)}");
        }
    }
}
