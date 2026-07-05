using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: ChangePdfPasswords <csvFilePath>");
            return;
        }

        string csvPath = args[0];
        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"CSV file not found: {csvPath}");
            return;
        }

        try
        {
            string[] lines = File.ReadAllLines(csvPath);
            foreach (string rawLine in lines)
            {
                // Skip empty lines and possible header (detect by number of commas)
                if (string.IsNullOrWhiteSpace(rawLine)) continue;
                string line = rawLine.Trim();
                if (line.StartsWith("#")) continue; // allow comment lines

                string[] parts = line.Split(',');
                if (parts.Length < 5)
                {
                    Console.Error.WriteLine($"Invalid line (expected 5 columns): {line}");
                    continue;
                }

                string inputFile   = parts[0].Trim();
                string outputFile  = parts[1].Trim();
                string ownerPwd    = parts[2].Trim();
                string newUserPwd  = parts[3].Trim();
                string newOwnerPwd = parts[4].Trim();

                if (!File.Exists(inputFile))
                {
                    Console.Error.WriteLine($"Input PDF not found: {inputFile}");
                    continue;
                }

                try
                {
                    using (PdfFileSecurity security = new PdfFileSecurity())
                    {
                        // Bind the source PDF
                        security.BindPdf(inputFile);

                        // Change passwords, keeping existing privileges
                        bool changed = security.ChangePassword(ownerPwd, newUserPwd, newOwnerPwd);
                        if (!changed)
                        {
                            Console.Error.WriteLine($"Password change failed for: {inputFile}");
                            continue;
                        }

                        // Save the result to the specified output file
                        security.Save(outputFile);
                    }

                    Console.WriteLine($"Processed: {inputFile} -> {outputFile}");
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error processing '{inputFile}': {ex.Message}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read CSV file: {ex.Message}");
        }
    }
}