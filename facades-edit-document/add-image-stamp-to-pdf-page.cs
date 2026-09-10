using System;
using System.IO;
using System.Net.Http;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string imageUrl = "https://example.com/logo.png";

        // Verify source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Download the image safely
        using (HttpClient httpClient = new HttpClient())
        {
            HttpResponseMessage response = httpClient.GetAsync(imageUrl).Result;
            if (!response.IsSuccessStatusCode)
            {
                Console.Error.WriteLine($"Failed to download image. Status code: {response.StatusCode}");
                return;
            }

            using (Stream imageStream = response.Content.ReadAsStreamAsync().Result)
            using (PdfFileStamp fileStamp = new PdfFileStamp())
            {
                // Initialize the facade with the source PDF (new API)
                fileStamp.BindPdf(inputPdf);

                // Create a stamp and bind the downloaded image
                Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
                stamp.BindImage(imageStream);
                stamp.SetOrigin(100, 500);          // X, Y position on the page
                stamp.SetImageSize(150, 100);       // Width, Height of the stamp
                stamp.IsBackground = false;        // Overlay the stamp
                stamp.Pages = new int[] { 2 };      // Apply only to page 2

                // Add the stamp and save the result (new API)
                fileStamp.AddStamp(stamp);
                fileStamp.Save(outputPdf);
                fileStamp.Close(); // optional, Dispose will also close
            }
        }
    }
}
