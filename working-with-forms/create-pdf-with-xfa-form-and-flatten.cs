using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "XfaFlattened.pdf";

        // Create a new PDF document and add a page.
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Create a text box field (AcroForm) on the page.
            // Rectangle constructor: (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 600, 300, 630);
            TextBoxField textField = new TextBoxField(page, fieldRect);
            textField.PartialName = "SampleField";
            textField.Value = "Initial Value";

            // Add the field to the document's form.
            doc.Form.Add(textField);

            // Prepare a minimal XFA data packet.
            // This XFA XML contains the same field name so that the data binds correctly.
            string xfaXml = @"<?xml version='1.0'?>
                <xfa:datasets xmlns:xfa='http://www.xfa.org/schema/xfa-data/1.0/'>
                    <xfa:data>
                        <SampleField>Initial Value from XFA</SampleField>
                    </xfa:data>
                </xfa:datasets>";

            XmlDocument xfaDoc = new XmlDocument();
            xfaDoc.LoadXml(xfaXml);

            // Assign the XFA data to the form.
            doc.Form.AssignXfa(xfaDoc);

            // Flatten the form: removes interactive fields and places their values directly on the page.
            doc.Form.Flatten();

            // Save the resulting PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with XFA fields flattened saved to '{outputPath}'.");
    }
}