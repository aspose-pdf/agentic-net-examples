using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "encrypted_input.pdf";
        const string outputPath = "updated_owner.pdf";
        const string userPassword = "user123";      // existing user password
        const string newOwnerPassword = "newOwner456"; // new owner password

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the encrypted PDF using the current user password
            Document pdfDoc = new Document(inputPath, userPassword);

            // Preserve the existing permissions (including printing permission)
            // pdfDoc.Permissions returns an int, so cast it to the Permissions enum
            Permissions currentPermissions = (Permissions)pdfDoc.Permissions;

            // Re‑encrypt the document with the same user password, a new owner password,
            // and the previously‑retrieved permissions. The same encryption algorithm
            // (AES‑128) is used; adjust if your source PDF uses a different algorithm.
            pdfDoc.Encrypt(userPassword, newOwnerPassword, currentPermissions, CryptoAlgorithm.AESx128);

            // Save the updated PDF
            pdfDoc.Save(outputPath);
            Console.WriteLine($"Owner password updated and saved to '{outputPath}'.");
        }
        catch (InvalidPasswordException ex)
        {
            Console.Error.WriteLine($"Invalid password: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
