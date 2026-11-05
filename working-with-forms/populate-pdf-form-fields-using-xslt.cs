using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Xsl;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string xmlPath = "data.xml";
        const string xsltPath = "transform.xslt";
        const string pdfTemplatePath = "template.pdf";
        const string outputPdfPath = "filled.pdf";

        // Verify required files exist
        if (!File.Exists(xmlPath) || !File.Exists(xsltPath) || !File.Exists(pdfTemplatePath))
        {
            Console.Error.WriteLine("One or more input files are missing.");
            return;
        }

        // Load the XSLT stylesheet
        XslCompiledTransform xslt = new XslCompiledTransform();
        xslt.Load(xsltPath);

        // Transform the source XML and capture the result in memory
        using (MemoryStream transformedStream = new MemoryStream())
        {
            using (XmlReader xmlReader = XmlReader.Create(xmlPath))
            {
                xslt.Transform(xmlReader, null, transformedStream);
            }

            // Prepare the transformed XML for reading
            transformedStream.Position = 0;
            XDocument transformedDoc = XDocument.Load(transformedStream);

            // Load the PDF template inside a using block (deterministic disposal)
            using (Document pdfDoc = new Document(pdfTemplatePath))
            {
                // Populate PDF form fields with values from the transformed XML
                foreach (XElement element in transformedDoc.Root.Elements())
                {
                    string fieldName = element.Name.LocalName;
                    string fieldValue = element.Value;

                    // The Form indexer returns a WidgetAnnotation; cast it to Field before using.
                    Field? pdfField = pdfDoc.Form[fieldName] as Field;
                    if (pdfField != null)
                    {
                        pdfField.Value = fieldValue;
                    }
                }

                // Save the resulting PDF
                pdfDoc.Save(outputPdfPath);
            }
        }

        Console.WriteLine($"PDF successfully saved to '{outputPdfPath}'.");
    }
}
