using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string xfdfPath = "annotations.xfdf";
        const string modifiedXfdfPath = "annotations_modified.xfdf";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Export existing annotations to XFDF
            using (FileStream exportStream = new FileStream(xfdfPath, FileMode.Create, FileAccess.Write))
            {
                pdfDoc.ExportAnnotationsToXfdf(exportStream);
            }

            // Load the XFDF XML, change all annotation colors to red (RGB 1 0 0)
            XDocument xfdfXml = XDocument.Load(xfdfPath);
            foreach (XElement colorElem in xfdfXml.Descendants("color"))
            {
                // XFDF stores color as three space‑separated numbers (0‑1 range)
                colorElem.Value = "1 0 0";
            }
            xfdfXml.Save(modifiedXfdfPath);

            // Import the modified XFDF back into the PDF
            pdfDoc.ImportAnnotationsFromXfdf(modifiedXfdfPath);

            // Save the updated PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Annotations exported, colors changed, and PDF saved to '{outputPdfPath}'.");
    }
}
