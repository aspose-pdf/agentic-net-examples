using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

public class Program
{
    public static void Main()
    {
        // Create a sample PDF document
        using (Document sampleDoc = new Document())
        {
            Page samplePage = sampleDoc.Pages.Add();
            TextFragment sampleFragment = new TextFragment("This is a sample PDF document.");
            samplePage.Paragraphs.Add(sampleFragment);
            sampleDoc.Save("input.pdf");
        }

        // Open the PDF and add a semi‑transparent text watermark behind the content on each page
        using (Document doc = new Document("input.pdf"))
        {
            foreach (Page page in doc.Pages)
            {
                WatermarkArtifact watermark = new WatermarkArtifact();
                watermark.IsBackground = true; // place behind existing content
                watermark.Opacity = 0.3f; // semi‑transparent

                // Center the watermark on the page
                watermark.ArtifactHorizontalAlignment = HorizontalAlignment.Center;
                watermark.ArtifactVerticalAlignment = VerticalAlignment.Center;

                // Define the appearance of the watermark text
                TextState textState = new TextState("Arial", 72);
                textState.ForegroundColor = Aspose.Pdf.Color.Gray;
                watermark.SetTextAndState("CONFIDENTIAL", textState);

                // Attach the watermark artifact to the page
                page.Artifacts.Add(watermark);
            }

            doc.Save("output.pdf");
        }
    }
}