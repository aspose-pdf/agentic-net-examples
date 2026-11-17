using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Aspose.Pdf; // Core PDF API

class Program
{
    // Entry point
    static async Task Main()
    {
        const string pdfPath      = "input.pdf";          // Source PDF with annotations
        const string xfdfPath     = "annotations.xfdf";   // Temporary XFDF file
        const string apiEndpoint  = "https://example.com/api/upload"; // REST endpoint

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // Load the PDF and export its annotations to XFDF
        using (Document doc = new Document(pdfPath))
        {
            // The older Aspose.Pdf versions do not expose SaveFormat.Xfdf.
            // Using the overload that infers the format from the file extension works across all versions.
            doc.Save(xfdfPath);
        }

        // Verify XFDF was created
        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine("Failed to create XFDF file.");
            return;
        }

        // Upload the XFDF file to the REST API
        using (HttpClient client = new HttpClient())
        using (MultipartFormDataContent multipart = new MultipartFormDataContent())
        using (FileStream xfdfStream = File.OpenRead(xfdfPath))
        {
            // Prepare the file content
            StreamContent fileContent = new StreamContent(xfdfStream);
            fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.adobe.xfdf");
            multipart.Add(fileContent, "file", Path.GetFileName(xfdfPath));

            try
            {
                HttpResponseMessage response = await client.PostAsync(apiEndpoint, multipart);
                response.EnsureSuccessStatusCode();
                Console.WriteLine("XFDF uploaded successfully.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Upload failed: {ex.Message}");
            }
        }

        // Clean up temporary XFDF file
        try { File.Delete(xfdfPath); } catch { /* ignore cleanup errors */ }
    }
}
