using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output_modified.pdf";
        const string xfdfPath      = "annotations.xfdf";

        // ------------------------------------------------------------
        // Ensure the source PDF exists. If it does not, create a simple
        // PDF with a single page and a sample text annotation. This
        // makes the sample self‑contained and prevents the FileNotFound
        // exception that caused the original build failure.
        // ------------------------------------------------------------
        if (!File.Exists(inputPdfPath))
        {
            using (Document sampleDoc = new Document())
            {
                // Add a blank page
                Page page = sampleDoc.Pages.Add();

                // Define the annotation rectangle (left, bottom, right, top)
                var annRect = new Aspose.Pdf.Rectangle(100, 700, 200, 800);

                // Create a simple text annotation (sticky note) on the page
                TextAnnotation txtAnn = new TextAnnotation(page, annRect)
                {
                    Title    = "Sample",
                    Contents = "This is a sample annotation created because 'input.pdf' was missing.",
                    Color    = Aspose.Pdf.Color.Yellow,
                    Open     = true
                };
                page.Annotations.Add(txtAnn);

                // Save the generated PDF so the rest of the workflow can use it
                sampleDoc.Save(inputPdfPath);
                Console.WriteLine($"Sample PDF created at '{inputPdfPath}'.");
            }
        }

        // ------------------------------------------------------------
        // Export existing annotations to XFDF file
        // ------------------------------------------------------------
        using (Document doc = new Document(inputPdfPath))
        {
            // The ExportAnnotationsToXfdf overload that takes a file path is fine.
            doc.ExportAnnotationsToXfdf(xfdfPath);
        }

        // ------------------------------------------------------------
        // Load the exported XFDF XML, modify annotation colors, and save it back
        // ------------------------------------------------------------
        XDocument xfdfXml = XDocument.Load(xfdfPath);
        // XFDF stores colors in <color> elements as space‑separated RGB values (0‑1 range)
        foreach (XElement colorElem in xfdfXml.Descendants("color"))
        {
            // Change every annotation color to green (RGB = 0 1 0)
            colorElem.Value = "0 1 0";
        }
        // Overwrite the XFDF file with the modified XML
        xfdfXml.Save(xfdfPath);

        // ------------------------------------------------------------
        // Re‑import the modified annotations into the PDF and save the result
        // ------------------------------------------------------------
        using (Document doc = new Document(inputPdfPath))
        {
            doc.ImportAnnotationsFromXfdf(xfdfPath);
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Annotations exported, colors changed, and PDF saved to '{outputPdfPath}'.");
    }
}
