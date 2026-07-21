using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "secured_output.pdf";

        const string userPassword  = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // -----------------------------------------------------------------
                // 1. Add a visible warning overlay on every page
                // -----------------------------------------------------------------
                TextStamp warningStamp = new TextStamp("CONFIDENTIAL – DO NOT COPY")
                {
                    // Position the stamp in the center of the page
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment   = VerticalAlignment.Center,
                    // Make the stamp semi‑transparent
                    Opacity = 0.5f,
                    // Render the stamp in front of page content
                    Background = false
                };

                // Configure text appearance
                warningStamp.TextState.Font = FontRepository.FindFont("Helvetica");
                warningStamp.TextState.FontSize = 24;
                warningStamp.TextState.ForegroundColor = Aspose.Pdf.Color.Red;

                // Apply the stamp to each page
                foreach (Page page in doc.Pages)
                {
                    page.AddStamp(warningStamp);
                }

                // -----------------------------------------------------------------
                // 2. Encrypt the document and disable copy‑paste
                // -----------------------------------------------------------------
                // Exclude the ExtractContent permission to prevent copying.
                Permissions allowedPermissions = Permissions.PrintDocument | Permissions.ModifyContent;

                // Use AES‑256 encryption (strongest supported algorithm)
                doc.Encrypt(userPassword, ownerPassword, allowedPermissions, CryptoAlgorithm.AESx256);

                // -----------------------------------------------------------------
                // 3. Save the secured PDF
                // -----------------------------------------------------------------
                doc.Save(outputPath);
            }

            Console.WriteLine($"Secured PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}