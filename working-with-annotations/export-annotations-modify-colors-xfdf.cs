using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string xfdfPath = "annotations.xfdf";
        const string modifiedXfdfPath = "annotations_modified.xfdf";
        const string outputPdf = "output.pdf";

        // ------------------------------------------------------------
        // 1. Create a sample PDF with at least one annotation so the
        //    sandbox has a real file to work with.
        // ------------------------------------------------------------
        using (Document seed = new Document())
        {
            // Add a blank page.
            Page page = seed.Pages.Add();

            // Create a simple text annotation.
            // Rectangle(left, bottom, right, top)
            var rect = new Rectangle(100, 600, 200, 650);
            TextAnnotation txtAnn = new TextAnnotation(page, rect)
            {
                Contents = "Sample annotation",
                Color = Color.Yellow // Aspose.Pdf.Color
            };
            page.Annotations.Add(txtAnn);

            // Save the seed PDF that will be used later.
            seed.Save(inputPdf);
        }

        // ------------------------------------------------------------
        // 2. Load the PDF and export its annotations to an XFDF file.
        // ------------------------------------------------------------
        using (Document pdfDoc = new Document(inputPdf))
        {
            pdfDoc.ExportAnnotationsToXfdf(xfdfPath);
        }

        // ------------------------------------------------------------
        // 3. Load the exported XFDF, change all annotation colors, and
        //    save the modified XFDF.
        // ------------------------------------------------------------
        XDocument xfdfDoc = XDocument.Load(xfdfPath);
        // XFDF stores colors in <color> elements (e.g., "1 0 0" for red).
        foreach (XElement colorElem in xfdfDoc.Descendants("color"))
        {
            // Change color to green (RGB values are space‑separated floats).
            colorElem.Value = "0 1 0";
        }
        xfdfDoc.Save(modifiedXfdfPath);

        // ------------------------------------------------------------
        // 4. Reload the original PDF, import the modified XFDF, and save.
        // ------------------------------------------------------------
        using (Document pdfDoc = new Document(inputPdf))
        {
            pdfDoc.ImportAnnotationsFromXfdf(modifiedXfdfPath);
            pdfDoc.Save(outputPdf);
        }
    }
}
