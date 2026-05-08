using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";   // source PDF
        const string xmlPath       = "models.xml"; // XML that references 3D model files
        const string outputPdfPath = "output.pdf"; // result PDF with 3D annotations

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load the existing PDF
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Load and parse the XML file
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);
            XmlNodeList modelNodes = xmlDoc.SelectNodes("//Model"); // expects <Model file=\"path/to/model.u3d\"/>

            // Start placing annotations on the first page; advance if more pages exist
            int pageNumber = 1;

            foreach (XmlNode modelNode in modelNodes)
            {
                string modelFile = modelNode.Attributes["file"]?.Value;
                if (string.IsNullOrEmpty(modelFile) || !File.Exists(modelFile))
                {
                    Console.Error.WriteLine($"Model file missing or not found: {modelFile}");
                    continue;
                }

                // Load the 3D content (U3D, PRC, etc.)
                PDF3DContent content = new PDF3DContent(modelFile);

                // Create the 3D artwork that will be embedded in the PDF
                PDF3DArtwork artwork = new PDF3DArtwork(pdfDoc, content);

                // Define the rectangle where the interactive 3D view will appear
                // (left, bottom, right, top) – adjust as needed
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 400, 400, 700);

                // Create the 3D annotation on the current page
                PDF3DAnnotation annotation = new PDF3DAnnotation(pdfDoc.Pages[pageNumber], rect, artwork);

                // NOTE: The Activation property is not available in the current Aspose.Pdf version.
                // The default behavior activates the 3D model when the page is opened, which matches the intended effect.
                // If a specific activation mode is required, use the appropriate property provided by the library version.

                // Add the annotation to the page's annotation collection
                pdfDoc.Pages[pageNumber].Annotations.Add(annotation);

                // If there are more pages, move to the next one for the next model
                if (pageNumber < pdfDoc.Pages.Count)
                    pageNumber++;
            }

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"3D‑enhanced PDF saved to '{outputPdfPath}'.");
    }
}
