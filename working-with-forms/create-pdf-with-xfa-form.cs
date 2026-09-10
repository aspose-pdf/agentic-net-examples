using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths for the XFA template XML and the output PDF
        const string xfaTemplatePath = "template.xfa";
        const string outputPdfPath   = "output_with_xfa.pdf";

        // Ensure the XFA template file exists
        if (!File.Exists(xfaTemplatePath))
        {
            Console.Error.WriteLine($"XFA template not found: {xfaTemplatePath}");
            return;
        }

        // Load the XFA XML document
        XmlDocument xfaXml = new XmlDocument();
        xfaXml.Load(xfaTemplatePath);

        // Create a new PDF document and add a blank page (required for a PDF container)
        using (Document pdfDoc = new Document())
        {
            pdfDoc.Pages.Add();

            // Assign the XFA data to the form
            pdfDoc.Form.AssignXfa(xfaXml);

            // Populate XFA fields (example field names must exist in the XFA template)
            if (pdfDoc.Form.HasXfa)
            {
                // The indexer returns a generic WidgetAnnotation. Cast to the concrete field type
                // (e.g., TextBoxField) and set its Value property.
                if (pdfDoc.Form["FirstName"] is TextBoxField firstNameField)
                {
                    firstNameField.Value = "John"; // Use Value instead of Text
                }

                if (pdfDoc.Form["LastName"] is TextBoxField lastNameField)
                {
                    lastNameField.Value = "Doe"; // Use Value instead of Text
                }
            }

            // Save the PDF containing the XFA form
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with XFA form saved to '{outputPdfPath}'.");
    }
}
