using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "signed.pdf";          // PDF containing a digital signature
        const string outputPath = "unsigned.pdf";        // Resulting PDF without the signature
        const string password   = "";                    // Owner password if the PDF is protected; leave empty otherwise

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF. If a password is required, pass it; otherwise use the overload without password.
            using (Document doc = string.IsNullOrEmpty(password)
                                   ? new Document(inputPath)
                                   : new Document(inputPath, password))
            {
                // Enable signature sanitization (default is true). This removes signature fields
                // when the document is saved, provided the current privileges allow it.
                doc.EnableSignatureSanitization = true;

                // Save the modified PDF. The signature fields are stripped from the output.
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