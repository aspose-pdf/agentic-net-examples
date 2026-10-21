using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security;

namespace AsposePdfDemo
{
    class Program
    {
        static void Main()
        {
            const string inputPath = "input.pdf";
            const string outputPath = "output_protected.pdf";

            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"Input file not found: {inputPath}");
                return;
            }

            try
            {
                // Load the existing PDF document
                Document pdfDoc = new Document(inputPath);

                // Allow copying (extracting) text but do NOT allow form editing or annotation changes.
                // The ExtractContent flag enables text copying. No other flags are set, so form fields
                // and annotations remain read‑only.
                Permissions permissions = Permissions.ExtractContent;

                // Apply encryption with an empty user password and a non‑empty owner password.
                // The owner password enforces the permissions.
                pdfDoc.Encrypt("", "ownerPassword", permissions, CryptoAlgorithm.AESx128);

                // Save the PDF with the new permissions
                pdfDoc.Save(outputPath);

                Console.WriteLine($"Permissions set and saved to '{outputPath}'.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
