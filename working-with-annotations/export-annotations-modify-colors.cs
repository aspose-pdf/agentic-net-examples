using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string xfdfExportPath = "annotations.xfdf";
        const string xfdfModifiedPath = "annotations_modified.xfdf";
        const string outputPdfPath = "output.pdf";
        const string newColorHex = "#00FF00"; // desired annotation color (green)

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // 1. Load the PDF and export its annotations to XFDF
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Export only annotations to XFDF file
            pdfDoc.ExportAnnotationsToXfdf(xfdfExportPath);
        }

        // 2. Load the exported XFDF XML, change all <color> values
        XDocument xfdfXml = XDocument.Load(xfdfExportPath);
        foreach (XElement colorElem in xfdfXml.Descendants("color"))
        {
            colorElem.Value = newColorHex;
        }
        xfdfXml.Save(xfdfModifiedPath);

        // 3. Re‑import the modified XFDF back into the PDF
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            using (FileStream xfdfStream = new FileStream(xfdfModifiedPath, FileMode.Open, FileAccess.Read))
            {
                pdfDoc.ImportAnnotationsFromXfdf(xfdfStream);
            }
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Annotations exported, colors changed to {newColorHex}, and PDF saved as '{outputPdfPath}'.");
    }
}
