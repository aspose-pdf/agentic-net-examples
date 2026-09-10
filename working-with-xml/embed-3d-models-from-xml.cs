using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Embed3DModels
{
    static void Main()
    {
        const string pdfPath   = "input.pdf";      // source PDF
        const string xmlPath   = "models.xml";     // XML with 3D model references
        const string outputPdf = "output_with_3d.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML not found: {xmlPath}");
            return;
        }

        // Load the XML. Expected format:
        // <Models>
        //   <Model page="1" x="100" y="500" width="300" height="300" file="model1.u3d" />
        //   ...
        // </Models>
        XDocument xmlDoc = XDocument.Load(xmlPath);

        using (Document pdfDoc = new Document(pdfPath))
        {
            foreach (XElement modelElem in xmlDoc.Root.Elements("Model"))
            {
                // Parse attributes
                int pageNumber = (int)modelElem.Attribute("page");
                double llx = (double)modelElem.Attribute("x");
                double lly = (double)modelElem.Attribute("y");
                double urx = llx + (double)modelElem.Attribute("width");
                double ury = lly + (double)modelElem.Attribute("height");
                string modelFile = (string)modelElem.Attribute("file");

                if (!File.Exists(modelFile))
                {
                    Console.Error.WriteLine($"3D model file not found: {modelFile}");
                    continue;
                }

                // Create PDF3DContent and load the model file
                PDF3DContent content = new PDF3DContent();
                string ext = Path.GetExtension(modelFile).ToLowerInvariant();
                if (ext == ".u3d")
                    content.LoadAsU3D(modelFile);
                else if (ext == ".prc")
                    content.LoadAsPRC(modelFile);
                else
                {
                    // Fallback to generic constructor that accepts filename
                    content = new PDF3DContent(modelFile);
                }

                // Create the 3D artwork using the document and the content
                PDF3DArtwork artwork = new PDF3DArtwork(pdfDoc, content);

                // Define the rectangle where the annotation will appear
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

                // Create the 3D annotation and add it to the page
                Page page = pdfDoc.Pages[pageNumber]; // 1‑based indexing
                PDF3DAnnotation annotation = new PDF3DAnnotation(page, rect, artwork);
                page.Annotations.Add(annotation);
            }

            // Save the modified PDF
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"3D models embedded and saved to '{outputPdf}'.");
    }
}