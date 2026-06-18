using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Embed3DModels
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string xmlPath      = "models.xml";
        const string outputPdfPath = "output_with_3d.pdf";

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

        // Load the XML that contains references to 3D model files.
        XDocument xmlDoc = XDocument.Load(xmlPath);

        // Open the PDF document inside a using block (lifecycle rule).
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Iterate over each <Model> element in the XML.
            foreach (XElement modelElem in xmlDoc.Root.Elements("Model"))
            {
                // Required attributes: file (path to 3D model), page (1‑based), and rectangle coordinates.
                string modelFile = (string)modelElem.Attribute("file");
                int pageNumber   = (int?)modelElem.Attribute("page") ?? 1;
                double llx       = (double?)modelElem.Attribute("llx") ?? 0;
                double lly       = (double?)modelElem.Attribute("lly") ?? 0;
                double urx       = (double?)modelElem.Attribute("urx") ?? 100;
                double ury       = (double?)modelElem.Attribute("ury") ?? 100;

                if (!File.Exists(modelFile))
                {
                    Console.Error.WriteLine($"3D model not found: {modelFile}");
                    continue; // skip this entry
                }

                // Ensure the requested page exists; Aspose.Pdf uses 1‑based indexing.
                if (pageNumber < 1 || pageNumber > pdfDoc.Pages.Count)
                {
                    Console.Error.WriteLine($"Invalid page number {pageNumber} for model {modelFile}");
                    continue;
                }

                Page page = pdfDoc.Pages[pageNumber];

                // Load the 3D content (U3D or PRC) from the file.
                PDF3DContent content = new PDF3DContent();
                content.Load(modelFile); // loads the 3D file into the content object

                // Create the 3D artwork using the loaded content.
                PDF3DArtwork artwork = new PDF3DArtwork(pdfDoc, content);

                // Define the rectangle where the annotation will appear.
                // Fully qualify to avoid ambiguity with System.Drawing.Rectangle.
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

                // Create the 3D annotation and attach it to the page.
                PDF3DAnnotation annotation = new PDF3DAnnotation(page, rect, artwork);

                // Optionally set a border or color (border requires a parent annotation instance).
                // Here we set a simple black border.
                annotation.Border = new Border(annotation) { Width = 1 };
                annotation.Color = Aspose.Pdf.Color.Black;

                // Add the annotation to the page's annotation collection.
                page.Annotations.Add(annotation);
            }

            // Save the modified PDF (lifecycle rule – save inside using block).
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"3D models embedded and saved to '{outputPdfPath}'.");
    }
}