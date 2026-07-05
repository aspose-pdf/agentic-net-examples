using System;
using System.IO;
using Aspose.Pdf.Facades;          // PdfAnnotationEditor
using System.Xml.Linq;            // XDocument for pretty‑printing

class ExportAnnotationsPretty
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";      // source PDF with annotations
        const string outputXfdfPath = "annotations_pretty.xfdf"; // pretty‑printed XFDF

        // Ensure the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Use PdfAnnotationEditor to export annotations to a memory stream
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPdfPath);

            using (MemoryStream tempStream = new MemoryStream())
            {
                // Export all annotations into the temporary stream
                editor.ExportAnnotationsToXfdf(tempStream);

                // Reset stream position for reading
                tempStream.Position = 0;

                // Load the XFDF XML into XDocument (provides LINQ‑to‑XML capabilities)
                XDocument xfdfDoc = XDocument.Load(tempStream);

                // Save the XML with indentation (pretty‑printed) to the final file
                using (FileStream outFile = new FileStream(outputXfdfPath, FileMode.Create, FileAccess.Write))
                {
                    // SaveOptions.None writes formatted XML (indented)
                    xfdfDoc.Save(outFile, SaveOptions.None);
                }
            }
        }

        Console.WriteLine($"Annotations exported and pretty‑printed to '{outputXfdfPath}'.");
    }
}