using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf.Facades;

class ExportAnnotationsExample
{
    static void Main()
    {
        // Input PDF containing annotations
        const string inputPdfPath = "input.pdf";

        // Output XFDF file with pretty‑printed XML
        const string outputXfdfPath = "annotations_pretty.xfdf";

        // Ensure the input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Use PdfAnnotationEditor to work with PDF annotations
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Bind the PDF document to the editor
            editor.BindPdf(inputPdfPath);

            // Export annotations to a memory stream first
            using (MemoryStream rawXfdf = new MemoryStream())
            {
                editor.ExportAnnotationsToXfdf(rawXfdf);

                // Reset stream position for reading
                rawXfdf.Position = 0;

                // Load the raw XFDF XML into XDocument for formatting
                XDocument xfdfDoc = XDocument.Load(rawXfdf);

                // Save the formatted XML to the final file (indented by default)
                using (FileStream outStream = new FileStream(outputXfdfPath, FileMode.Create, FileAccess.Write))
                {
                    xfdfDoc.Save(outStream);
                }
            }
        }

        Console.WriteLine($"Annotations exported to pretty‑printed XFDF: {outputXfdfPath}");
    }
}