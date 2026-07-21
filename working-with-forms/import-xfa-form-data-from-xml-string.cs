using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the output PDF
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";

        // XML string containing the form data to be imported
        const string formDataXml = @"
            <xfa:datasets xmlns:xfa='http://www.xfa.org/schema/xfa-data/1.0/'>
                <xfa:data>
                    <form>
                        <FirstName>John</FirstName>
                        <LastName>Doe</LastName>
                        <Email>john.doe@example.com</Email>
                    </form>
                </xfa:data>
            </xfa:datasets>";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Load the XML string into an XmlDocument
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(formDataXml);

                // Assign the XFA data to the form (core API, no Facades)
                pdfDoc.Form.AssignXfa(xmlDoc);

                // Save the updated PDF
                pdfDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"Form data imported and saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}