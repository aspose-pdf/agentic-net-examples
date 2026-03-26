using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string backupPath = "backup.pdf";
        const string outputPath = "flattened.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the original PDF
        using (Document document = new Document(inputPath))
        {
            // Create a backup copy before any flattening operation
            document.Save(backupPath);

            // Perform flattening (removes form fields and annotations)
            // Aspose.Pdf provides a parameter‑less Flatten method; advanced options are not required for basic flattening.
            document.Flatten();

            // Save the flattened result
            document.Save(outputPath);
        }

        Console.WriteLine($"Backup created at '{backupPath}' and flattened PDF saved as '{outputPath}'.");
    }
}
