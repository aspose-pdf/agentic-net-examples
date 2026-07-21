using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing the PDF files
        const string folderPath = @"C:\PdfFolder";

        // The standardized user password to apply to every PDF
        const string newUserPassword = "StandardPassword";

        // Assume the PDFs have no owner password (empty string). Adjust if needed.
        const string ownerPassword = "";

        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder not found: {folderPath}");
            return;
        }

        // Iterate over all PDF files in the folder
        foreach (string inputFile in Directory.GetFiles(folderPath, "*.pdf"))
        {
            // Create an output file name (you can overwrite the original if desired)
            string outputFile = Path.Combine(
                folderPath,
                Path.GetFileNameWithoutExtension(inputFile) + "_updated.pdf");

            try
            {
                // Initialize the PdfFileSecurity facade with input and output paths
                PdfFileSecurity security = new PdfFileSecurity(inputFile, outputFile);

                // Change the user password; keep the owner password unchanged (empty),
                // let Aspose generate a random owner password by passing null.
                bool changed = security.ChangePassword(ownerPassword, newUserPassword, null);

                if (!changed)
                {
                    Console.Error.WriteLine($"Failed to change password for: {inputFile}");
                }
                else
                {
                    Console.WriteLine($"Password updated successfully: {outputFile}");
                }

                // Close the facade to release resources
                security.Close();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputFile}': {ex.Message}");
            }
        }
    }
}