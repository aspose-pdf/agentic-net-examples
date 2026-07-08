using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths for the template PDF (with XFA form), XFA data XML and the output PDF
        const string templatePath = "template.pdf";
        const string xfaDataPath  = "data.xfa.xml";
        const string outputPath   = "filled_output.pdf";

        // Verify that required files exist
        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template PDF not found: {templatePath}");
            return;
        }
        if (!File.Exists(xfaDataPath))
        {
            Console.Error.WriteLine($"XFA data XML not found: {xfaDataPath}");
            return;
        }

        // Load the PDF document that contains an XFA form
        using (Document pdfDoc = new Document(templatePath))
        {
            // Access the form object
            Form pdfForm = pdfDoc.Form;

            // Ensure the document actually contains an XFA form
            if (!pdfForm.HasXfa)
            {
                Console.Error.WriteLine("The loaded PDF does not contain an XFA form.");
                return;
            }

            // Load the XFA XML data that will replace the existing XFA data
            XmlDocument xfaXml = new XmlDocument();
            xfaXml.Load(xfaDataPath);

            // Assign the new XFA data to the form
            pdfForm.AssignXfa(xfaXml);

            // Example: set values for specific XFA fields.
            // The path syntax follows the XFA data namespace ("data") and field name.
            // Adjust the field names according to your XFA template.
            try
            {
                // Set a text field named "FirstName"
                pdfForm.XFA["data.FirstName"] = "John";

                // Set a text field named "LastName"
                pdfForm.XFA["data.LastName"] = "Doe";

                // Set a numeric field named "Age"
                pdfForm.XFA["data.Age"] = "30";

                // Set a date field named "BirthDate"
                pdfForm.XFA["data.BirthDate"] = "1990-01-01";
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error setting XFA field values: {ex.Message}");
            }

            // Save the modified PDF with populated XFA fields
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"XFA form populated and saved to '{outputPath}'.");
    }
}
