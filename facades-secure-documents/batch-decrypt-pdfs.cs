using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        string configPath = "passwords.txt";
        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine("Configuration file not found: " + configPath);
            return;
        }

        string[] lines = File.ReadAllLines(configPath);
        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            string[] parts = line.Split(new char[] { ',' }, 2);
            if (parts.Length != 2)
                continue;

            string inputFile = parts[0].Trim();
            string ownerPassword = parts[1].Trim();

            if (!File.Exists(inputFile))
            {
                Console.Error.WriteLine("Input file not found: " + inputFile);
                continue;
            }

            string outputFile = Path.GetFileNameWithoutExtension(inputFile) + "_decrypted.pdf";

            using (PdfFileSecurity security = new PdfFileSecurity())
            {
                security.BindPdf(inputFile);
                bool result = security.DecryptFile(ownerPassword);
                if (result)
                {
                    security.Save(outputFile);
                    Console.WriteLine("Decrypted: " + outputFile);
                }
                else
                {
                    Console.Error.WriteLine("Failed to decrypt: " + inputFile);
                }
            }
        }

        Console.WriteLine("Batch decryption completed.");
    }
}