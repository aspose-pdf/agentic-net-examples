using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath      = "protected.pdf";          // existing PDF encrypted with an owner password
        const string ownerPassword  = "oldOwnerPass";           // current owner password
        const string newUserPass    = "newUserPass";            // desired new user password
        const string newOwnerPass   = "newOwnerPass";           // desired new owner password
        const string outputPath     = "updated_protected.pdf";  // file to save the re‑encrypted PDF

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Open the PDF using the current owner password.
            // The password argument works for both user and owner passwords.
            using (Document doc = new Document(inputPath, ownerPassword))
            {
                // Change the user and owner passwords.
                // The third argument is the current (old) owner password.
                doc.ChangePasswords(newUserPass, newOwnerPass, ownerPassword);

                // Save the updated PDF. No special SaveOptions are required for PDF output.
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF passwords updated and saved to '{outputPath}'.");
        }
        catch (InvalidPasswordException ex)
        {
            Console.Error.WriteLine($"Password error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
