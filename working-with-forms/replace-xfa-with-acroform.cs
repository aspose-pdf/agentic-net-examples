using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input_with_xfa.pdf";
        const string xmlTemplate = "field_template.xml";
        const string outputPdf = "output_acroform.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(xmlTemplate))
        {
            Console.Error.WriteLine($"XML template not found: {xmlTemplate}");
            return;
        }

        // Load the PDF document that contains an XFA form
        using (Document doc = new Document(inputPdf))
        {
            // Load the XML template which defines the desired AcroForm fields
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlTemplate);

            // Replace the existing XFA data with the new XML definition
            doc.Form.AssignXfa(xmlDoc);

            // Convert the form to a standard AcroForm (static form)
            doc.Form.Type = FormType.Standard;

            // Save the PDF now containing an AcroForm
            doc.Save(outputPdf);
        }

        Console.WriteLine($"AcroForm PDF saved to '{outputPdf}'.");
    }
}