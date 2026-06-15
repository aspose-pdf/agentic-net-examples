using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

public class Program
{
    public static void Main()
    {
        // Step 1: Create a sample PDF (self‑contained)
        using (Document sampleDoc = new Document())
        {
            // First page
            Page firstPage = sampleDoc.Pages.Add();
            TextFragment tf1 = new TextFragment("First page content");
            firstPage.Paragraphs.Add(tf1);

            // Second page
            Page secondPage = sampleDoc.Pages.Add();
            TextFragment tf2 = new TextFragment("Second page content");
            secondPage.Paragraphs.Add(tf2);

            sampleDoc.Save("input.pdf");
        }

        // Step 2: Open the PDF, add watermark to every page, then encrypt
        using (Document doc = new Document("input.pdf"))
        {
            foreach (Page page in doc.Pages)
            {
                WatermarkArtifact watermark = new WatermarkArtifact();
                watermark.Text = "CONFIDENTIAL";
                // Font size and color are set via TextState
                watermark.TextState = new TextState();
                watermark.TextState.FontSize = 72;
                watermark.TextState.ForegroundColor = Color.Gray;
                watermark.Opacity = 0.3f;
                watermark.Rotation = 45;
                page.Artifacts.Add(watermark);
            }

            // Encrypt the document – no permissions granted (all bits cleared)
            Permissions permissions = (Permissions)0; // equivalent to "no permissions"
            doc.Encrypt("userPassword", "ownerPassword", permissions, CryptoAlgorithm.AESx128);

            doc.Save("output.pdf");
        }
    }
}