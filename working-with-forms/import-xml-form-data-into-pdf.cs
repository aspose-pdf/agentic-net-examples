using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    // Entry point
    static async Task Main(string[] args)
    {
        // Paths to the source PDF (with form fields) and the output PDF
        const string sourcePdfPath = "template.pdf";
        const string outputPdfPath = "filled.pdf";

        // URL of the XML form data (served over HTTP)
        const string xmlDataUrl = "https://example.com/formdata.xml";

        // Ensure the source PDF exists
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }

        // Create an HttpClient to fetch the XML data stream
        using (HttpClient httpClient = new HttpClient())
        {
            try
            {
                // Obtain the XML data as a stream from the network
                using (Stream xmlStream = await httpClient.GetStreamAsync(xmlDataUrl))
                // Initialize the Form facade with input and output PDF files
                using (Aspose.Pdf.Facades.Form form = new Aspose.Pdf.Facades.Form(sourcePdfPath, outputPdfPath))
                {
                    // Import the XML form data into the PDF form fields
                    form.ImportXml(xmlStream);

                    // Save the resulting PDF with populated fields
                    form.Save();
                }

                Console.WriteLine($"Form data imported and saved to '{outputPdfPath}'.");
            }
            catch (HttpRequestException httpEx)
            {
                Console.Error.WriteLine($"Error fetching XML data: {httpEx.Message}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Unexpected error: {ex.Message}");
            }
        }
    }
}