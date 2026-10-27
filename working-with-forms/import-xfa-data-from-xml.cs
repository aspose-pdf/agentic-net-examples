using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath = "form.pdf";
        const string xmlDataPath = "data.xml";
        const string outputPath = "filled_form.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        if (!File.Exists(xmlDataPath))
        {
            Console.Error.WriteLine($"XML data file not found: {xmlDataPath}");
            return;
        }

        try
        {
            // Load the PDF that contains an XFA form
            using (Document pdfDoc = new Document(pdfPath))
            {
                // Verify that the document actually has an XFA form
                if (pdfDoc.Form != null && pdfDoc.Form.HasXfa)
                {
                    // Load the XML data
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(xmlDataPath);

                    // Access the XFA object (note the upper‑case XFA)
                    XFA xfa = pdfDoc.Form.XFA;

                    // Replace the datasets node with the data from the XML file
                    // This effectively imports the XML data into the XFA form
                    if (xfa.Datasets != null && xmlDoc.DocumentElement != null)
                    {
                        // Preserve the original <datasets> wrapper and inject the child nodes
                        xfa.Datasets.InnerXml = xmlDoc.DocumentElement.InnerXml;
                    }
                }
                else
                {
                    Console.Error.WriteLine("The PDF does not contain an XFA form.");
                }

                // Save the PDF with the imported data
                pdfDoc.Save(outputPath);
            }

            Console.WriteLine($"XFA data imported and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
