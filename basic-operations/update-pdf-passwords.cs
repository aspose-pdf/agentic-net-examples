using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "protected.pdf";
        const string outputPath = "updated.pdf";

        // Existing passwords: one to open the file, the owner password required for changing passwords
        const string openPassword = "currentPassword";   // user or owner password that opens the file
        const string currentOwnerPassword = "ownerPass"; // must be the owner password

        // New passwords to set
        const string newUserPassword = "newUserPass";
        const string newOwnerPassword = "newOwnerPass";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Open the encrypted PDF using the existing password
            using (Document doc = new Document(inputPath, openPassword))
            {
                // Change passwords – requires the current owner password
                doc.ChangePasswords(currentOwnerPassword, newUserPassword, newOwnerPassword);

                // Save the PDF with the updated credentials
                doc.Save(outputPath);
            }

            Console.WriteLine($"Password‑updated PDF saved to '{outputPath}'.");
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