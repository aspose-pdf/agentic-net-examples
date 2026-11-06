using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "flattened.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF to inspect for digital signatures
        using (Document doc = new Document(inputPath))
        {
            // Determine if any signature fields exist (signed or unsigned)
            bool hasSignatures = doc?.Form?.SignaturesExist ?? false;
            // Alternative approach (if SignaturesExist is unavailable):
            // bool hasSignatures = doc?.Form?.Fields?.OfType<SignatureField>().Any() ?? false;

            if (hasSignatures)
            {
                // Preserve signature validity by skipping flattening
                Console.WriteLine("PDF contains digital signatures. Skipping flattening to preserve signatures.");
                // Optionally copy the original file to the output location
                File.Copy(inputPath, outputPath, true);
            }
            else
            {
                // Safe to flatten when no signatures are present
                doc.Form.Flatten();
                doc.Save(outputPath);
                Console.WriteLine($"Flattening completed. Output saved to '{outputPath}'.");
            }
        }
    }
}
