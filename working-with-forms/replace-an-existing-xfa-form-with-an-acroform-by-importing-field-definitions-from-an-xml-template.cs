using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class ReplaceXfaWithAcroForm
{
    static void Main()
    {
        const string inputPdfPath   = "input_with_xfa.pdf";
        const string xmlTemplatePath = "acroform_template.xml";
        const string outputPdfPath  = "output_acroform.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(xmlTemplatePath))
        {
            Console.Error.WriteLine($"XML template not found: {xmlTemplatePath}");
            return;
        }

        // Load the source PDF (contains an XFA form)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Verify that the document actually contains an XFA form
            if (pdfDoc.Form.HasXfa)
            {
                // Load the XML template that defines the AcroForm fields
                XmlDocument xmlTemplate = new XmlDocument();
                xmlTemplate.Load(xmlTemplatePath);

                // Assign the XML data to the form.
                // This replaces the existing XFA data with the new definition.
                // After assignment, the form type becomes Standard (AcroForm).
                pdfDoc.Form.AssignXfa(xmlTemplate);

                // Optionally, force the form to be treated as a standard AcroForm
                pdfDoc.Form.Type = FormType.Standard;
            }
            else
            {
                Console.WriteLine("The source PDF does not contain an XFA form; no replacement performed.");
            }

            // Save the modified PDF (AcroForm)
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with AcroForm: {outputPdfPath}");
    }
}