using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string csvFile = "files.csv";
        if (!File.Exists(csvFile))
        {
            Console.Error.WriteLine($"CSV file not found: {csvFile}");
            return;
        }

        string[] lines = File.ReadAllLines(csvFile);
        foreach (string rawLine in lines)
        {
            if (string.IsNullOrWhiteSpace(rawLine))
                continue;

            string[] parts = rawLine.Split(',');
            if (parts.Length < 4)
            {
                Console.Error.WriteLine($"Invalid line (expected 4 columns): {rawLine}");
                continue;
            }

            string inputPath = parts[0].Trim();
            string ownerPassword = parts[1].Trim();
            string newUserPassword = parts[2].Trim();
            string newOwnerPassword = parts[3].Trim();

            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"PDF file not found: {inputPath}");
                continue;
            }

            string outputPath = Path.GetFileNameWithoutExtension(inputPath) + "_changed.pdf";

            PdfFileSecurity fileSecurity = new PdfFileSecurity();
            fileSecurity.BindPdf(inputPath);
            bool changed = fileSecurity.ChangePassword(ownerPassword, newUserPassword, newOwnerPassword);
            if (!changed)
            {
                Console.Error.WriteLine($"Failed to change password for: {inputPath}");
                continue;
            }
            fileSecurity.Save(outputPath);
            Console.WriteLine($"Password updated: {outputPath}");
        }
    }
}