using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using System.Xml;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string xmlPath = "data.xml";
        const string outputPath = "output.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML not found: {xmlPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(pdfPath))
            {
                // Load the XML data into an XmlDocument
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlPath);

                // If the PDF contains an XFA form, import the XML data
                if (doc.Form.HasXfa)
                {
                    doc.Form.AssignXfa(xmlDoc);
                }
                else
                {
                    Console.WriteLine("The PDF does not contain an XFA form. No data was imported.");
                }

                // Save the updated PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Form data imported and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}