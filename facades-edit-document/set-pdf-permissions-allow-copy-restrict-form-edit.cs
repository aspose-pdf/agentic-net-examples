using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_permissions.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document.
        Document doc = new Document(inputPath);

        // Permissions: allow extracting (copying) text, but do NOT allow form filling, annotation editing, etc.
        // By specifying only Permissions.ExtractContent we implicitly deny all other mutable actions.
        Permissions perms = Permissions.ExtractContent;

        // Apply the permissions by encrypting the document. An empty user password allows opening the PDF
        // without a password, while a non‑empty owner password enforces the permission set.
        // AES‑256 is used for strong encryption.
        const string userPassword = "";          // no password required to open the PDF
        const string ownerPassword = "ownerPwd"; // owner password that protects the permissions
        doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

        // Save the modified PDF.
        doc.Save(outputPath);
        Console.WriteLine($"Permissions applied successfully. Output saved to '{outputPath}'.");
    }
}
