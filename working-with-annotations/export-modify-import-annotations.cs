using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;

namespace ExportModifyImportAnnotations
{
    class Program
    {
        static void Main(string[] args)
        {
            // Step 1: Create a sample PDF with a red square annotation
            using (Document sampleDoc = new Document())
            {
                sampleDoc.Pages.Add();
                // Create a square annotation on page 1
                SquareAnnotation square = new SquareAnnotation(sampleDoc.Pages[1], new Aspose.Pdf.Rectangle(100, 600, 200, 500));
                square.Color = Color.FromRgb(1.0f, 0.0f, 0.0f); // red
                square.Border = new Border(square);
                sampleDoc.Pages[1].Annotations.Add(square);
                sampleDoc.Save("sample.pdf");
            }

            // Step 2: Export annotations to XFDF
            using (Document docWithAnn = new Document("sample.pdf"))
            {
                docWithAnn.ExportAnnotationsToXfdf("annotations.xfdf");
            }

            // Step 3: Load XFDF, change annotation color to green (RGB 0 1 0)
            XmlDocument xfdfDoc = new XmlDocument();
            xfdfDoc.Load("annotations.xfdf");
            XmlNodeList annotNodes = xfdfDoc.GetElementsByTagName("annot");
            foreach (XmlNode annotNode in annotNodes)
            {
                XmlAttribute colorAttr = annotNode.Attributes["c"];
                if (colorAttr != null)
                {
                    colorAttr.Value = "0 1 0"; // green
                }
                else
                {
                    // If the color attribute does not exist, create it
                    XmlAttribute newColor = xfdfDoc.CreateAttribute("c");
                    newColor.Value = "0 1 0";
                    annotNode.Attributes.Append(newColor);
                }
            }
            xfdfDoc.Save("annotations_modified.xfdf");

            // Step 4: Create a blank PDF (same size) without annotations
            using (Document blankDoc = new Document())
            {
                blankDoc.Pages.Add();
                blankDoc.Save("blank.pdf");
            }

            // Step 5: Import the modified XFDF into the blank PDF and save the result
            using (Document resultDoc = new Document("blank.pdf"))
            {
                resultDoc.ImportAnnotationsFromXfdf("annotations_modified.xfdf");
                resultDoc.Save("output.pdf");
            }
        }
    }
}
