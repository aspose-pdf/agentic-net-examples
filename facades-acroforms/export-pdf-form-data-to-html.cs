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
        // Paths – adjust as needed
        const string pdfPath   = "input_form.pdf";      // PDF with AcroForm
        const string xsltPath  = "form_to_html.xslt";   // XSLT that converts the exported XML to HTML
        const string htmlPath  = "output.html";         // Resulting HTML file
        const string tempXml   = "temp_form.xml";       // Temporary XML file

        // Ensure source files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(xsltPath))
        {
            Console.Error.WriteLine($"XSLT not found: {xsltPath}");
            return;
        }

        // 1) Load the PDF document
        using (Document pdfDoc = new Document(pdfPath))
        {
            // 2) Bind the PDF to the Form facade
            using (Form form = new Form(pdfDoc))
            {
                // 3) Export form fields to XML (temporary file)
                using (FileStream xmlStream = new FileStream(tempXml, FileMode.Create, FileAccess.Write))
                {
                    form.ExportXml(xmlStream);
                }
            }
        }

        // 4) Load the XSLT stylesheet
        XslCompiledTransform xslt = new XslCompiledTransform();
        xslt.Load(xsltPath);

        // 5) Transform the exported XML into HTML
        using (FileStream xmlInput = new FileStream(tempXml, FileMode.Open, FileAccess.Read))
        using (FileStream htmlOutput = new FileStream(htmlPath, FileMode.Create, FileAccess.Write))
        using (XmlReader xmlReader = XmlReader.Create(xmlInput))
        {
            xslt.Transform(xmlReader, null, htmlOutput);
        }

        // 6) Clean up the temporary XML file
        try { File.Delete(tempXml); } catch { /* ignore cleanup errors */ }

        Console.WriteLine($"Form data exported to HTML: {htmlPath}");
    }
}