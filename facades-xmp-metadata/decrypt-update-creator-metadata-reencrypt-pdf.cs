using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Facades namespace is available for PDF security operations

class Program
{
    static void Main()
    {
        const string inputPath  = "protected.pdf";
        const string outputPath = "protected_updated.pdf";
        const string userPassword  = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Open the encrypted PDF using the known user password
            using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath, userPassword))
            {
                // Decrypt the document (removes existing encryption)
                doc.Decrypt();

                // Update the Creator metadata field (CreatorTool does not exist; use Creator)
                doc.Info.Creator = "MyCreatorTool";

                // Re‑encrypt the document with desired permissions and AES‑256 algorithm
                Aspose.Pdf.Permissions perms = Aspose.Pdf.Permissions.PrintDocument |
                                               Aspose.Pdf.Permissions.ExtractContent;
                doc.Encrypt(userPassword, ownerPassword, perms, Aspose.Pdf.CryptoAlgorithm.AESx256);

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
