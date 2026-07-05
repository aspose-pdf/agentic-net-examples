using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath   = "input.pdf";
        const string xfdfPath       = "annotations.xfdf";
        const string modifiedXfdfPath = "annotations_modified.xfdf";
        const string outputPdfPath  = "output.pdf";

        // Ensure the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdfPath}");
            return;
        }

        // Load the PDF, export its annotations to XFDF, modify colors, re‑import, and save
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // 1. Export all annotations to an XFDF file
            pdfDoc.ExportAnnotationsToXfdf(xfdfPath);

            // 2. Load the XFDF XML, change annotation colors, and save to a new file
            XDocument xfdfXml = XDocument.Load(xfdfPath);

            // XFDF stores colors in <color> elements (e.g., <color>0 0 1</color> for blue)
            // Replace every <color> element with a new RGB value – here we set all to red (1 0 0)
            foreach (XElement colorElem in xfdfXml.Descendants("color"))
            {
                colorElem.Value = "1 0 0"; // Red in normalized RGB (range 0‑1)
            }

            // Save the modified XFDF
            xfdfXml.Save(modifiedXfdfPath);

            // 3. (Optional) Remove existing annotations to avoid duplication
            // Iterate all pages and clear their annotation collections
            foreach (Page page in pdfDoc.Pages)
            {
                // Annotation collection uses 1‑based indexing; clear by deleting from the end
                while (page.Annotations.Count > 0)
                {
                    page.Annotations.Delete(page.Annotations.Count);
                }
            }

            // 4. Import the modified annotations back into the PDF
            pdfDoc.ImportAnnotationsFromXfdf(modifiedXfdfPath);

            // 5. Save the updated PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPdfPath}'.");
    }
}