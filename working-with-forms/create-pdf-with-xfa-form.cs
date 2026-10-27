using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
#if XFA_ENABLED
using Aspose.Pdf.Xfa;
#endif

class Program
{
    static void Main()
    {
        const string outputPath = "xfa_form.pdf";

        // Create a new PDF document and ensure deterministic disposal.
        using (Document doc = new Document())
        {
#if XFA_ENABLED
            // Create an XFA form instance associated with the document.
            // The XfaForm class is located in the Aspose.Pdf.Xfa namespace.
            XfaForm xfaForm = new XfaForm(doc);

            // Build a minimal XFA XML structure with two fields: "FirstName" and "LastName".
            string xfaXml = @"
                <xfa:datasets xmlns:xfa='http://www.xfa.org/schema/xfa-data/1.0/'>
                    <xfa:data>
                        <FirstName></FirstName>
                        <LastName></LastName>
                    </xfa:data>
                </xfa:datasets>";

            // Load the XML into an XmlDocument and assign it to the XFA form.
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xfaXml);
            xfaForm.DomDocument = xmlDoc;

            // OPTIONAL: Verify that the XFA form was created before populating fields.
            if (xfaForm != null)
            {
                // Populate the XFA fields with sample data.
                xfaForm.SetFieldValue("FirstName", "John");
                xfaForm.SetFieldValue("LastName", "Doe");
            }
#endif
            // Save the PDF (with or without XFA content).
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with XFA form saved to '{outputPath}'.");
    }
}
