using System;
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string xmlPath = "formdata.xml"; // Xfdf (XML) output
        const string xsltPath = "form2html.xslt";
        const string htmlPath = "formview.html";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(xsltPath))
        {
            Console.Error.WriteLine($"XSLT file not found: {xsltPath}");
            return;
        }

        // Export form fields to Xfdf (XML) using the Form facade
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Enable automatic recalculation of form fields
            pdfDoc.Form.AutoRecalculate = true;

            using (Form formFacade = new Form(pdfDoc))
            using (FileStream xmlStream = new FileStream(xmlPath, FileMode.Create, FileAccess.Write))
            {
                // Export form data to XML (Xfdf format)
                formFacade.ExportXfdf(xmlStream);
            }
        }

        // Transform the exported XML to HTML using XSLT
        XslCompiledTransform xslt = new XslCompiledTransform();
        xslt.Load(xsltPath);

        using (FileStream xmlInput = new FileStream(xmlPath, FileMode.Open, FileAccess.Read))
        using (XmlReader xmlReader = XmlReader.Create(xmlInput))
        using (FileStream htmlOutput = new FileStream(htmlPath, FileMode.Create, FileAccess.Write))
        using (XmlWriter htmlWriter = XmlWriter.Create(htmlOutput, xslt.OutputSettings))
        {
            xslt.Transform(xmlReader, htmlWriter);
        }

        Console.WriteLine($"Form data exported to XML ({xmlPath}) and transformed to HTML ({htmlPath}).");
    }
}
