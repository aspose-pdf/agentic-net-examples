using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

class Program
{
    // Note: C# 7.1+ supports async Main
    static async Task Main()
    {
        // URL of the PDF to process – replace with a reachable address
        const string pdfUrl = "https://example.com/sample.pdf";

        try
        {
            // Download the PDF – ensure the request succeeded before reading the body
            using (HttpClient httpClient = new HttpClient())
            using (HttpResponseMessage response = await httpClient.GetAsync(pdfUrl))
            {
                response.EnsureSuccessStatusCode(); // throws if status is not 2xx

                // Read the content into a seek‑able MemoryStream
                byte[] pdfBytes = await response.Content.ReadAsByteArrayAsync();
                using (MemoryStream pdfStream = new MemoryStream(pdfBytes))
                {
                    // Bind the PDF stream to the editor
                    using (PdfPageEditor editor = new PdfPageEditor())
                    {
                        editor.BindPdf(pdfStream);
                        editor.Rotation = 90; // rotate all pages 90° (valid values: 0,90,180,270)
                        editor.ApplyChanges(); // optional – Save also applies changes

                        // Overwrite the original stream with the modified PDF
                        pdfStream.Position = 0;
                        pdfStream.SetLength(0);
                        editor.Save(pdfStream);
                        pdfStream.Position = 0; // reset for subsequent reads
                    }

                    // Example: persist the rotated PDF to a file (or send it elsewhere)
                    using (FileStream file = File.Create("rotated_output.pdf"))
                    {
                        pdfStream.CopyTo(file);
                    }
                }
            }
        }
        catch (HttpRequestException ex)
        {
            Console.Error.WriteLine($"Failed to download PDF: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}
