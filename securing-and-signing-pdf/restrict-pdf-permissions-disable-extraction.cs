using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "signed_input.pdf";
        const string outputPath = "privileged_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Define the permissions that are allowed.
        // Exclude ExtractContent and FillForm to disable those actions.
        Permissions allowedPermissions =
            Permissions.PrintDocument |
            Permissions.ModifyContent |
            Permissions.ModifyTextAnnotations |
            Permissions.AssembleDocument |
            Permissions.PrintingQuality;

        // Optional passwords – can be empty if no password protection is required.
        const string userPassword  = "";
        const string ownerPassword = "";

        using (Document doc = new Document(inputPath))
        {
            // Apply encryption with the allowed permissions.
            // CryptoAlgorithm.AESx256 is the recommended algorithm.
            doc.Encrypt(userPassword, ownerPassword, allowedPermissions, CryptoAlgorithm.AESx256);

            // Save the document. The encryption settings are persisted.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved with restricted privileges to '{outputPath}'.");
    }
}