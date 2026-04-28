using System;
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths for the source PDF, intermediate XML, XSLT stylesheet and final HTML output
        const string pdfPath   = "input_form.pdf";
        const string xmlPath   = "form_data.xml";
        const string xsltPath  = "form_to_html.xslt";
        const string htmlPath  = "form_view.html";

        // ------------------------------------------------------------
        // 1. Ensure a PDF with a simple AcroForm exists (create if missing)
        // ------------------------------------------------------------
        if (!File.Exists(pdfPath))
        {
            // Create a minimal PDF document containing a single text box field
            var doc = new Document();
            var page = doc.Pages.Add();

            // Define a rectangle for the field (lower‑left X, lower‑left Y, upper‑right X, upper‑right Y)
            // Fully qualify the Rectangle type to avoid ambiguity between Aspose.Pdf and Aspose.Pdf.Drawing
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 600, 300, 650);
            var textBox = new TextBoxField(page, fieldRect)
            {
                PartialName = "SampleTextBox",
                Value = "Default value"
            };
            doc.Form.Add(textBox);
            doc.Save(pdfPath);
            Console.WriteLine($"Created placeholder PDF at '{pdfPath}'.");
        }

        // ------------------------------------------------------------
        // 2. Export AcroForm field data from the PDF to an XML file
        // ------------------------------------------------------------
        // Use the fully‑qualified Form class from Aspose.Pdf.Facades to avoid the ambiguous reference.
        using (Aspose.Pdf.Facades.Form pdfForm = new Aspose.Pdf.Facades.Form(pdfPath))
        {
            // Create (or overwrite) the XML file where the form data will be stored
            using (FileStream xmlStream = new FileStream(xmlPath, FileMode.Create, FileAccess.Write))
            {
                // ExportXml writes the form fields (excluding button values) to the stream
                pdfForm.ExportXml(xmlStream);
            }
        }

        // ------------------------------------------------------------
        // 3. Transform the exported XML into a web‑friendly HTML view using XSLT
        // ------------------------------------------------------------
        // XslCompiledTransform is part of the .NET framework and performs the XSLT transformation.
        // It reads the XML produced in step 2 and applies the stylesheet defined in xsltPath.
        // If the XSLT file does not exist, create a very simple default stylesheet.
        if (!File.Exists(xsltPath))
        {
            const string defaultXslt = @"<?xml version='1.0' encoding='UTF-8'?>
<xsl:stylesheet version='1.0' xmlns:xsl='http://www.w3.org/1999/XSL/Transform'>
  <xsl:output method='html' encoding='UTF-8' indent='yes'/>
  <xsl:template match='/'>
    <html><head><title>Form Data</title></head><body>
      <h2>Exported Form Fields</h2>
      <table border='1' cellpadding='5'>
        <tr><th>Field Name</th><th>Value</th></tr>
        <xsl:for-each select='Form/Field'>
          <tr>
            <td><xsl:value-of select='@Name'/></td>
            <td><xsl:value-of select='@Value'/></td>
          </tr>
        </xsl:for-each>
      </table>
    </body></html>
  </xsl:template>
</xsl:stylesheet>";
            File.WriteAllText(xsltPath, defaultXslt);
            Console.WriteLine($"Created default XSLT stylesheet at '{xsltPath}'.");
        }

        XslCompiledTransform xslt = new XslCompiledTransform();
        xslt.Load(xsltPath);

        // Perform the transformation: XML -> HTML
        using (FileStream xmlInput = new FileStream(xmlPath, FileMode.Open, FileAccess.Read))
        using (XmlReader xmlReader = XmlReader.Create(xmlInput))
        using (FileStream htmlOutput = new FileStream(htmlPath, FileMode.Create, FileAccess.Write))
        using (XmlWriter htmlWriter = XmlWriter.Create(htmlOutput, xslt.OutputSettings))
        {
            xslt.Transform(xmlReader, htmlWriter);
        }

        Console.WriteLine("Form data exported to XML and transformed to HTML successfully.");
    }
}
