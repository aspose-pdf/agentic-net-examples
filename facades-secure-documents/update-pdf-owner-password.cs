using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security;

class Program
{
    static void Main()
    {
        // List of PDF files to process
        string[] pdfFiles = { "file1.pdf", "file2.pdf", "file3.pdf" };

        // New owner password to apply to all PDFs
        const string newOwnerPassword = "NewOwnerPass123";

        // Mapping of each PDF to its existing user password
        var userPasswords = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "file1.pdf", "UserPass1" },
            { "file2.pdf", "UserPass2" },
            { "file3.pdf", "UserPass3" }
        };

        foreach (string pdfPath in pdfFiles)
        {
            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"File not found: {pdfPath}");
                continue;
            }

            // Retrieve the original user password; assume empty string if not provided
            string userPassword = userPasswords.TryGetValue(pdfPath, out string pwd) ? pwd : string.Empty;

            // Load the PDF document using Document (the recommended API)
            Document pdfDoc = new Document(pdfPath);

            // Combine the permission flags that are available in the current Aspose.Pdf version.
            // Flags that are not present in older versions (ModifyAnnotations, FillForms,
            // AccessibilitySupport, PrintHighResolution) have been removed.
            Permissions perms =
                Permissions.PrintDocument |
                Permissions.ModifyContent |
                Permissions.ExtractContent |
                Permissions.AssembleDocument;

            // Apply the new owner password while keeping the original user password.
            pdfDoc.Encrypt(userPassword, newOwnerPassword, perms, CryptoAlgorithm.AESx256);

            // Save the updated PDF (writes to a new file to avoid overwriting the source)
            string outputPath = Path.Combine(
                Path.GetDirectoryName(pdfPath) ?? string.Empty,
                Path.GetFileNameWithoutExtension(pdfPath) + "_updated.pdf");

            pdfDoc.Save(outputPath);

            Console.WriteLine($"Updated owner password for '{pdfPath}' → '{outputPath}'");
        }
    }
}
