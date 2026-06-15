using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Aspose.Pdf; // Document class is in this namespace

class ExportAnnotationsAndUpload
{
    // Entry point
    static async Task Main(string[] args)
    {
        // Validate arguments: args[0] = input PDF path, args[1] = REST endpoint URL
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: ExportAnnotationsAndUpload <input.pdf> <endpointUrl>");
            return;
        }

        string inputPdfPath = args[0];
        string endpointUrl  = args[1];

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Temporary XFDF file path (in the same folder as the input PDF)
        string xfdfPath = Path.ChangeExtension(inputPdfPath, ".xfdf");

        try
        {
            // Load the PDF document (using the proper load constructor)
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Export all annotations to XFDF file
                pdfDoc.ExportAnnotationsToXfdf(xfdfPath);
            }

            // Read the generated XFDF file into a stream for upload
            using (FileStream xfdfStream = File.OpenRead(xfdfPath))
            using (HttpClient httpClient = new HttpClient())
            using (MultipartFormDataContent content = new MultipartFormDataContent())
            {
                // Prepare the XFDF content as a stream content
                StreamContent xfdfContent = new StreamContent(xfdfStream);
                xfdfContent.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.adobe.xfdf");
                // The name "file" can be adjusted to match the server's expected form field name
                content.Add(xfdfContent, "file", Path.GetFileName(xfdfPath));

                // POST the multipart content to the REST endpoint
                HttpResponseMessage response = await httpClient.PostAsync(endpointUrl, content);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("XFDF uploaded successfully.");
                }
                else
                {
                    Console.Error.WriteLine($"Upload failed. Status: {(int)response.StatusCode} {response.ReasonPhrase}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            // Clean up the temporary XFDF file if desired
            if (File.Exists(xfdfPath))
            {
                try { File.Delete(xfdfPath); } catch { /* ignore cleanup errors */ }
            }
        }
    }
}