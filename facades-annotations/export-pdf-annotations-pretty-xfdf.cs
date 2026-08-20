using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using System.Xml.Linq;

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

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Initialize the annotation editor and bind the loaded document
            using (PdfAnnotationEditor annotEditor = new PdfAnnotationEditor())
            {
                annotEditor.BindPdf(pdfDoc);

                // Export all annotations to a memory stream
                using (MemoryStream tempStream = new MemoryStream())
                {
                    annotEditor.ExportAnnotationsToXfdf(tempStream);
                    tempStream.Position = 0; // Reset stream for reading

                    // Load the XFDF XML from the stream
                    XDocument xfdfXml = XDocument.Load(tempStream);

                    // Save the XML with indentation (pretty‑printed) to the target file
                    using (FileStream outFile = new FileStream(outputXfdfPath, FileMode.Create, FileAccess.Write))
                    {
                        xfdfXml.Save(outFile); // Default formatting includes indentation
                    }
                }
            }
        }

        Console.WriteLine($"Annotations exported to pretty XFDF file: {outputXfdfPath}");
    }
}