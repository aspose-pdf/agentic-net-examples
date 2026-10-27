using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "secured_watermarked.pdf";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        // Create a new PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document())
        {
            // Add a single blank page (pages are 1‑based).
            Page page = doc.Pages.Add();

            // Create a text stamp that will serve as the watermark.
            TextStamp watermark = new TextStamp("CONFIDENTIAL")
            {
                // Position the stamp at the center of the page.
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center,

                // Rotate the watermark for a typical overlay effect.
                RotateAngle = 45f,

                // Make the watermark semi‑transparent.
                Opacity = 0.3f,

                // Enable background rectangle (default background color is used).
                Background = true
                // Note: TextStamp does not expose a BackgroundColor property; the default background is sufficient.
            };

            // Apply the watermark to the page.
            page.AddStamp(watermark);

            // Define permissions for the encrypted PDF.
            Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

            // Encrypt the document using the recommended AES‑256 algorithm.
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

            // Save the secured PDF. The Document is still alive until Save completes.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF created with watermark and encryption: {outputPath}");
    }
}