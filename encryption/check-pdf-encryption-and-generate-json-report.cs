using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "encrypted.pdf";
        const string reportPath = "report.json";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        bool isEncrypted = false;
        string fileName = Path.GetFileName(inputPath);

        try
        {
            // Attempt to open the PDF without a password.
            // If it succeeds, we can read the IsEncrypted flag.
            using (Document doc = new Document(inputPath))
            {
                isEncrypted = doc.IsEncrypted;
            }
        }
        catch (InvalidPasswordException)
        {
            // The PDF is encrypted and a password was not supplied.
            isEncrypted = true;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
            return;
        }

        // Build a simple report object.
        var report = new
        {
            FileName    = fileName,
            IsEncrypted = isEncrypted
        };

        // Serialize the report to formatted JSON.
        string json = JsonSerializer.Serialize(report, new JsonSerializerOptions { WriteIndented = true });

        // Write the JSON report to disk.
        File.WriteAllText(reportPath, json);
        Console.WriteLine($"Encryption report saved to '{reportPath}'.");
    }
}