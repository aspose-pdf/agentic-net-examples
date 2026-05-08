using System;
using System.IO;
using System.Net.Http;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main(string[] args)
    {
        // Paths to the source PDF and the destination PDF
        const string inputPdfPath  = "template.pdf";
        const string outputPdfPath = "filled.pdf";

        // URL of the XML (XFDF) form data to import
        const string xmlDataUrl = "https://example.com/formdata.xfdf";

        // Validate input files
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Download the XML form data as a stream
                using (HttpClient httpClient = new HttpClient())
                using (Stream xmlStream = httpClient.GetStreamAsync(xmlDataUrl).GetAwaiter().GetResult())
                {
                    // Import field values from the XFDF (XML) stream into the PDF
                    XfdfReader.ReadFields(xmlStream, pdfDoc);
                }

                // Save the updated PDF
                pdfDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"Form data imported and saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}