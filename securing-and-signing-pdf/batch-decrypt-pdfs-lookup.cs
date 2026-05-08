using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;

class BatchDecrypt
{
    static void Main()
    {
        // Folder containing encrypted PDFs
        const string inputFolder = "EncryptedPdfs";
        // Folder where decrypted PDFs will be written
        const string outputFolder = "DecryptedPdfs";
        // CSV file with two columns: filename,password
        const string lookupPath = "passwords.csv";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure output folder exists
        Directory.CreateDirectory(outputFolder);

        // Load the lookup table into a dictionary for fast access
        var passwordMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        if (!File.Exists(lookupPath))
        {
            Console.Error.WriteLine($"Lookup file not found: {lookupPath}");
            return;
        }

        foreach (var line in File.ReadAllLines(lookupPath))
        {
            if (string.IsNullOrWhiteSpace(line)) continue; // skip empty lines
            var parts = line.Split(new[] { ',' }, 2);
            if (parts.Length == 2)
            {
                string fileName = parts[0].Trim();
                string pwd = parts[1].Trim();
                passwordMap[fileName] = pwd;
            }
        }

        // Process each PDF in the input folder
        foreach (var filePath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileName(filePath);

            // Find the password for this file
            if (!passwordMap.TryGetValue(fileName, out string password))
            {
                Console.WriteLine($"No password entry for {fileName}, skipping.");
                continue;
            }

            try
            {
                // Open the encrypted PDF using the password
                using (Document doc = new Document(filePath, password))
                {
                    // Decrypt the document (no parameters)
                    doc.Decrypt();

                    // Save the decrypted version to the output folder
                    string outPath = Path.Combine(outputFolder, fileName);
                    doc.Save(outPath);
                }

                Console.WriteLine($"Decrypted: {fileName}");
            }
            catch (InvalidPasswordException)
            {
                Console.Error.WriteLine($"Invalid password for {fileName}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing {fileName}: {ex.Message}");
            }
        }
    }
}