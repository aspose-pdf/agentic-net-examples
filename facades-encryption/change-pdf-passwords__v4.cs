using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "encrypted_input.pdf";
        const string outputPath = "reencrypted_output.pdf";
        const string ownerPassword = "owner123";
        const string newUserPassword = "newUser456";
        const string newOwnerPassword = "newOwner456";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the encrypted PDF using the owner password directly (no LoadOptions needed)
            using (Document doc = new Document(inputPath, ownerPassword))
            {
                // Change both user and owner passwords. The third argument is the permission (owner) password – null if not used.
                doc.ChangePasswords(newUserPassword, newOwnerPassword, null);
                // Save the PDF with the new passwords
                doc.Save(outputPath);
            }

            Console.WriteLine($"Passwords changed and saved to '{outputPath}'.");
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
