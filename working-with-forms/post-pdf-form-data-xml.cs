using System;
using System.IO;
using System.Net.Http;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms; // <-- added for Field access

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";                 // Path to the PDF containing the form
        const string endpoint = "https://example.com/submit"; // Remote XML endpoint URL

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document to optionally set field values
        using (Document doc = new Document(pdfPath))
        {
            // OPTIONAL: set form field values before posting
            // Use the Field class from Aspose.Pdf.Forms – it exposes the Value property.
            Field nameField = doc.Form["Name"] as Field;
            if (nameField != null)
            {
                nameField.Value = "John Doe";
            }

            // Save the (potentially modified) PDF into a memory stream so the Facade can work with it
            using (MemoryStream pdfStream = new MemoryStream())
            {
                doc.Save(pdfStream);
                pdfStream.Position = 0;

                // Use the Facade Form class to export the form data as XML
                using (Aspose.Pdf.Facades.Form facadeForm = new Aspose.Pdf.Facades.Form())
                {
                    facadeForm.BindPdf(pdfStream);
                    using (MemoryStream xmlStream = new MemoryStream())
                    {
                        facadeForm.ExportXml(xmlStream);
                        xmlStream.Position = 0;

                        // POST the XML to the remote endpoint using HttpClient
                        using (HttpClient client = new HttpClient())
                        {
                            var content = new StreamContent(xmlStream);
                            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/xml");

                            HttpResponseMessage response = client.PostAsync(endpoint, content)
                                                                 .GetAwaiter()
                                                                 .GetResult();

                            Console.WriteLine($"Status: {response.StatusCode}");
                            string responseBody = response.Content.ReadAsStringAsync()
                                                               .GetAwaiter()
                                                               .GetResult();
                            Console.WriteLine("Server response:");
                            Console.WriteLine(responseBody);
                        }
                    }
                }
            }
        }
    }
}
