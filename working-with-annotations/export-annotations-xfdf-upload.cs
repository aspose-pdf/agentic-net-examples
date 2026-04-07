using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using Aspose.Pdf;

class ExportAnnotationsAndUpload
{
    static void Main()
    {
        // Input PDF file containing annotations
        const string pdfPath = "input.pdf";

        // Temporary XFDF file that will hold the exported annotations
        const string xfdfPath = "annotations.xfdf";

        // REST API endpoint that accepts the XFDF file
        const string apiUrl = "https://example.com/api/upload";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load the PDF document and export its annotations to XFDF
        using (Document doc = new Document(pdfPath))
        {
            // Export all annotations to the XFDF file
            doc.ExportAnnotationsToXfdf(xfdfPath);
        }

        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine($"Failed to create XFDF file: {xfdfPath}");
            return;
        }

        // Upload the XFDF file to the REST API
        using (HttpClient client = new HttpClient())
        using (MultipartFormDataContent multipart = new MultipartFormDataContent())
        {
            // Read the XFDF file into a byte array
            byte[] xfdfBytes = File.ReadAllBytes(xfdfPath);
            ByteArrayContent fileContent = new ByteArrayContent(xfdfBytes);
            // Set the appropriate content type for XFDF
            fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.adobe.xfdf");
            // Add the file content to the multipart request
            multipart.Add(fileContent, "file", Path.GetFileName(xfdfPath));

            // Perform the POST request synchronously
            HttpResponseMessage response = client.PostAsync(apiUrl, multipart).Result;

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("XFDF file uploaded successfully.");
            }
            else
            {
                Console.Error.WriteLine($"Upload failed. Status: {response.StatusCode}");
                string error = response.Content.ReadAsStringAsync().Result;
                Console.Error.WriteLine($"Server response: {error}");
            }
        }

        // Optionally delete the temporary XFDF file
        try
        {
            File.Delete(xfdfPath);
        }
        catch
        {
            // Ignore any errors during cleanup
        }
    }
}