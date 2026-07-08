using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Xsl;
using Aspose.Pdf;
using Aspose.Pdf.Facades;          // Facade classes (Form, PdfFileSignature, …)
using Aspose.Pdf.Forms;            // Form field classes (TextBoxField, …)

class Program
{
    static void Main()
    {
        const string pdfPath   = "input.pdf";          // PDF with AcroForm
        const string xmlPath   = "formData.xml";       // Intermediate XML file
        const string xsltPath  = "formToHtml.xslt";    // XSLT that creates HTML
        const string htmlPath  = "output.html";        // Resulting HTML file

        // ------------------------------------------------------------
        // Ensure a sample PDF with a simple form exists (self‑contained).
        // ------------------------------------------------------------
        if (!File.Exists(pdfPath))
        {
            // Create a new PDF document.
            using (Document doc = new Document())
            {
                // Add a page.
                Page page = doc.Pages.Add();

                // Define a rectangle for the text box field (left, bottom, right, top).
                // NOTE: Aspose.Pdf.Rectangle is used explicitly to avoid ambiguity with Aspose.Pdf.Drawing.Rectangle.
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 300, 720);

                // Create a text box field, give it a name and a default value.
                TextBoxField txtField = new TextBoxField(page, rect)
                {
                    PartialName = "SampleName",
                    Value = "John Doe"
                };

                // Add the field to the document's form collection.
                doc.Form.Add(txtField);

                // Save the PDF so that the later Form logic can work on it.
                doc.Save(pdfPath);
            }
        }

        // ------------------------------------------------------------
        // Export form fields to XML.
        // ------------------------------------------------------------
        // Use the Facade Form class (Aspose.Pdf.Facades.Form) – fully qualified to avoid the ambiguous reference with Aspose.Pdf.Forms.Form.
        using (Aspose.Pdf.Facades.Form pdfForm = new Aspose.Pdf.Facades.Form(pdfPath))
        {
            // ExportXml writes the form data into the provided stream.
            using (FileStream xmlStream = new FileStream(xmlPath, FileMode.Create, FileAccess.Write))
            {
                pdfForm.ExportXml(xmlStream);
            }
        }

        // ------------------------------------------------------------
        // Ensure the XSLT file exists – create a minimal one if it does not.
        // ------------------------------------------------------------
        if (!File.Exists(xsltPath))
        {
            string xsltContent = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n" +
                                 "<xsl:stylesheet version=\"1.0\" xmlns:xsl=\"http://www.w3.org/1999/XSL/Transform\">\n" +
                                 "  <xsl:output method=\"html\" encoding=\"UTF-8\" indent=\"yes\"/>\n" +
                                 "  <xsl:template match=\"/\">\n" +
                                 "    <html><head><title>Form Data</title></head><body>\n" +
                                 "      <h1>Form Data</h1>\n" +
                                 "      <table border=\"1\">\n" +
                                 "        <xsl:for-each select=\"//field\">\n" +
                                 "          <tr>\n" +
                                 "            <td><xsl:value-of select=\"@name\"/></td>\n" +
                                 "            <td><xsl:value-of select=\".\"/></td>\n" +
                                 "          </tr>\n" +
                                 "        </xsl:for-each>\n" +
                                 "      </table>\n" +
                                 "    </body></html>\n" +
                                 "  </xsl:template>\n" +
                                 "</xsl:stylesheet>";
            File.WriteAllText(xsltPath, xsltContent, Encoding.UTF8);
        }

        // ------------------------------------------------------------
        // Transform the exported XML to HTML using the supplied XSLT.
        // ------------------------------------------------------------
        XslCompiledTransform transformer = new XslCompiledTransform();
        transformer.Load(xsltPath);

        using (FileStream xmlInput  = new FileStream(xmlPath, FileMode.Open, FileAccess.Read))
        using (FileStream htmlOut   = new FileStream(htmlPath, FileMode.Create, FileAccess.Write))
        using (XmlReader   xmlReader = XmlReader.Create(xmlInput))
        using (XmlWriter   htmlWriter = XmlWriter.Create(htmlOut, transformer.OutputSettings))
        {
            transformer.Transform(xmlReader, htmlWriter);
        }

        Console.WriteLine($"Form data exported to XML and transformed to HTML: {htmlPath}");
    }
}
