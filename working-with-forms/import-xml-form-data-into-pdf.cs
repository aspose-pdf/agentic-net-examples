using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using System.Xml;

class Program
{
    // Entry point
    static async Task Main()
    {
        // Paths to the source PDF and the output PDF
        const string sourcePdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";

        // URL of the XML form data (replace with actual endpoint)
        const string xmlDataUrl = "https://example.com/formdata.xml";

        // Validate source PDF existence
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }

        // Download the XML data from the network stream
        XmlDocument xmlDoc = await DownloadXmlAsync(xmlDataUrl);
        if (xmlDoc == null)
        {
            Console.Error.WriteLine("Failed to download or parse XML form data.");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(sourcePdfPath))
        {
            // Assign the XFA data to the form (core API, no Facades)
            pdfDoc.Form.AssignXfa(xmlDoc);

            // Save the updated PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Form data imported and saved to '{outputPdfPath}'.");
    }

    // Helper method to download XML from a URL and load it into an XmlDocument
    private static async Task<XmlDocument> DownloadXmlAsync(string url)
    {
        try
        {
            using (HttpClient client = new HttpClient())
            using (Stream stream = await client.GetStreamAsync(url))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(stream);
                return doc;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error downloading XML: {ex.Message}");
            return null;
        }
    }
}