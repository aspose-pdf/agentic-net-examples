using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the source PDF (with XFA form) and the XML data file
        const string pdfPath = "input_with_xfa.pdf";
        const string xmlPath = "xfa_data.xml";
        const string outputPath = "output_filled.pdf";

        // Verify that the required files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        try
        {
            // Load the XML data into an XmlDocument
            XmlDocument xfaData = new XmlDocument();
            xfaData.Load(xmlPath);

            // Open the PDF document that contains an XFA form
            using (Document pdfDoc = new Document(pdfPath))
            {
                // Ensure the document actually has an XFA form
                if (!pdfDoc.Form.HasXfa)
                {
                    Console.Error.WriteLine("The PDF does not contain an XFA form.");
                    return;
                }

                // Assign the XFA data to the form
                pdfDoc.Form.AssignXfa(xfaData);

                // Save the updated PDF
                pdfDoc.Save(outputPath);
            }

            Console.WriteLine($"XFA data imported successfully. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}