using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using System.Xml;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string xmlDataPath   = "formData.xml";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document using the recommended lifecycle pattern.
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Verify that the XML file exists before attempting to load it.
            if (!File.Exists(xmlDataPath))
            {
                Console.Error.WriteLine($"XML import file not found: {xmlDataPath}");
                // Skip the import step but continue to save the original PDF.
            }
            else
            {
                try
                {
                    // Load the XML into an XmlDocument.
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(xmlDataPath);

                    // Assign the XFA data to the form if the document contains an XFA form.
                    if (pdfDoc.Form.HasXfa)
                    {
                        pdfDoc.Form.AssignXfa(xmlDoc);
                    }
                    else
                    {
                        Console.WriteLine("PDF does not contain an XFA form; XML import skipped.");
                    }
                }
                catch (Exception ex)
                {
                    // Handle any errors that occur during XML loading or assignment.
                    Console.Error.WriteLine($"Error importing XML data: {ex.Message}");
                }
            }

            // Save the (potentially modified) PDF.
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Processing completed. Output saved to '{outputPdfPath}'.");
    }
}