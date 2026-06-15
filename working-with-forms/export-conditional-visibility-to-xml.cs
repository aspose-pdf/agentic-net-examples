using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputXml = "output.xml";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Ensure all annotations are visible (remove Hidden flag if set)
            foreach (Page page in pdfDoc.Pages)
            {
                foreach (Annotation ann in page.Annotations)
                {
                    // Clear the Hidden flag to make the annotation visible
                    if (ann.Flags.HasFlag(AnnotationFlags.Hidden))
                    {
                        ann.Flags &= ~AnnotationFlags.Hidden;
                    }
                }
            }

            // Export the document model (including annotation flags) to XML
            XmlSaveOptions xmlOpts = new XmlSaveOptions(); // default options
            pdfDoc.Save(outputXml, xmlOpts);
        }

        Console.WriteLine($"Conditional visibility state exported to XML: {outputXml}");
    }
}
