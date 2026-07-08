using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;   // PDF3D* classes
using Aspose.Pdf.Drawing;      // for Rectangle (core PDF rectangle)

class Embed3DModels
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string xmlPath        = "models.xml";   // XML that lists 3D model file paths
        const string outputPdfPath  = "output_with_3d.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML not found: {xmlPath}");
            return;
        }

        // Load the XML that contains <Model> elements with a file attribute or inner text.
        XDocument xmlDoc = XDocument.Load(xmlPath);
        // Example expected format:
        // <Models>
        //   <Model file="model1.u3d" page="1" x="100" y="500" width="300" height="300" />
        //   <Model file="model2.prc" page="2" x="50"  y="400" width="250" height="250" />
        // </Models>

        using (Document pdfDoc = new Document(inputPdfPath))
        {
            foreach (XElement modelElem in xmlDoc.Root.Elements("Model"))
            {
                // Resolve required attributes; provide defaults if missing.
                string modelFile = (string)modelElem.Attribute("file");
                if (string.IsNullOrEmpty(modelFile) || !File.Exists(modelFile))
                {
                    Console.Error.WriteLine($"Model file missing or not found: {modelFile}");
                    continue;
                }

                int pageNumber = (int?)modelElem.Attribute("page") ?? 1;
                if (pageNumber < 1 || pageNumber > pdfDoc.Pages.Count)
                {
                    Console.Error.WriteLine($"Invalid page number {pageNumber} for model {modelFile}");
                    continue;
                }

                double x      = (double?)modelElem.Attribute("x")      ?? 100;
                double y      = (double?)modelElem.Attribute("y")      ?? 500;
                double width  = (double?)modelElem.Attribute("width")  ?? 300;
                double height = (double?)modelElem.Attribute("height") ?? 300;

                // Create PDF3DContent and load the 3D file.
                PDF3DContent content = new PDF3DContent();
                // The Load method determines format from extension; it throws if unsupported.
                content.Load(modelFile);

                // Create the 3D artwork using the content.
                PDF3DArtwork artwork = new PDF3DArtwork(pdfDoc, content);

                // Optional: set a default view (camera) so the model is visible when opened.
                // Here we use the default view; more complex view setup can be added if needed.

                // Define the rectangle where the annotation will appear.
                // Aspose.Pdf.Rectangle constructor: (llx, lly, urx, ury)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(
                    x,                     // lower‑left X
                    y - height,            // lower‑left Y (origin is bottom‑left)
                    x + width,             // upper‑right X
                    y);                    // upper‑right Y

                // Create the 3D annotation on the target page.
                Page targetPage = pdfDoc.Pages[pageNumber];
                PDF3DAnnotation annotation = new PDF3DAnnotation(targetPage, rect, artwork);

                // Set a border and background color for visual cue (optional).
                annotation.Border = new Border(annotation) { Width = 1 };
                annotation.Color = Aspose.Pdf.Color.LightGray;

                // Add the annotation to the page.
                targetPage.Annotations.Add(annotation);
            }

            // Save the modified PDF.
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"3D models embedded and saved to '{outputPdfPath}'.");
    }
}