using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using System.Xml.Linq;

class ExportAnnotationsPretty
{
    static void Main()
    {
        const string pdfPath = "input.pdf";          // source PDF with annotations
        const string xfdfPath = "annotations_pretty.xfdf"; // output pretty‑printed XFDF

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Initialize the annotation editor and bind the loaded document
            PdfAnnotationEditor editor = new PdfAnnotationEditor();
            editor.BindPdf(pdfDoc);

            // Export all annotations to a memory stream (raw XFDF)
            using (MemoryStream rawXfdf = new MemoryStream())
            {
                editor.ExportAnnotationsToXfdf(rawXfdf);
                rawXfdf.Position = 0; // rewind for reading

                // Load the raw XFDF into an XDocument for pretty‑printing
                XDocument xfdfDoc = XDocument.Load(rawXfdf);

                // Save the pretty‑printed XFDF to the target file with indentation
                using (FileStream outStream = new FileStream(xfdfPath, FileMode.Create, FileAccess.Write))
                {
                    // Fully qualify SaveOptions to avoid ambiguity between System.Xml.Linq and Aspose.Pdf
                    xfdfDoc.Save(outStream, System.Xml.Linq.SaveOptions.None);
                }
            }
        }

        Console.WriteLine($"Annotations exported and pretty‑printed to '{xfdfPath}'.");
    }
}