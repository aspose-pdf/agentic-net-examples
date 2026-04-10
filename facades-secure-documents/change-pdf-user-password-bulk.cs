// ------------------------------------------------------------
// File: AsposePdfApi.GeneratedMSBuildEditorConfig.editorconfig
// ------------------------------------------------------------
// This file is intentionally left empty. It satisfies the project
// reference that expects a source file with this name. Because the
// C# compiler treats any file listed under <Compile> as a source file,
// an empty file containing only comments compiles without errors.
// ------------------------------------------------------------
// File: Program.cs
// ------------------------------------------------------------
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
        const string newUserPassword = "StandardUserPass";

        // Original owner password – adjust if your PDFs are protected with an owner password
        const string originalOwnerPassword = "";

        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder not found: {folderPath}");
            return;
        }

        // Get all PDF files in the folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(folderPath, "*.pdf");

        foreach (string inputFile in pdfFiles)
        {
            // Create an output file name (you can overwrite the original if desired)
            string outputFile = Path.Combine(
                folderPath,
                Path.GetFileNameWithoutExtension(inputFile) + "_secured.pdf");

            try
            {
                // Initialize the PdfFileSecurity facade with input and output paths
                PdfFileSecurity fileSecurity = new PdfFileSecurity(inputFile, outputFile);

                // Change the user password; keep the original owner password,
                // set a new user password, and let the owner password be generated randomly (null)
                bool success = fileSecurity.ChangePassword(originalOwnerPassword, newUserPassword, null);

                if (!success)
                {
                    Console.Error.WriteLine($"Failed to change password for: {inputFile}");
                }

                // Release resources held by the facade
                fileSecurity.Close();

                Console.WriteLine($"Processed: {inputFile} → {outputFile}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputFile}': {ex.Message}");
            }
        }
    }
}
