using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;
using Aspose.Pdf.Operators;

namespace AddCurvedWatermarkExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a simple PDF document to work with
            using (Document doc = new Document())
            {
                // Add a blank page
                Page page = doc.Pages.Add();
                // Save the temporary document (required by the rule to use create/save)
                doc.Save("input.pdf");
            }

            // Re-open the document to add the curved text watermark
            using (Document doc = new Document("input.pdf"))
            {
                // Get the first page (1‑based indexing)
                Page page = doc.Pages[1];

                // Create a text state for the watermark text
                TextState textState = new TextState();
                textState.Font = FontRepository.FindFont("Arial");
                textState.FontSize = 36;
                textState.ForegroundColor = Aspose.Pdf.Color.Red;

                // Create a watermark artifact
                WatermarkArtifact watermark = new WatermarkArtifact();
                // Set the watermark text and its appearance
                watermark.SetTextAndState("Curved Watermark", textState);
                // Make the watermark semi‑transparent
                watermark.Opacity = 0.5f;
                // Position the watermark (optional – margins are ignored when Position is set)
                watermark.Position = new Aspose.Pdf.Point(100f, 500f);

                // Build a simple curved path for the text to follow
                // Move to the start point of the curve
                MoveTo moveTo = new MoveTo(100f, 500f);
                // First curve segment (v operator – CurveTo1)
                CurveTo1 curve1 = new CurveTo1(150f, 450f, 200f, 400f);
                // Second curve segment (y operator – CurveTo2)
                CurveTo2 curve2 = new CurveTo2(250f, 350f, 300f, 300f);
                // Add the operators to the artifact's content collection
                watermark.Contents.Add(moveTo);
                watermark.Contents.Add(curve1);
                watermark.Contents.Add(curve2);

                // Add the watermark artifact to the page's artifact collection
                page.Artifacts.Add(watermark);

                // Save the resulting PDF with the curved watermark
                doc.Save("output.pdf");
            }
        }
    }
}
