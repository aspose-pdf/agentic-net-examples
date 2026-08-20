using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string xmlDataPath    = "formData.xml";
        const string outputPdfPath  = "output.pdf";

        // Verify that the source PDF exists.
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found – '{inputPdfPath}'.");
            return;
        }

        // Load the PDF document.
        try
        {
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Check for the XML import file.
                if (File.Exists(xmlDataPath))
                {
                    // Load the XML into an XmlDocument.
                    XmlDocument xmlDoc = new XmlDocument();
                    try
                    {
                        xmlDoc.Load(xmlDataPath);
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine($"Error loading XML file '{xmlDataPath}': {ex.Message}");
                        // Proceed without importing XML data.
                        xmlDoc = null;
                    }

                    // If the XML was loaded successfully, assign it to the XFA form.
                    if (xmlDoc != null)
                    {
                        try
                        {
                            pdfDoc.Form.AssignXfa(xmlDoc);
                            Console.WriteLine("XML form data imported successfully.");
                        }
                        catch (Exception ex)
                        {
                            Console.Error.WriteLine($"Error assigning XFA data: {ex.Message}");
                        }
                    }
                }
                else
                {
                    // XML file is missing – handle gracefully.
                    Console.WriteLine($"Warning: XML import file '{xmlDataPath}' not found. Skipping form data import.");
                }

                // Save the modified PDF.
                pdfDoc.Save(outputPdfPath);
                Console.WriteLine($"PDF saved to '{outputPdfPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}