using System;
using System.IO;
using System.Net;
using System.Xml;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the output PDF
        const string sourcePdfPath = "template.pdf";
        const string outputPdfPath = "filled.pdf";

        // URL of the XML form data (could be any endpoint returning XML)
        const string xmlDataUrl = "https://example.com/formdata.xml";

        // Validate source PDF existence
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }

        // Download XML data from the network stream
        XmlDocument xmlDoc = new XmlDocument();
        try
        {
            // Create a request to the XML endpoint
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(xmlDataUrl);
            request.Method = "GET";

            // Get the response stream
            using (WebResponse response = request.GetResponse())
            using (Stream responseStream = response.GetResponseStream())
            {
                // Load the XML directly from the network stream
                xmlDoc.Load(responseStream);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to retrieve XML data: {ex.Message}");
            return;
        }

        // Open the PDF, assign the XFA data, and save the result
        try
        {
            using (Document pdfDoc = new Document(sourcePdfPath))
            {
                // Ensure the PDF contains an XFA form before assigning
                if (pdfDoc.Form.HasXfa)
                {
                    // Assign the XML data to the XFA form
                    pdfDoc.Form.AssignXfa(xmlDoc);
                }
                else
                {
                    Console.Error.WriteLine("The PDF does not contain an XFA form.");
                    return;
                }

                // Save the updated PDF
                pdfDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"PDF with imported XML data saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}