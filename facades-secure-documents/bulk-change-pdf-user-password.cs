using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing the PDF files
        string folderPath = @"C:\PdfFolder";

        // The standardized user password to apply to every PDF
        const string standardizedUserPassword = "StandardPassword123";

        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder not found: {folderPath}");
            return;
        }

        // Process each PDF file in the folder
        foreach (string inputFile in Directory.GetFiles(folderPath, "*.pdf"))
        {
            // Create an output file name (you can overwrite the original if desired)
            string outputFile = Path.Combine(
                folderPath,
                Path.GetFileNameWithoutExtension(inputFile) + "_updated.pdf");

            try
            {
                // PdfFileSecurity handles both opening the source PDF and writing the result
                using (PdfFileSecurity security = new PdfFileSecurity(inputFile, outputFile))
                {
                    // Assume the original owner password is empty.
                    // Pass null for the new owner password so Aspose generates a random one.
                    // TryChangePassword returns false if the operation fails (e.g., wrong owner password),
                    // but it does not throw an exception.
                    security.TryChangePassword(string.Empty, standardizedUserPassword, null);
                }

                Console.WriteLine($"Processed: {inputFile} → {outputFile}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputFile}': {ex.Message}");
            }
        }
    }
}