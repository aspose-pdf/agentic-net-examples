using System;
using System.IO;
using Aspose.Pdf.Facades;

namespace PdfPasswordUpdater
{
    class Program
    {
        // CSV format: InputFile,OwnerPassword,NewUserPassword,NewOwnerPassword,OutputFile
        static void Main()
        {
            const string csvPath = "files.csv";

            if (!File.Exists(csvPath))
            {
                Console.Error.WriteLine($"CSV file not found: {csvPath}");
                return;
            }

            var lines = File.ReadAllLines(csvPath);
            foreach (var rawLine in lines)
            {
                // Skip empty lines
                if (string.IsNullOrWhiteSpace(rawLine))
                    continue;

                var parts = rawLine.Split(',');
                if (parts.Length < 5)
                {
                    Console.Error.WriteLine($"Invalid CSV line (expected 5 columns): {rawLine}");
                    continue;
                }

                string inputFile      = parts[0].Trim();
                string ownerPassword  = parts[1].Trim();
                string newUserPass    = parts[2].Trim();
                string newOwnerPass   = parts[3].Trim();
                string outputFile     = parts[4].Trim();

                if (!File.Exists(inputFile))
                {
                    Console.Error.WriteLine($"Input PDF not found: {inputFile}");
                    continue;
                }

                try
                {
                    // Create the security facade and bind the source PDF
                    using (PdfFileSecurity security = new PdfFileSecurity())
                    {
                        security.BindPdf(inputFile);

                        // Change passwords, keeping existing privileges
                        bool changed = security.ChangePassword(ownerPassword, newUserPass, newOwnerPass);
                        if (!changed)
                        {
                            Console.Error.WriteLine($"Password change failed for: {inputFile}");
                            continue;
                        }

                        // Save the result to the specified output file
                        security.Save(outputFile);
                    }

                    Console.WriteLine($"Passwords updated: '{inputFile}' -> '{outputFile}'");
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error processing '{inputFile}': {ex.Message}");
                }
            }
        }
    }
}
