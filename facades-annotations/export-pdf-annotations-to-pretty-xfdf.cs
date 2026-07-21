using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputXfdfPath = "annotations_pretty.xfdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Initialize the annotation editor and bind it to the loaded document
            using (PdfAnnotationEditor annotEditor = new PdfAnnotationEditor())
            {
                annotEditor.BindPdf(pdfDoc);

                // Export annotations to a memory stream first
                using (MemoryStream rawXfdfStream = new MemoryStream())
                {
                    annotEditor.ExportAnnotationsToXfdf(rawXfdfStream);
                    rawXfdfStream.Position = 0; // Reset stream position for reading

                    // Load the raw XFDF XML into XDocument for pretty‑printing
                    XDocument xfdfXml = XDocument.Load(rawXfdfStream);

                    // Save the pretty‑printed XFDF to the target file
                    using (FileStream outFile = new FileStream(outputXfdfPath, FileMode.Create, FileAccess.Write))
                    {
                        // XDocument.Save writes indented XML by default
                        xfdfXml.Save(outFile);
                    }
                }
            }
        }

        Console.WriteLine($"Annotations exported and pretty‑printed to '{outputXfdfPath}'.");
    }
}