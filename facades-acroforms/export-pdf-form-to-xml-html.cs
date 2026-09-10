using System;
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string pdfFormPath   = "input_form.pdf";
        const string xmlExportPath = "form_data.xml";
        const string xsltPath      = "form_to_html.xslt";
        const string htmlOutputPath= "form_view.html";

        // Ensure source PDF exists
        if (!File.Exists(pdfFormPath))
        {
            Console.Error.WriteLine($"PDF form not found: {pdfFormPath}");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Export form fields to XML using Aspose.Pdf.Facades.Form
        // -----------------------------------------------------------------
        using (Form form = new Form(pdfFormPath))
        {
            // Export XML to a file (could also use a MemoryStream)
            using (FileStream xmlStream = new FileStream(xmlExportPath, FileMode.Create, FileAccess.Write))
            {
                form.ExportXml(xmlStream);
            }
        }

        // -----------------------------------------------------------------
        // 2. Transform the exported XML to HTML using XSLT
        // -----------------------------------------------------------------
        if (!File.Exists(xmlExportPath))
        {
            Console.Error.WriteLine($"Exported XML not found: {xmlExportPath}");
            return;
        }

        if (!File.Exists(xsltPath))
        {
            Console.Error.WriteLine($"XSLT file not found: {xsltPath}");
            return;
        }

        // Prepare the XSLT processor
        XslCompiledTransform xslt = new XslCompiledTransform();

        // Load the XSLT stylesheet
        xslt.Load(xsltPath);

        // Perform the transformation: XML -> HTML
        using (FileStream xmlInput = new FileStream(xmlExportPath, FileMode.Open, FileAccess.Read))
        using (XmlReader xmlReader = XmlReader.Create(xmlInput))
        using (FileStream htmlOutput = new FileStream(htmlOutputPath, FileMode.Create, FileAccess.Write))
        using (XmlWriter htmlWriter = XmlWriter.Create(htmlOutput, xslt.OutputSettings))
        {
            xslt.Transform(xmlReader, htmlWriter);
        }

        Console.WriteLine($"Form data exported to XML ({xmlExportPath}) and transformed to HTML ({htmlOutputPath}).");
    }
}