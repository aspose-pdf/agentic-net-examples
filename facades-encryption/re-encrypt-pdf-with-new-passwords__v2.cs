using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security;

class Program
{
    static void Main()
    {
        const string inputPath = "encrypted_input.pdf";
        const string outputPath = "reprotected_output.pdf";

        // Current password of the encrypted PDF (owner password is not required for opening)
        const string currentUserPassword = "oldUser";

        // New passwords to set
        const string newUserPassword = "newUser123";
        const string newOwnerPassword = "newOwner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the encrypted PDF using the existing user password.
            // Use the Document(string, string) overload – no LoadOptions instance is needed.
            Document pdfDoc = new Document(inputPath, currentUserPassword);

            // Re‑encrypt with the new passwords and enable printing permission.
            // Permissions enum replaces the obsolete DocumentPrivilege enum.
            pdfDoc.Encrypt(
                newUserPassword,
                newOwnerPassword,
                Permissions.PrintDocument,
                CryptoAlgorithm.AESx128);

            // Save the newly protected PDF.
            pdfDoc.Save(outputPath);
            Console.WriteLine($"PDF saved with new passwords and printing enabled: {outputPath}");
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
