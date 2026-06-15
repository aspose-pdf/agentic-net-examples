using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath = "template.pdf";
        const string xmlDataPath = "formData.xml";
        const string outputPath = "filled.pdf";

        // Verify the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            // Check if the XML file that contains form data exists
            if (File.Exists(xmlDataPath))
            {
                try
                {
                    // Load the XML into an XmlDocument
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(xmlDataPath);

                    // Apply the XML data to the PDF if it contains an XFA form
                    if (doc.Form.HasXfa)
                    {
                        doc.Form.AssignXfa(xmlDoc);
                        Console.WriteLine("XML form data imported successfully.");
                    }
                    else
                    {
                        Console.WriteLine("The PDF does not contain an XFA form; XML data was not applied.");
                    }
                }
                catch (Exception ex) when (ex is IOException || ex is XmlException)
                {
                    // Handle I/O errors or malformed XML gracefully
                    Console.Error.WriteLine($"Error loading XML file '{xmlDataPath}': {ex.Message}");
                }
            }
            else
            {
                // XML file is missing – log and continue without throwing
                Console.WriteLine($"XML import file not found: {xmlDataPath}. Skipping form data import.");
            }

            // Save the (potentially modified) PDF
            doc.Save(outputPath);
            Console.WriteLine($"Output PDF saved to '{outputPath}'.");
        }
    }
}