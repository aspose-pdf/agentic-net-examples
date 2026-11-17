using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "secured_watermarked.pdf";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a sample page with some content
            Page page = doc.Pages.Add();
            page.Paragraphs.Add(new TextFragment("Sample PDF content."));

            // Add a watermark overlay to every page using TextStamp
            foreach (Page p in doc.Pages)
            {
                // Prepare TextState for the stamp (use font name overload to avoid type mismatch)
                TextState textState = new TextState("Arial", 72);
                textState.FontStyle = FontStyles.Bold;

                // Create the stamp with the prepared TextState
                TextStamp watermark = new TextStamp("CONFIDENTIAL", textState)
                {
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment   = VerticalAlignment.Center,
                    // RotateAngle replaces the non‑existent Rotation property
                    RotateAngle = 45f,
                    // Semi‑transparent watermark
                    Opacity = 0.3,
                    // Place the watermark behind existing content
                    Background = true
                };
                p.AddStamp(watermark);
            }

            // Define permissions: allow printing and content extraction, but disallow modifications
            Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

            // Encrypt the document with a strong algorithm
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

            // Save the secured PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}' with encryption and watermark.");
    }
}
