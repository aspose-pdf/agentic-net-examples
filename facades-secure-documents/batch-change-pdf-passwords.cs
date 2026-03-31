using System;
using System.IO;
using Aspose.Pdf.Facades;

namespace ChangePdfPasswords
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.Error.WriteLine("Usage: ChangePdfPasswords <csv-file-path>");
                return;
            }

            string csvPath = args[0];
            if (!File.Exists(csvPath))
            {
                Console.Error.WriteLine($"CSV file not found: {csvPath}");
                return;
            }

            string[] csvLines = File.ReadAllLines(csvPath);
            // Expect header: InputPath,OwnerPassword,NewUserPassword,NewOwnerPassword
            for (int i = 1; i < csvLines.Length; i++)
            {
                string line = csvLines[i];
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                string[] fields = line.Split(',');
                if (fields.Length < 4)
                {
                    Console.Error.WriteLine($"Invalid CSV line {i + 1}: {line}");
                    continue;
                }

                string inputPath = fields[0].Trim();
                string ownerPassword = fields[1].Trim();
                string newUserPassword = fields[2].Trim();
                string newOwnerPassword = fields[3].Trim();

                if (!File.Exists(inputPath))
                {
                    Console.Error.WriteLine($"Input PDF not found: {inputPath}");
                    continue;
                }

                string outputFileName = Path.GetFileNameWithoutExtension(inputPath) + "_secured.pdf";

                PdfFileSecurity fileSecurity = new PdfFileSecurity(inputPath, outputFileName);
                bool changed = fileSecurity.ChangePassword(ownerPassword, newUserPassword, newOwnerPassword);
                if (changed)
                {
                    Console.WriteLine($"Password changed successfully: {outputFileName}");
                }
                else
                {
                    Console.Error.WriteLine($"Failed to change password for: {inputPath}");
                }
            }
        }
    }
}