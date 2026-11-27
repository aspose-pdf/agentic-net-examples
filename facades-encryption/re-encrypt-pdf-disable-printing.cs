using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security; // Required for Permissions enum

class Program
{
    static void Main()
    {
        const string inputPath = "encrypted_input.pdf";
        const string outputPath = "resecured_output.pdf";
        const string currentUserPassword = "oldUserPass";
        const string newUserPassword = "newUserPass";
        const string newOwnerPassword = "newOwnerPass";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the encrypted PDF using the current user password.
            Document pdfDoc = new Document(inputPath, currentUserPassword);

            // Define permissions – we want to disable printing.
            // Using a cast to 0 gives a Permissions value with no flags set (i.e., all optional actions disabled).
            Permissions permissions = (Permissions)0;

            // Re‑encrypt the document with new user/owner passwords and the desired permissions.
            pdfDoc.Encrypt(newUserPassword, newOwnerPassword, permissions, CryptoAlgorithm.AESx256);

            // Save the updated PDF.
            pdfDoc.Save(outputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
