using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string endpoint = "https://example.com/receive.xml";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(pdfPath))
            {
                // Ensure the PDF contains a form
                if (doc.Form == null || doc.Form.Count == 0)
                {
                    Console.WriteLine("No form fields found in the PDF.");
                    return;
                }

                // Build a simple XML representation of the form data (no Facades used)
                XDocument xmlDoc = new XDocument(
                    new XElement("FormData",
                        doc.Form.Fields.Select(field =>
                            new XElement("Field",
                                new XAttribute("Name", field.Name ?? field.FullName ?? string.Empty),
                                new XAttribute("Value", field.Value?.ToString() ?? string.Empty)
                            )
                        )
                    )
                );

                // Write the XML to an in‑memory stream
                using (MemoryStream xmlStream = new MemoryStream())
                {
                    xmlDoc.Save(xmlStream);
                    xmlStream.Position = 0; // rewind for reading

                    // Post the XML to the remote endpoint using HttpClient
                    using (HttpClient client = new HttpClient())
                    using (StreamContent content = new StreamContent(xmlStream))
                    {
                        content.Headers.ContentType = new MediaTypeHeaderValue("application/xml");
                        HttpResponseMessage response = client.PostAsync(endpoint, content)
                            .GetAwaiter()
                            .GetResult();

                        Console.WriteLine($"POST status: {response.StatusCode}");
                        string responseBody = response.Content.ReadAsStringAsync()
                            .GetAwaiter()
                            .GetResult();
                        Console.WriteLine("Server response:");
                        Console.WriteLine(responseBody);
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
