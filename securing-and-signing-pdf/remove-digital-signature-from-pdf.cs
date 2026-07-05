using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "signed.pdf";
        const string outputPath = "unsigned.pdf";
        const string password = ""; // set if the PDF is password‑protected

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF; supply the password only when needed
            using (Document doc = string.IsNullOrEmpty(password)
                                 ? new Document(inputPath)
                                 : new Document(inputPath, password))
            {
                // EnableSignatureSanitization is true by default.
                // When true, saving the document strips removable digital signatures.
                doc.EnableSignatureSanitization = true;

                // Save the modified PDF – signatures are removed if the document permits it.
                doc.Save(outputPath);
            }

            Console.WriteLine($"Unsigned PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}