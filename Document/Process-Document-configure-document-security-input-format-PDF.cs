using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security;

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPath = "input.pdf";
        const string outputPath = "secured.pdf";

        // Passwords for encryption
        const string userPassword = "user";
        const string ownerPassword = "owner";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        try
        {
            // Load the existing PDF document
            Document pdfDocument = new Document(inputPath);

            // Define the permissions to grant to the user
            Permissions permissions = Permissions.PrintDocument |
                                      Permissions.ModifyContent |
                                      Permissions.ExtractContent;

            // Apply encryption with the specified passwords and permissions
            pdfDocument.Encrypt(userPassword, ownerPassword, permissions, CryptoAlgorithm.RC4x128);

            // Save the secured PDF (using the provided document-save rule)
            pdfDocument.Save(outputPath);

            Console.WriteLine($"Document secured and saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}