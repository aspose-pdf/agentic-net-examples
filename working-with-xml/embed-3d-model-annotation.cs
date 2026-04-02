using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

namespace Embed3DModelAnnotation
{
    class Program
    {
        static void Main(string[] args)
        {
            // -----------------------------------------------------------------
            // Step 1: Create a simple XML file that lists 3D model file paths.
            // -----------------------------------------------------------------
            string xmlPath = "models.xml";
            // For demonstration we also create a dummy 3D file (empty) – in real scenarios use a valid U3D/PRC file.
            string dummyModelPath = "sample.u3d";
            if (!File.Exists(dummyModelPath))
            {
                using (FileStream fs = File.Create(dummyModelPath))
                {
                    // Write minimal content – a real 3D file should be provided.
                }
            }
            XmlDocument xmlDoc = new XmlDocument();
            XmlElement root = xmlDoc.CreateElement("Models");
            XmlElement modelElement = xmlDoc.CreateElement("Model");
            modelElement.SetAttribute("path", dummyModelPath);
            root.AppendChild(modelElement);
            xmlDoc.AppendChild(root);
            xmlDoc.Save(xmlPath);

            // -----------------------------------------------------------------
            // Step 2: Load the XML and iterate over each model entry.
            // -----------------------------------------------------------------
            XmlDocument loadDoc = new XmlDocument();
            loadDoc.Load(xmlPath);
            XmlNodeList modelNodes = loadDoc.SelectNodes("/Models/Model");

            // -----------------------------------------------------------------
            // Step 3: Create a PDF document and add a page.
            // -----------------------------------------------------------------
            using (Document pdfDoc = new Document())
            {
                Page page = pdfDoc.Pages.Add();

                // -----------------------------------------------------------------
                // Step 4: For each model, create a 3D annotation and add it to the page.
                // -----------------------------------------------------------------
                foreach (XmlNode modelNode in modelNodes)
                {
                    string modelPath = modelNode.Attributes["path"].Value;

                    // Load the 3D content (U3D or PRC). Here we assume U3D format.
                    PDF3DContent pdf3dContent = new PDF3DContent();
                    pdf3dContent.LoadAsU3D(modelPath);

                    // Create the 3D artwork object.
                    PDF3DArtwork pdf3dArtwork = new PDF3DArtwork(pdfDoc, pdf3dContent);

                    // Define the rectangle where the annotation will appear (left, bottom, width, height).
                    Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100f, 500f, 300f, 200f);

                    // Create the 3D annotation with activation set to visible.
                    PDF3DAnnotation pdf3dAnnotation = new PDF3DAnnotation(page, rect, pdf3dArtwork, PDF3DActivation.activeWhenVisible);

                    // Add the annotation to the page.
                    page.Annotations.Add(pdf3dAnnotation);
                }

                // -----------------------------------------------------------------
                // Step 5: Save the resulting PDF.
                // -----------------------------------------------------------------
                pdfDoc.Save("output.pdf");
            }
        }
    }
}
