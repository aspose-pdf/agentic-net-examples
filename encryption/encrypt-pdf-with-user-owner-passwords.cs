using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";      // source PDF
        const string outputPath = "alphaOmega.pdf"; // encrypted output

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Define the permission set: only allow screen readers (accessibility)
                Permissions perms = Permissions.ExtractContentWithDisabilities;

                // Encrypt with user password "alpha", owner password "omega",
                // AES 128‑bit algorithm, and the defined permissions
                doc.Encrypt("alpha", "omega", perms, CryptoAlgorithm.AESx128);

                // Save the encrypted document
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
