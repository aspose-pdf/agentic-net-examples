using System;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "protected.pdf";
        const string outputPath = "updated_owner.pdf";
        const string currentOwnerPassword = "oldOwner";
        const string newOwnerPassword = "newOwner";

        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the encrypted PDF using the existing owner password
            using (Document document = new Document(inputPath, currentOwnerPassword))
            {
                // Change only the owner password; pass null for new user password to keep it unchanged
                document.ChangePasswords(currentOwnerPassword, null, newOwnerPassword);
                document.Save(outputPath);
            }

            Console.WriteLine($"Owner password updated. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
