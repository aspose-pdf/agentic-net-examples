using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

public class Program
{
    public static void Main()
    {
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            doc.Pages.Add();

            // Get the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Create a watermark artifact with dynamic year text
            WatermarkArtifact watermark = new WatermarkArtifact();
            watermark.Text = "© " + DateTime.Now.Year + " My Company";
            watermark.TextState = new TextState("Arial", 36);
            watermark.IsBackground = true;
            watermark.Opacity = 0.5f;
            watermark.ArtifactHorizontalAlignment = HorizontalAlignment.Center;
            watermark.ArtifactVerticalAlignment = VerticalAlignment.Center;

            // Add the watermark to the page's artifacts collection
            page.Artifacts.Add(watermark);

            // Save the resulting PDF
            doc.Save("output.pdf");
        }
    }
}