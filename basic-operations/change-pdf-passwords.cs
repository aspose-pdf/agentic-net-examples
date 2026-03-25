using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "protected.pdf";
        const string outputPath = "updated.pdf";
        const string ownerPassword = "owner123";
        const string newUserPassword = "newUser456";
        const string newOwnerPassword = "newOwner789";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPath, ownerPassword))
            {
                // Change the user and owner passwords
                doc.ChangePasswords(ownerPassword, newUserPassword, newOwnerPassword);

                // Save the PDF with the new credentials
                doc.Save(outputPath);
            }

            Console.WriteLine($"Passwords updated and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
