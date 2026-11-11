using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Facades namespace is available; not required for encryption but included per requirement

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_privileged.pdf";
        const string userPassword  = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF inside a using block for deterministic disposal
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath))
        {
            // Permissions are a flag enum. We allow form filling and nothing else.
            // The permission for form filling is FillForm (singular) in this version of Aspose.Pdf.
            // Document assembly is controlled by AssembleDocument; we simply do not include it.
            Aspose.Pdf.Permissions perms = Aspose.Pdf.Permissions.FillForm;

            // Encrypt the document with AES‑256 and the specified permissions
            doc.Encrypt(userPassword, ownerPassword, perms, Aspose.Pdf.CryptoAlgorithm.AESx256);

            // Save the protected PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with form‑filling enabled and document assembly disabled: {outputPath}");
    }
}