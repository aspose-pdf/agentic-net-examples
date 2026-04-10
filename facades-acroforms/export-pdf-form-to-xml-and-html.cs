using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Xml;
using System.Xml.Xsl;

class Program
{
    static void Main()
    {
        // Paths to the source PDF form, intermediate XML, XSLT stylesheet and final HTML.
        const string pdfPath   = "input_form.pdf";
        const string xmlPath   = "form_data.xml";
        const string xsltPath  = "form_to_html.xslt";
        const string htmlPath  = "form_view.html";

        // ------------------------------------------------------------
        // 1. Export the PDF form fields to an XML file.
        // ------------------------------------------------------------
        // Form implements IDisposable, so wrap it in a using block.
        using (Form pdfForm = new Form(pdfPath))
        // Create the XML output stream.
        using (FileStream xmlStream = new FileStream(xmlPath, FileMode.Create, FileAccess.Write))
        {
            // ExportXml writes the form data into the provided stream.
            pdfForm.ExportXml(xmlStream);
        }

        // ------------------------------------------------------------
        // 2. Transform the exported XML into HTML using XSLT.
        // ------------------------------------------------------------
        // Load the XSLT stylesheet.
        XslCompiledTransform xslt = new XslCompiledTransform();
        xslt.Load(xsltPath);

        // Open the XML source and the HTML destination streams.
        using (FileStream xmlInput  = new FileStream(xmlPath, FileMode.Open, FileAccess.Read))
        using (FileStream htmlOutput = new FileStream(htmlPath, FileMode.Create, FileAccess.Write))
        // Create readers/writers that work with the streams.
        using (XmlReader  xmlReader  = XmlReader.Create(xmlInput))
        using (XmlWriter  htmlWriter = XmlWriter.Create(htmlOutput, xslt.OutputSettings))
        {
            // Perform the transformation.
            xslt.Transform(xmlReader, htmlWriter);
        }

        Console.WriteLine("Form data exported to XML and transformed to HTML successfully.");
    }
}