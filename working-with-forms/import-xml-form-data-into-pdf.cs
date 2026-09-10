using System;
using System.IO;
using System.Net.Http;
using System.Xml;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";
        // Output PDF file path after importing XML form data
        const string outputPdfPath = "output.pdf";
        // URL of the XML form data (could be any network resource)
        const string xmlDataUrl = "https://example.com/formdata.xml";

        // Validate input PDF existence
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Download the XML form data as a stream
        using (HttpClient httpClient = new HttpClient())
        using (Stream xmlStream = httpClient.GetStreamAsync(xmlDataUrl).Result)
        {
            // Load the XML document from the network stream
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlStream);

            // Open the PDF document
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Assign the XFA data (XML) to the form in the PDF
                pdfDoc.Form.AssignXfa(xmlDoc);

                // Save the updated PDF
                pdfDoc.Save(outputPdfPath);
            }
        }

        Console.WriteLine($"Form data imported and saved to '{outputPdfPath}'.");
    }
}