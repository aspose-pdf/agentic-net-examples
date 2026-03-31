using System;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source PDF exists before attempting any operation.
        if (!System.IO.File.Exists(inputPath))
        {
            Console.WriteLine($"Input file '{inputPath}' not found.");
            return;
        }

        // Load the PDF document.
        using (var doc = new Document(inputPath))
        {
            // Define the permissions you want to allow.
            // PrintDocument = allow printing.
            // Do NOT include ExtractContent (copy) or any other permission.
            var permissions = Permissions.PrintDocument;

            // Owner password protects the permission settings; user password can be empty if you don't want to require a password to open.
            const string userPassword = "";          // no password needed to open the PDF
            const string ownerPassword = "ownerPwd"; // password required to change permissions later

            // Apply encryption with the selected permissions.
            doc.Encrypt(userPassword, ownerPassword, permissions, CryptoAlgorithm.AESx256);

            // Save the protected PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine("Privileges set successfully.");
    }
}
