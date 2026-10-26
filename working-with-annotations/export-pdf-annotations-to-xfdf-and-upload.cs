using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Xml;
using Aspose.Pdf; // Document and core PDF types

class Program
{
    // Entry point – async to allow awaiting HTTP calls
    static async Task Main()
    {
        // Folder containing PDFs to process
        const string pdfFolder = "InputPdfs";
        // REST API endpoint that accepts XFDF files
        const string apiEndpoint = "https://example.com/api/uploadXfdf";

        // Validate input folder
        if (!Directory.Exists(pdfFolder))
        {
            Console.Error.WriteLine($"Folder not found: {pdfFolder}");
            return;
        }

        // Create a single HttpClient instance (reused for all uploads)
        using (HttpClient httpClient = new HttpClient())
        {
            // Iterate over each PDF file in the folder
            foreach (string pdfPath in Directory.GetFiles(pdfFolder, "*.pdf"))
            {
                // Export annotations to XFDF
                string xfdfPath = Path.ChangeExtension(pdfPath, ".xfdf");
                ExportAnnotationsToXfdf(pdfPath, xfdfPath);

                // Upload the generated XFDF file
                bool uploadSuccess = await UploadXfdfAsync(httpClient, apiEndpoint, xfdfPath);
                Console.WriteLine($"{Path.GetFileName(xfdfPath)} upload {(uploadSuccess ? "succeeded" : "failed")}");
            }
        }
    }

    // Exports annotations from a PDF file to an XFDF file using only core Aspose.Pdf APIs.
    // The implementation creates a minimal XFDF document that lists each annotation's
    // type, page number and rectangle. If no annotations are present, an empty XFDF
    // skeleton is written. This avoids the need for the optional Aspose.Pdf.Xfdf assembly.
    private static void ExportAnnotationsToXfdf(string pdfPath, string xfdfPath)
    {
        // Ensure the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(pdfPath))
        {
            // Prepare an XML writer for the XFDF file
            using (XmlWriter writer = XmlWriter.Create(xfdfPath, new XmlWriterSettings { Indent = true }))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("xfdf");
                writer.WriteAttributeString("xmlns", "http://ns.adobe.com/xfdf/" );
                writer.WriteStartElement("annots");

                // Iterate through pages and their annotations
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    var page = doc.Pages[i];
                    foreach (var annotation in page.Annotations)
                    {
                        writer.WriteStartElement("annot");
                        writer.WriteAttributeString("page", i.ToString());
                        writer.WriteAttributeString("type", annotation.GetType().Name);

                        // Write rectangle bounds if available
                        if (annotation.Rect != null)
                        {
                            writer.WriteStartElement("rect");
                            writer.WriteString($"{annotation.Rect.LLX} {annotation.Rect.LLY} {annotation.Rect.URX} {annotation.Rect.URY}");
                            writer.WriteEndElement(); // rect
                        }

                        // For text annotations, write the contents
                        if (annotation is Aspose.Pdf.Annotations.TextAnnotation textAnn)
                        {
                            writer.WriteStartElement("contents");
                            writer.WriteString(textAnn.Contents ?? string.Empty);
                            writer.WriteEndElement(); // contents
                        }

                        writer.WriteEndElement(); // annot
                    }
                }

                writer.WriteEndElement(); // annots
                writer.WriteEndElement(); // xfdf
                writer.WriteEndDocument();
            }
        }
    }

    // Uploads an XFDF file to the specified REST API endpoint
    private static async Task<bool> UploadXfdfAsync(HttpClient client, string url, string xfdfPath)
    {
        // Verify the XFDF file exists
        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine($"XFDF not found: {xfdfPath}");
            return false;
        }

        // Prepare multipart/form-data content
        using (MultipartFormDataContent multipart = new MultipartFormDataContent())
        {
            byte[] fileBytes = await File.ReadAllBytesAsync(xfdfPath);
            ByteArrayContent fileContent = new ByteArrayContent(fileBytes);
            // Set the correct MIME type for XFDF
            fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.adobe.xfdf");
            // Add the file content; "file" is the form field name expected by the API
            multipart.Add(fileContent, "file", Path.GetFileName(xfdfPath));

            // Perform the POST request
            HttpResponseMessage response = await client.PostAsync(url, multipart);
            return response.IsSuccessStatusCode;
        }
    }
}
