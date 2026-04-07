using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Embed3DModels
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string xmlPath        = "models.xml";
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

        // Load the source PDF
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Load XML that contains references to 3D model files
            XDocument xmlDoc = XDocument.Load(xmlPath);

            // Expected XML format:
            // <Models>
            //   <Model src="model1.u3d" page="1" llx="100" lly="500" urx="300" ury="600" />
            //   <Model src="model2.prc" page="2" llx="50"  lly="400" urx="250" ury="550" />
            // </Models>

            var modelElements = xmlDoc.Root?.Elements("Model");
            if (modelElements == null)
            {
                Console.Error.WriteLine("No <Model> elements found in XML.");
                return;
            }

            foreach (var modelElem in modelElements)
            {
                string modelPath = (string)modelElem.Attribute("src");
                int pageNumber   = (int?)modelElem.Attribute("page") ?? 1;
                double llx       = (double?)modelElem.Attribute("llx") ?? 0;
                double lly       = (double?)modelElem.Attribute("lly") ?? 0;
                double urx       = (double?)modelElem.Attribute("urx") ?? 0;
                double ury       = (double?)modelElem.Attribute("ury") ?? 0;

                if (string.IsNullOrEmpty(modelPath) || !File.Exists(modelPath))
                {
                    Console.Error.WriteLine($"3D model file not found: {modelPath}");
                    continue;
                }

                // Ensure the requested page exists (Aspose.Pdf uses 1‑based indexing)
                if (pageNumber < 1 || pageNumber > pdfDoc.Pages.Count)
                {
                    Console.Error.WriteLine($"Invalid page number {pageNumber} for model {modelPath}");
                    continue;
                }

                // Load the 3D content (U3D or PRC)
                PDF3DContent content = new PDF3DContent();
                string ext = Path.GetExtension(modelPath).ToLowerInvariant();
                if (ext == ".u3d")
                {
                    content.LoadAsU3D(modelPath);
                }
                else if (ext == ".prc")
                {
                    content.LoadAsPRC(modelPath);
                }
                else
                {
                    Console.Error.WriteLine($"Unsupported 3D format '{ext}' for file {modelPath}");
                    continue;
                }

                // Create the 3D artwork (default lighting and render mode)
                PDF3DArtwork artwork = new PDF3DArtwork(pdfDoc, content);

                // Define the rectangle where the annotation will appear
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

                // Create the 3D annotation (activate when visible)
                PDF3DAnnotation annotation = new PDF3DAnnotation(pdfDoc.Pages[pageNumber], rect, artwork,
                    PDF3DActivation.activeWhenVisible);

                // Optional: set a border and a background color for better visibility
                annotation.Border = new Border(annotation) { Width = 1 };
                annotation.Color = Aspose.Pdf.Color.LightGray;

                // Add the annotation to the page
                pdfDoc.Pages[pageNumber].Annotations.Add(annotation);
            }

            // Save the resulting PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"3D‑enhanced PDF saved to '{outputPdfPath}'.");
    }
}