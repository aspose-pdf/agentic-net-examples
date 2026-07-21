using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        // Folder containing encrypted PDFs
        const string inputFolder = "EncryptedPdfs";
        // Folder where decrypted PDFs will be written
        const string outputFolder = "DecryptedPdfs";
        // Simple lookup file: each line "FileName.pdf|Password"
        const string passwordLookupFile = "passwords.txt";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure output folder exists
        Directory.CreateDirectory(outputFolder);

        // Load the filename‑to‑password map
        Dictionary<string, string> passwordMap = LoadPasswordMap(passwordLookupFile);

        // Process each PDF in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileName(pdfPath);

            // Find the password for this file
            if (!passwordMap.TryGetValue(fileName, out string password))
            {
                Console.WriteLine($"No password entry for '{fileName}'. Skipping.");
                continue;
            }

            try
            {
                // Open the encrypted document using the supplied password
                using (Document doc = new Document(pdfPath, password))
                {
                    // Decrypt the document (no parameters)
                    doc.Decrypt();

                    // Save the decrypted version to the output folder
                    string outPath = Path.Combine(outputFolder, fileName);
                    doc.Save(outPath);
                }

                Console.WriteLine($"Decrypted '{fileName}' → '{outputFolder}'.");
            }
            catch (InvalidPasswordException)
            {
                Console.Error.WriteLine($"Invalid password for '{fileName}'.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{fileName}': {ex.Message}");
            }
        }
    }

    // Reads a simple "filename|password" text file into a dictionary
    static Dictionary<string, string> LoadPasswordMap(string filePath)
    {
        var map = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        if (!File.Exists(filePath))
        {
            Console.Error.WriteLine($"Password lookup file not found: {filePath}");
            return map;
        }

        foreach (string line in File.ReadAllLines(filePath))
        {
            // Skip empty lines or lines without the delimiter
            if (string.IsNullOrWhiteSpace(line) || !line.Contains("|"))
                continue;

            string[] parts = line.Split(new[] { '|' }, 2);
            string name = parts[0].Trim();
            string pwd  = parts[1].Trim();

            if (!string.IsNullOrEmpty(name))
                map[name] = pwd;
        }

        return map;
    }
}