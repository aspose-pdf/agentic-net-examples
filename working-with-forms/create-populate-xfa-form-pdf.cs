using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths for the template PDF (must contain an XFA form) and the result PDF
        const string templatePath = "XfaTemplate.pdf";
        const string outputPath   = "XfaFilled.pdf";

        // Optional flag to enable XFA processing
        bool enableXfa = true;

        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template PDF not found: {templatePath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(templatePath))
        {
            // Access the form object (core API)
            Form pdfForm = pdfDoc.Form;

            // Verify that the document actually contains an XFA form
            if (enableXfa && pdfForm.HasXfa)
            {
                // Build an XmlDocument with the data to populate the XFA fields
                // The XML structure must match the XFA form's data schema.
                XmlDocument xfaData = new XmlDocument();

                // Example XML – replace with your actual XFA data structure
                // <xfa:datasets xmlns:xfa="http://www.xfa.org/schema/xfa-data/1.0/">
                //   <xfa:data>
                //     <FirstName>John</FirstName>
                //     <LastName>Doe</LastName>
                //     <Email>john.doe@example.com</Email>
                //   </xfa:data>
                // </xfa:datasets>
                string xmlContent =
@"<?xml version=""1.0"" encoding=""UTF-8""?>
<xfa:datasets xmlns:xfa=""http://www.xfa.org/schema/xfa-data/1.0/"">
  <xfa:data>
    <FirstName>John</FirstName>
    <LastName>Doe</LastName>
    <Email>john.doe@example.com</Email>
  </xfa:data>
</xfa:datasets>";

                xfaData.Load(new StringReader(xmlContent));

                // Assign the XFA data to the form
                pdfForm.AssignXfa(xfaData);
            }
            else
            {
                Console.WriteLine("XFA feature is disabled or the document does not contain an XFA form.");
            }

            // Save the updated PDF
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}