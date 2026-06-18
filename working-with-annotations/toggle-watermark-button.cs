using System;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;

namespace AsposePdfExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a sample PDF
            using (Document createDoc = new Document())
            {
                Page createPage = createDoc.Pages.Add();
                createDoc.Save("input.pdf");
            }

            // Open the sample PDF
            using (Document doc = new Document("input.pdf"))
            {
                Page page = doc.Pages[1];

                // Add a watermark artifact to the page
                WatermarkArtifact watermarkArtifact = new WatermarkArtifact();
                watermarkArtifact.Text = "CONFIDENTIAL";
                watermarkArtifact.Position = new Aspose.Pdf.Point(200, 400);
                watermarkArtifact.Opacity = 0.5f;
                page.Artifacts.Add(watermarkArtifact);

                // Add a visible watermark annotation (so the effect can be seen)
                WatermarkAnnotation watermarkAnnotation = new WatermarkAnnotation(page, new Aspose.Pdf.Rectangle(150, 350, 450, 550));
                watermarkAnnotation.Contents = "Watermark";
                watermarkAnnotation.Color = Aspose.Pdf.Color.Gray;
                page.Annotations.Add(watermarkAnnotation);

                // Add a button field that toggles the watermark annotation visibility
                ButtonField toggleButton = new ButtonField(page, new Aspose.Pdf.Rectangle(50, 50, 150, 80));
                toggleButton.Actions.OnPressMouseBtn = new JavascriptAction("var ann = this.getAnnots()[0]; ann.hidden = !ann.hidden;");
                doc.Form.Add(toggleButton);

                doc.Save("output.pdf");
            }
        }
    }
}
