using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string xmlPath = "interaction_log.xml";
        const string xfdfPath = "annotations.xfdf";
        const string outputPdf = "reconstructed_viewer.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load the XML interaction log into a PDF document using XmlLoadOptions
        using (Document doc = new Document(xmlPath, new XmlLoadOptions()))
        {
            // If an XFDF file with annotations exists, import them into the document
            if (File.Exists(xfdfPath))
            {
                doc.ImportAnnotationsFromXfdf(xfdfPath);
            }

            // Save the reconstructed PDF for testing the viewer behavior
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Reconstructed PDF saved to '{outputPdf}'.");
    }
}