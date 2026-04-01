using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputDirectory = "input";
        const string standardUserPassword = "standardPassword";
        const string ownerPassword = "owner"; // original owner password for the PDFs

        // Ensure the input directory exists
        Directory.CreateDirectory(inputDirectory);

        foreach (string pdfFilePath in Directory.GetFiles(inputDirectory, "*.pdf"))
        {
            // Overwrite the same file after changing the password
            string outputPath = pdfFilePath;

            PdfFileSecurity fileSecurity = new PdfFileSecurity();
            fileSecurity.BindPdf(pdfFilePath);

            // Change the user password to the standardized value.
            // New owner password is null, so a random one will be generated.
            bool changed = fileSecurity.ChangePassword(ownerPassword, standardUserPassword, null);
            if (!changed)
            {
                Console.WriteLine($"Failed to change password for {Path.GetFileName(pdfFilePath)}: {fileSecurity.LastException?.Message}");
                continue;
            }

            fileSecurity.Save(outputPath);
            Console.WriteLine($"Password updated for {Path.GetFileName(pdfFilePath)}");
        }

        Console.WriteLine("All PDFs processed.");
    }
}