using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Operators;

class Program
{
    static void Main()
    {
        // Create a sample PDF (self‑contained example)
        using (Document sampleDoc = new Document())
        {
            Page samplePage = sampleDoc.Pages.Add();
            sampleDoc.Save("input.pdf");
        }

        // Open the PDF and add a curved text watermark using WatermarkArtifact
        using (Document doc = new Document("input.pdf"))
        {
            Page page = doc.Pages[1];

            WatermarkArtifact watermark = new WatermarkArtifact();
            watermark.IsBackground = false; // place on top of page content
            watermark.Text = "Curved Watermark";

            TextState textState = new TextState("Arial", 48);
            textState.ForegroundColor = Aspose.Pdf.Color.Gray;
            watermark.TextState = textState;

            // Define a simple curved path (MoveTo + CurveTo1 + CurveTo2)
            watermark.Contents.Add(new MoveTo(100.0, 400.0));
            watermark.Contents.Add(new CurveTo1(200.0, 500.0, 300.0, 300.0));
            watermark.Contents.Add(new CurveTo2(400.0, 200.0, 500.0, 400.0));

            page.Artifacts.Add(watermark);

            doc.Save("output.pdf");
        }
    }
}