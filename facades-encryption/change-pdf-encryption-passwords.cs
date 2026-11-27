using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath   = "encrypted_input.pdf";
        const string outputPath  = "encrypted_output.pdf";
        const string oldOwnerPwd = "oldUserPassword"; // current owner password (used for decryption)
        const string newUserPwd  = "newUserPassword";
        const string newOwnerPwd = "newOwnerPassword";

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Check whether the PDF is encrypted
            var fileInfo = new PdfFileInfo(inputPath);
            if (!fileInfo.IsEncrypted)
            {
                Console.WriteLine("The PDF is not encrypted. No password change needed.");
                return;
            }

            // Load the encrypted PDF using the current owner password
            var doc = new Document(inputPath, oldOwnerPwd);

            // Define full privileges by combining all Permissions flags
            Permissions fullPermissions = Permissions.PrintDocument |
                                            Permissions.ModifyContent |
                                            Permissions.ExtractContent |
                                            Permissions.ModifyTextAnnotations |
                                            Permissions.FillForm |
                                            Permissions.ExtractContentWithDisabilities |
                                            Permissions.AssembleDocument |
                                            Permissions.PrintingQuality;

            // Re‑encrypt with the new user and owner passwords, preserving full privileges and using AES‑256
            doc.Encrypt(newUserPwd, newOwnerPwd, fullPermissions, CryptoAlgorithm.AESx256);

            // Save the newly encrypted PDF
            doc.Save(outputPath);

            Console.WriteLine($"Password changed successfully. Saved to '{outputPath}'.");
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
