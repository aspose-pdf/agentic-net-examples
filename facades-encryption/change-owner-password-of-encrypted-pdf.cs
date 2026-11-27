using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath        = "encrypted_input.pdf";
        const string outputPath       = "updated_owner.pdf";
        const string userPassword     = "user123";
        const string newOwnerPassword = "newOwner456";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the encrypted PDF using the existing user password
            using (Document doc = new Document(inputPath, userPassword))
            {
                // Bind the document to the PdfFileSecurity facade
                PdfFileSecurity security = new PdfFileSecurity();
                security.BindPdf(doc);

                // Change only the owner password; keep the user password unchanged.
                // ChangePassword expects three arguments: current user password, new user password, new owner password.
                // Pass the same value for current and new user password to leave it unchanged.
                security.ChangePassword(userPassword, userPassword, newOwnerPassword);

                // Save the updated PDF
                doc.Save(outputPath);
            }

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
