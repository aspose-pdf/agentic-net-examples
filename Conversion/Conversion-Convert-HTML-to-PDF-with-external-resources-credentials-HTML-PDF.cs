using System;
using System.IO;
using System.Net.Http;
using System.Text;
using Aspose.Pdf;

class HtmlToPdfConverter
{
    static void Main(string[] args)
    {
        try
        {
            // Input parameters
            string htmlUrl = "https://example.com/page.html"; // URL of the HTML page
            string basePath = Path.Combine(Directory.GetCurrentDirectory(), "ExternalResources"); // Folder that contains CSS, images, etc.
            string outputPdf = "output.pdf"; // Result PDF file
            string userName = "user"; // Basic‑auth user name
            string password = "password"; // Basic‑auth password

            // Validate output directory
            string outputDir = Path.GetDirectoryName(Path.GetFullPath(outputPdf));
            if (!Directory.Exists(outputDir))
                Directory.CreateDirectory(outputDir);

            // Download HTML content using credentials
            string htmlContent = DownloadHtmlWithCredentials(htmlUrl, userName, password);
            if (string.IsNullOrEmpty(htmlContent))
                throw new InvalidOperationException("Downloaded HTML content is empty.");

            // Load HTML into Aspose.Pdf Document from a memory stream
            using (var htmlStream = new MemoryStream(Encoding.UTF8.GetBytes(htmlContent)))
            {
                var loadOptions = new HtmlLoadOptions(basePath);
                var pdfDocument = new Document(htmlStream, loadOptions);

                // Save the PDF
                pdfDocument.Save(outputPdf);
            }

            Console.WriteLine($"HTML has been successfully converted to PDF: {outputPdf}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Helper method to download HTML using Basic Authentication
    private static string DownloadHtmlWithCredentials(string url, string user, string pass)
    {
        using (var handler = new HttpClientHandler())
        {
            handler.PreAuthenticate = true;
            handler.Credentials = new System.Net.NetworkCredential(user, pass);
            using (var client = new HttpClient(handler))
            {
                var response = client.GetAsync(url).Result;
                response.EnsureSuccessStatusCode();
                return response.Content.ReadAsStringAsync().Result;
            }
        }
    }
}