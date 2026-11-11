using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // List of PDF files to process
        var pdfFiles = new List<string>
        {
            "doc1.pdf",
            "doc2.pdf",
            "doc3.pdf"
        };

        // Mapping of each file to its current user password (empty string if none)
        var userPasswords = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "doc1.pdf", "userPass1" },
            { "doc2.pdf", "userPass2" },
            { "doc3.pdf", "" } // no user password
        };

        // New owner password to apply to all PDFs
        const string newOwnerPassword = "newOwner123";

        // Permissions to retain (adjust as needed)
        Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

        // Crypto algorithm to use (AES-256 is recommended)
        CryptoAlgorithm algorithm = CryptoAlgorithm.AESx256;

        foreach (string inputPath in pdfFiles)
        {
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                continue;
            }

            // Determine the user password for this file (default to empty string)
            userPasswords.TryGetValue(inputPath, out string userPassword);
            userPassword ??= string.Empty;

            // Define a temporary output path
            string tempOutput = Path.Combine(
                Path.GetDirectoryName(inputPath) ?? string.Empty,
                Path.GetFileNameWithoutExtension(inputPath) + "_temp.pdf");

            try
            {
                // Load the PDF with the existing user password (if any)
                using (Document pdfDoc = string.IsNullOrEmpty(userPassword)
                    ? new Document(inputPath)
                    : new Document(inputPath, userPassword))
                {
                    // Re‑encrypt the document with the same user password and the new owner password
                    pdfDoc.Encrypt(userPassword, newOwnerPassword, perms, algorithm);

                    // Save to a temporary file
                    pdfDoc.Save(tempOutput);
                }

                // Replace the original file with the newly encrypted version
                File.Replace(tempOutput, inputPath, null);
                Console.WriteLine($"Updated owner password for: {inputPath}");
            }
            catch (InvalidPasswordException)
            {
                Console.Error.WriteLine($"Invalid user password for file: {inputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing {inputPath}: {ex.Message}");
            }
        }
    }
}
