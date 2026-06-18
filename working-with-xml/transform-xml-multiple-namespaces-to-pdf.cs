using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        // Paths to the source XML, the XSLT stylesheet and the desired PDF output.
        const string xmlPath   = "input.xml";
        const string xslPath   = "transform.xsl";
        const string outputPdf = "output.pdf";

        // Verify that the required files exist.
        if (!File.Exists(xmlPath) || !File.Exists(xslPath))
        {
            Console.Error.WriteLine("XML or XSLT file not found.");
            return;
        }

        // XmlLoadOptions can be constructed with the XSLT file.
        // The XSLT should declare all namespaces it uses (e.g., xmlns:ns1="...", xmlns:ns2="...")
        // Aspose.Pdf will apply the transformation respecting those namespaces,
        // thereby avoiding conflicts when multiple XML namespaces are present.
        XmlLoadOptions loadOptions = new XmlLoadOptions(xslPath);

        // If the stylesheet requires parameters, they can be supplied via XsltArgumentList:
        // loadOptions.XsltArgumentList = new System.Xml.Xsl.XsltArgumentList();
        // loadOptions.XsltArgumentList.AddParam("paramName", "", "value");

        // Load the XML, apply the XSLT, and create a PDF document.
        using (Document pdfDoc = new Document(xmlPath, loadOptions))
        {
            // Save the resulting PDF.
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF successfully created at '{outputPdf}'.");
    }
}