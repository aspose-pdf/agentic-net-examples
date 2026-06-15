using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string endpointUrl = "https://example.com/submit";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document pdfDocument = new Document(pdfPath))
            {
                // OPTIONAL: fill form fields if needed
                // pdfDocument.Form.FillField("FieldName", "Value");

                // Export form data to an in‑memory XML stream
                using (MemoryStream xmlStream = new MemoryStream())
                {
                    using (Form formFacade = new Form())
                    {
                        formFacade.BindPdf(pdfDocument);
                        formFacade.ExportXml(xmlStream);
                    }

                    // Reset stream position before reading
                    xmlStream.Position = 0;

                    // POST the XML to the remote endpoint
                    using (HttpClient httpClient = new HttpClient())
                    {
                        using (StreamContent content = new StreamContent(xmlStream))
                        {
                            content.Headers.ContentType = new MediaTypeHeaderValue("application/xml");
                            HttpResponseMessage response = httpClient
                                .PostAsync(endpointUrl, content)
                                .GetAwaiter()
                                .GetResult();

                            Console.WriteLine($"POST response status: {response.StatusCode}");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}