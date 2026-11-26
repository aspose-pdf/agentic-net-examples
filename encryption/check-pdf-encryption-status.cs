using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string statusFilePath = "status.txt";

        bool isEncrypted = false;

        try
        {
            // Attempt to open the PDF without a password.
            // If it succeeds, the document is not encrypted.
            using (Document doc = new Document(inputPdfPath))
            {
                // No action needed; just confirming successful load.
                isEncrypted = false;
            }
        }
        catch (InvalidPasswordException)
        {
            // Thrown when the PDF requires a password.
            isEncrypted = true;
        }
        catch (Exception ex)
        {
            // Any other error (e.g., file not found) is written to the status file.
            File.WriteAllText(statusFilePath, $"Error: {ex.Message}");
            return;
        }

        string status = isEncrypted ? "Encrypted" : "Not Encrypted";
        File.WriteAllText(statusFilePath, status);
    }
}