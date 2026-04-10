using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Path to the CSV file containing the list of PDFs to process.
        // Expected CSV columns (comma‑separated):
        // InputFile,OutputFile,OldOwnerPassword,NewUserPassword,NewOwnerPassword
        const string csvPath = "files.csv";

        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"CSV file not found: {csvPath}");
            return;
        }

        int processed = 0;
        int succeeded = 0;
        int failed = 0;

        using (StreamReader reader = new StreamReader(csvPath))
        {
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                    continue; // skip empty lines

                // Split the line into fields; trim whitespace.
                string[] fields = line.Split(',');
                if (fields.Length < 5)
                {
                    Console.Error.WriteLine($"Invalid CSV line (expected 5 fields): {line}");
                    failed++;
                    continue;
                }

                string inputFile   = fields[0].Trim();
                string outputFile  = fields[1].Trim();
                string oldOwnerPwd = fields[2].Trim();
                string newUserPwd  = fields[3].Trim();
                string newOwnerPwd = fields[4].Trim();

                if (!File.Exists(inputFile))
                {
                    Console.Error.WriteLine($"Input PDF not found: {inputFile}");
                    failed++;
                    continue;
                }

                try
                {
                    // PdfFileSecurity constructor binds the input and output files.
                    PdfFileSecurity security = new PdfFileSecurity(inputFile, outputFile);

                    // ChangePassword keeps the original security settings and updates passwords.
                    bool result = security.ChangePassword(oldOwnerPwd, newUserPwd, newOwnerPwd);

                    // Close the facade to release resources.
                    security.Close();

                    if (result)
                    {
                        Console.WriteLine($"Success: '{inputFile}' -> '{outputFile}'");
                        succeeded++;
                    }
                    else
                    {
                        Console.Error.WriteLine($"Failed to change password for: {inputFile}");
                        failed++;
                    }
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error processing '{inputFile}': {ex.Message}");
                    failed++;
                }

                processed++;
            }
        }

        Console.WriteLine($"Processing complete. Total: {processed}, Succeeded: {succeeded}, Failed: {failed}");
    }
}