using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "betaGamma.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Forbid all permissions by specifying no permission flags
                Permissions perms = (Permissions)0; // equivalent to "no permissions"

                // Encrypt with user password "beta", owner password "gamma",
                // RC4x128 algorithm, and the defined permissions
                doc.Encrypt("beta", "gamma", perms, CryptoAlgorithm.RC4x128);

                // Save the encrypted PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Encrypted PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
