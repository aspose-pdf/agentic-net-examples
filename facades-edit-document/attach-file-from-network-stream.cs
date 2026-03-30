using System;
using System.IO;
using System.Net.Http;
using Aspose.Pdf.Facades;
using Aspose.Pdf;               // needed to create a blank PDF when the source file is missing
using System.Drawing;          // System.Drawing.Rectangle is used by PdfContentEditor

class Program
{
    static void Main()
    {
        // Paths for the source PDF and the output PDF
        string inputPdfPath = "input.pdf";
        string outputPdfPath = "output.pdf";

        // URL of the file to download and attach
        string fileUrl = "https://example.com/file.bin"; // replace with a valid URL in production

        // ------------------------------------------------------------
        // Ensure the source PDF exists. If it does not, create a minimal PDF
        // with a single blank page so that the editor can bind to it.
        // ------------------------------------------------------------
        if (!File.Exists(inputPdfPath))
        {
            using (Document doc = new Document())
            {
                doc.Pages.Add();
                doc.Save(inputPdfPath);
            }
        }

        // ------------------------------------------------------------
        // Download the file into a byte array (with graceful error handling)
        // ------------------------------------------------------------
        byte[] fileBytes;
        using (HttpClient httpClient = new HttpClient())
        {
            try
            {
                HttpResponseMessage response = httpClient.GetAsync(fileUrl).Result;
                response.EnsureSuccessStatusCode();
                fileBytes = response.Content.ReadAsByteArrayAsync().Result;
            }
            catch (Exception ex)
            {
                // If the download fails (e.g., 404), fall back to a placeholder byte array
                Console.WriteLine($"Warning: Unable to download file from '{fileUrl}'. Reason: {ex.Message}");
                string placeholder = "Placeholder content – original file could not be downloaded.";
                fileBytes = System.Text.Encoding.UTF8.GetBytes(placeholder);
            }
        }

        // ------------------------------------------------------------
        // Attach the downloaded (or placeholder) data to the PDF
        // ------------------------------------------------------------
        using (MemoryStream attachmentStream = new MemoryStream(fileBytes))
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPdfPath);

            // Define the annotation rectangle (position and size)
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, 100, 100);

            // Parameters for the file attachment annotation
            string contents = "Network file attachment";
            string attachmentName = "downloaded_file.bin";
            int pageNumber = 1; // 1‑based page index
            string iconName = "Graph"; // Graph, PushPin, Paperclip, or Tag

            // Create the file attachment annotation using the memory stream
            editor.CreateFileAttachment(rect, contents, attachmentStream, attachmentName, pageNumber, iconName);

            // Save the modified PDF
            editor.Save(outputPdfPath);
        }

        Console.WriteLine($"File attachment added and PDF saved to '{outputPdfPath}'.");
    }
}
