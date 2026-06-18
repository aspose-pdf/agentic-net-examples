using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Xml.Linq;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string xfdfPath = "annotations_pretty.xfdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Bind the PDF to the annotation editor
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(pdfPath);

            // Export annotations to a memory stream
            using (MemoryStream rawStream = new MemoryStream())
            {
                editor.ExportAnnotationsToXfdf(rawStream);
                rawStream.Position = 0; // Reset stream for reading

                // Load the raw XFDF XML
                XDocument xfdfDoc = XDocument.Load(rawStream);

                // Save the XML with indentation (pretty‑printed)
                using (FileStream outFs = new FileStream(xfdfPath, FileMode.Create, FileAccess.Write))
                {
                    xfdfDoc.Save(outFs);
                }
            }
        }

        Console.WriteLine($"Annotations exported to '{xfdfPath}' with pretty‑printed XML.");
    }
}