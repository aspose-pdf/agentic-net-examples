using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input_with_xfa.pdf";   // PDF that contains an XFA form
        const string xmlTemplatePath = "field_template.xml"; // XML file defining the new AcroForm fields
        const string outputPdfPath  = "output_acroform.pdf";

        // Verify files exist
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

        try
        {
            // Load the PDF document
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Load the XML template that contains the field definitions
                XmlDocument xmlTemplate = new XmlDocument();
                xmlTemplate.Load(xmlTemplatePath);

                // If the document currently contains an XFA form, replace its XFA data
                if (pdfDoc.Form.HasXfa)
                {
                    // Assign the new XFA data (the template) to the form
                    pdfDoc.Form.AssignXfa(xmlTemplate);
                }

                // Convert the form to a standard AcroForm
                pdfDoc.Form.Type = FormType.Standard;

                // Save the resulting PDF with an AcroForm
                pdfDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"AcroForm PDF saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}