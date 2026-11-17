using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "secured_output.pdf";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source PDF
            using (Document doc = new Document(inputPath))
            {
                // ----- Add visible warning overlay on every page -----
                TextStamp warningStamp = new TextStamp("WARNING: Copy‑paste is disabled");
                warningStamp.Background = false;
                warningStamp.Opacity = 0.5;
                warningStamp.HorizontalAlignment = HorizontalAlignment.Center;
                warningStamp.VerticalAlignment = VerticalAlignment.Bottom;
                // Configure the text appearance via the read‑only TextState object
                warningStamp.TextState.FontSize = 14;
                warningStamp.TextState.ForegroundColor = Color.Red;

                foreach (Page page in doc.Pages)
                {
                    page.AddStamp(warningStamp);
                }

                // ----- Apply security: disable copy‑paste (no ExtractContent permission) -----
                // Only allow printing; copy‑paste is implicitly disabled because ExtractContent is not granted.
                Permissions perms = Permissions.PrintDocument;
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                // Save the secured PDF with overlay
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
