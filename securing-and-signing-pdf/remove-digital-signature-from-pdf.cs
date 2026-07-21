using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "signed.pdf";
        const string outputPath = "unsigned.pdf";
        const string password = ""; // set owner password if the PDF is encrypted

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF; include password only when needed
            using (Document doc = string.IsNullOrEmpty(password)
                ? new Document(inputPath)
                : new Document(inputPath, password))
            {
                // Enable sanitization to strip digital signatures during save
                doc.EnableSignatureSanitization = true;

                // Save the sanitized PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Signature removed. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}