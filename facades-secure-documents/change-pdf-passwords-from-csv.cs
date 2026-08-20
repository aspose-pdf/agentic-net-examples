using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string csvPath = "files.csv";

        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"CSV file not found: {csvPath}");
            return;
        }

        // Expected CSV columns: InputPdf,OutputPdf,OldOwnerPassword,NewUserPassword,NewOwnerPassword
        foreach (var line in File.ReadLines(csvPath))
        {
            // Skip empty lines and header (assumes header starts with "InputPdf")
            if (string.IsNullOrWhiteSpace(line) || line.StartsWith("InputPdf", StringComparison.OrdinalIgnoreCase))
                continue;

            var parts = line.Split(',');

            if (parts.Length < 5)
            {
                Console.Error.WriteLine($"Invalid CSV line (expected 5 columns): {line}");
                continue;
            }

            string inputPdf   = parts[0].Trim();
            string outputPdf  = parts[1].Trim();
            string oldOwnerPw = parts[2].Trim();
            string newUserPw  = parts[3].Trim();
            string newOwnerPw = parts[4].Trim();

            if (!File.Exists(inputPdf))
            {
                Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
                continue;
            }

            try
            {
                // Create the PdfFileSecurity facade
                using (PdfFileSecurity security = new PdfFileSecurity())
                {
                    // Load the source PDF
                    security.BindPdf(inputPdf);

                    // Change passwords (keeps original security settings)
                    bool changed = security.ChangePassword(oldOwnerPw, newUserPw, newOwnerPw);

                    if (!changed)
                    {
                        Console.Error.WriteLine($"Password change failed for: {inputPdf}");
                        continue;
                    }

                    // Save the result to the output path
                    security.Save(outputPdf);
                }

                Console.WriteLine($"Processed: {inputPdf} -> {outputPdf}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPdf}': {ex.Message}");
            }
        }
    }
}