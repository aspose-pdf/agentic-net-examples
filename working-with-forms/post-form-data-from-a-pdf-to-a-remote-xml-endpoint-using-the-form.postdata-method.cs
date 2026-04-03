using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

class Program
{
    // Entry point
    static async Task Main()
    {
        const string pdfPath = "input.pdf";          // source PDF with form fields
        const string endpointUrl = "https://example.com/receive"; // remote XML endpoint

        // Ensure the PDF file exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // 1. Bind the PDF to the Form facade
        using (Form form = new Form(pdfPath))
        {
            // 2. Export the form data to an in‑memory XML stream
            using (MemoryStream xmlStream = new MemoryStream())
            {
                form.ExportXml(xmlStream);
                xmlStream.Position = 0; // rewind for reading

                // 3. Read the XML content as a string
                string xmlContent;
                using (StreamReader reader = new StreamReader(xmlStream, Encoding.UTF8, true, 1024, leaveOpen: true))
                {
                    xmlContent = reader.ReadToEnd();
                }

                // 4. Post the XML to the remote endpoint
                using (HttpClient httpClient = new HttpClient())
                {
                    HttpContent httpContent = new StringContent(xmlContent, Encoding.UTF8, "application/xml");
                    try
                    {
                        HttpResponseMessage response = await httpClient.PostAsync(endpointUrl, httpContent);
                        response.EnsureSuccessStatusCode();
                        Console.WriteLine($"Form data posted successfully. Server responded with {(int)response.StatusCode} {response.ReasonPhrase}");
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine($"Error posting form data: {ex.Message}");
                    }
                }
            }
        }
    }
}