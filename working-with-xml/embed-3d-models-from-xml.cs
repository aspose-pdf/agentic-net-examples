using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Embed3DModels
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";      // existing PDF to augment
        const string xmlPath        = "models.xml";     // XML describing 3D models
        const string outputPdfPath  = "output.pdf";     // result PDF with 3D annotations

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

        // Load the XML that contains references to 3D files.
        XDocument xmlDoc = XDocument.Load(xmlPath);

        // Open the PDF document inside a using block (ensures proper disposal).
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Iterate over each <Model> element in the XML.
            foreach (XElement modelElem in xmlDoc.Root.Elements("Model"))
            {
                // Required attributes: page, x, y, width, height, file
                int pageNumber = (int)modelElem.Attribute("page");
                double x        = (double)modelElem.Attribute("x");
                double y        = (double)modelElem.Attribute("y");
                double width    = (double)modelElem.Attribute("width");
                double height   = (double)modelElem.Attribute("height");
                string modelFile = (string)modelElem.Attribute("file");

                if (!File.Exists(modelFile))
                {
                    Console.Error.WriteLine($"3D model file not found: {modelFile}");
                    continue; // skip this entry
                }

                // Ensure the requested page exists (Aspose.Pdf uses 1‑based indexing).
                if (pageNumber < 1 || pageNumber > pdfDoc.Pages.Count)
                {
                    Console.Error.WriteLine($"Invalid page number {pageNumber} for model {modelFile}");
                    continue;
                }

                Page page = pdfDoc.Pages[pageNumber];

                // Create PDF3DContent and load the 3D file (U3D or PRC).
                PDF3DContent content = new PDF3DContent();
                string ext = Path.GetExtension(modelFile).ToLowerInvariant();
                if (ext == ".u3d")
                {
                    content.LoadAsU3D(modelFile);
                }
                else if (ext == ".prc")
                {
                    content.LoadAsPRC(modelFile);
                }
                else
                {
                    Console.Error.WriteLine($"Unsupported 3D format: {modelFile}");
                    continue;
                }

                // Create the 3D artwork using the loaded content.
                PDF3DArtwork artwork = new PDF3DArtwork(pdfDoc, content);

                // Define the rectangle where the annotation will appear.
                // Fully qualify to avoid ambiguity with System.Drawing.Rectangle.
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(x, y, x + width, y + height);

                // Create the 3D annotation (activate when the page is opened).
                PDF3DAnnotation annotation = new PDF3DAnnotation(page, rect, artwork, PDF3DActivation.activeWhenOpen);

                // Optional: set a border or color if desired.
                annotation.Color = Aspose.Pdf.Color.LightGray;

                // Add the annotation to the page.
                page.Annotations.Add(annotation);
            }

            // Save the modified PDF.
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"3D‑enhanced PDF saved to '{outputPdfPath}'.");
    }
}