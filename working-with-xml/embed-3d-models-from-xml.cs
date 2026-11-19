using System;
using System.IO;
using System.Xml.Linq;
using System.Reflection;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Embed3DModels
{
    static void Main()
    {
        const string pdfPath   = "input.pdf";      // source PDF
        const string xmlPath   = "models.xml";     // XML describing 3D models
        const string outputPdf = "output.pdf";     // result PDF

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

        // Load the XML that contains references to 3D model files.
        // Expected format (example):
        // <Models>
        //   <Model page="1" x="100" y="500" width="200" height="200" file="model1.u3d" />
        //   <Model page="2" x="50"  y="400" width="150" height="150" file="model2.prc" />
        // </Models>
        XDocument xmlDoc = XDocument.Load(xmlPath);

        // Open the PDF document inside a using block for deterministic disposal.
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Iterate over each <Model> element in the XML.
            foreach (XElement modelElem in xmlDoc.Root.Elements("Model"))
            {
                // Parse required attributes.
                int pageNumber = (int)modelElem.Attribute("page");
                double llx      = (double)modelElem.Attribute("x");
                double lly      = (double)modelElem.Attribute("y");
                double urx      = llx + (double)modelElem.Attribute("width");
                double ury      = lly + (double)modelElem.Attribute("height");
                string modelFile = (string)modelElem.Attribute("file");

                if (!File.Exists(modelFile))
                {
                    Console.Error.WriteLine($"3D model file not found: {modelFile}");
                    continue; // skip this entry
                }

                // Aspose.Pdf uses 1‑based page indexing.
                if (pageNumber < 1 || pageNumber > pdfDoc.Pages.Count)
                {
                    Console.Error.WriteLine($"Invalid page number {pageNumber} for model {modelFile}");
                    continue;
                }

                // Get the target page.
                Page page = pdfDoc.Pages[pageNumber];

                // Define the annotation rectangle (lower‑left x/y to upper‑right x/y).
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

                // Create a RichMediaAnnotation which can host 3D content.
                RichMediaAnnotation richMedia = new RichMediaAnnotation(page, rect);

                // -----------------------------------------------------------------
                // Attach the 3‑D file using reflection – the RichMediaContent type
                // may not exist in older Aspose.Pdf versions. If it is unavailable we
                // still add a valid RichMediaAnnotation (without embedded content).
                // -----------------------------------------------------------------
                Type richMediaContentType = Type.GetType("Aspose.Pdf.Annotations.RichMediaContent, Aspose.Pdf");
                if (richMediaContentType != null)
                {
                    // Create an instance of RichMediaContent.
                    object richMediaContent = Activator.CreateInstance(richMediaContentType);

                    // Try to locate a method that adds a 3‑D file. Different versions
                    // expose either AddFile or Add3DFile – we handle both.
                    MethodInfo addFileMethod = richMediaContentType.GetMethod("AddFile", new[] { typeof(string) })
                                                ?? richMediaContentType.GetMethod("Add3DFile", new[] { typeof(string) });
                    addFileMethod?.Invoke(richMediaContent, new object[] { modelFile });

                    // Assign the created content to the annotation.
                    PropertyInfo contentProp = typeof(RichMediaAnnotation).GetProperty("RichMediaContent");
                    contentProp?.SetValue(richMedia, richMediaContent);
                }
                else
                {
                    Console.Error.WriteLine($"RichMediaContent type not available – 3D model '{modelFile}' will not be embedded.");
                }

                // Set a human‑readable title. RichMediaAnnotation does not derive from MarkupAnnotation,
                // so we set the Title property via reflection (if it exists).
                string title = Path.GetFileNameWithoutExtension(modelFile);
                PropertyInfo titleProp = typeof(RichMediaAnnotation).GetProperty("Title");
                if (titleProp != null && titleProp.CanWrite)
                {
                    titleProp.SetValue(richMedia, title);
                }
                else
                {
                    // Fallback: try a base type that might expose Title.
                    PropertyInfo baseTitleProp = typeof(RichMediaAnnotation).BaseType?.GetProperty("Title");
                    if (baseTitleProp != null && baseTitleProp.CanWrite)
                    {
                        baseTitleProp.SetValue(richMedia, title);
                    }
                }

                // Add the annotation to the page.
                page.Annotations.Add(richMedia);
            }

            // Save the modified PDF.
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"3D models processed. Output saved to '{outputPdf}'.");
    }
}
