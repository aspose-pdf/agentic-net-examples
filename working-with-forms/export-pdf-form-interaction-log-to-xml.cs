using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "form.pdf";
        const string outputXmlPath = "interaction_log.xml";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document (required for the Form facade)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Bind the Form facade to the loaded document
            using (Form form = new Form(pdfDoc))
            {
                // Export the form interaction data to XML (XFDF format)
                using (FileStream xmlStream = new FileStream(outputXmlPath, FileMode.Create, FileAccess.Write))
                {
                    form.ExportXml(xmlStream);
                }
            }
        }

        Console.WriteLine($"Interaction log exported to '{outputXmlPath}'.");
    }
}