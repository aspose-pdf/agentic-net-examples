using System;
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string xmlPath      = "input.xml";          // source XML
        const string xslPath      = "transform.xsl";      // XSLT stylesheet
        const string pdfTemplate  = "template.pdf";       // PDF with form fields
        const string outputPdf    = "filled_output.pdf";  // result PDF

        // Verify files exist
        if (!File.Exists(xmlPath) || !File.Exists(xslPath) || !File.Exists(pdfTemplate))
        {
            Console.Error.WriteLine("One or more input files are missing.");
            return;
        }

        // ------------------------------------------------------------
        // 1. Transform the XML using the XSLT stylesheet.
        // ------------------------------------------------------------
        XslCompiledTransform xslt = new XslCompiledTransform();
        xslt.Load(xslPath);

        // The transformation result will be stored in a memory stream as XML.
        using (MemoryStream transformedXmlStream = new MemoryStream())
        {
            using (XmlReader xmlReader = XmlReader.Create(xmlPath))
            using (XmlWriter xmlWriter = XmlWriter.Create(transformedXmlStream, xslt.OutputSettings))
            {
                xslt.Transform(xmlReader, xmlWriter);
                xmlWriter.Flush();
                transformedXmlStream.Position = 0; // rewind for reading
            }

            // ------------------------------------------------------------
            // 2. Load the PDF template.
            // ------------------------------------------------------------
            using (Document pdfDoc = new Document(pdfTemplate))
            {
                // ------------------------------------------------------------
                // 3. Populate form fields with values from the transformed XML.
                // ------------------------------------------------------------
                // The core API provides a simple way to import XML data into a PDF form:
                // BindXml reads the XML and maps element names to form field names.
                // It works for AcroForm fields.
                pdfDoc.BindXml(transformedXmlStream);

                // If additional manual field handling is required, you can access fields like this:
                // Aspose.Pdf.Forms.Form form = pdfDoc.Form;
                // if (form["CustomerName"] != null)
                //     form["CustomerName"].SetValue("John Doe");

                // ------------------------------------------------------------
                // 4. Save the populated PDF.
                // ------------------------------------------------------------
                pdfDoc.Save(outputPdf);
            }
        }

        Console.WriteLine($"PDF generated successfully: {outputPdf}");
    }
}