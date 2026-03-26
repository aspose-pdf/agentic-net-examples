using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Xml.Xsl;

class Program
{
    static void Main()
    {
        const string pdfPath = "form.pdf";
        const string xmlPath = "formdata.xml";
        const string xslPath = "formdata.xsl";
        const string htmlPath = "formdata.html";

        // Verify that the source PDF exists before attempting any operation.
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file '{pdfPath}' not found.");
            return;
        }

        // Export form fields to XML.
        var pdfForm = new Form(pdfPath);
        using (var xmlStream = new FileStream(xmlPath, FileMode.Create, FileAccess.Write))
        {
            pdfForm.ExportXml(xmlStream);
        }

        // Transform XML to HTML using XSLT.
        var xslt = new XslCompiledTransform();
        xslt.Load(xslPath);
        // Use the string‑path overload for Transform – it avoids the need for manual XmlReader/Writer handling.
        xslt.Transform(xmlPath, htmlPath);

        Console.WriteLine("Form data exported to XML and transformed to HTML.");
    }
}
