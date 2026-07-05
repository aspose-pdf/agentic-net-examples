using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF (may be encrypted)
        const string inputPath = "input.pdf";

        // Temporary file to hold the decrypted version (if needed)
        const string tempPath = "temp_decrypted.pdf";

        // Final output PDF after modifications
        const string outputPath = "output.pdf";

        // Owner password for decryption (empty string if unknown or not needed)
        const string ownerPassword = "owner123";

        // ------------------------------------------------------------
        // Ensure an input PDF exists – create a simple one if missing.
        // ------------------------------------------------------------
        if (!File.Exists(inputPath))
        {
            var dummyDoc = new Document();
            dummyDoc.Pages.Add(); // add a blank page
            dummyDoc.Save(inputPath);
            Console.WriteLine($"Created placeholder PDF at '{inputPath}'.");
        }

        // ------------------------------------------------------------
        // Step 1: Decrypt the PDF if it is encrypted.
        // ------------------------------------------------------------
        using (PdfFileSecurity security = new PdfFileSecurity())
        {
            // Bind the source PDF to the facade.
            security.BindPdf(inputPath);

            try
            {
                // Attempt to decrypt using the provided owner password.
                // DecryptFile returns true on success (also true when the file is not encrypted).
                bool success = security.DecryptFile(ownerPassword);

                // Save the (decrypted) PDF to a temporary file.
                // If decryption failed (e.g., wrong password), we fall back to copying the original.
                if (success)
                {
                    security.Save(tempPath);
                }
                else
                {
                    File.Copy(inputPath, tempPath, overwrite: true);
                }
            }
            catch (Exception ex)
            {
                // If decryption throws (e.g., wrong password), fall back to copying the original file.
                Console.WriteLine($"Decryption error: {ex.Message}. Using original PDF.");
                File.Copy(inputPath, tempPath, overwrite: true);
            }
        }

        // ------------------------------------------------------------
        // Step 2: Modify the PDF content using a Facades editor.
        // Example: replace all occurrences of "Hello" with "Hi".
        // ------------------------------------------------------------
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the (now decrypted) PDF.
            editor.BindPdf(tempPath);

            // Perform a simple text replacement.
            editor.ReplaceText("Hello", "Hi");

            // Save the modified PDF to the final output path.
            editor.Save(outputPath);
        }

        // ------------------------------------------------------------
        // Step 3: Clean up the temporary file.
        // ------------------------------------------------------------
        try
        {
            File.Delete(tempPath);
        }
        catch (Exception ex)
        {
            // Log but ignore – the OS will clean up later.
            Console.WriteLine($"Could not delete temporary file: {ex.Message}");
        }

        Console.WriteLine($"Processing complete. Output saved to '{outputPath}'.");
    }
}
