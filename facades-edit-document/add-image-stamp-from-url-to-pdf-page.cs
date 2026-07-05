using System;
using System.IO;
using System.Net.Http;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";          // source PDF
        const string outputPdf = "output.pdf";        // destination PDF
        const string imageUrl = "https://example.com/logo.png";

        // Download the image to a temporary file
        string tempImagePath = Path.GetTempFileName();
        try
        {
            // Use HttpClient to download the image and verify the response status.
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync(imageUrl).Result;
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Failed to download image. HTTP {(int)response.StatusCode} {response.ReasonPhrase}");
                    // Abort stamping because the image is unavailable.
                    return;
                }

                using (Stream imgStream = response.Content.ReadAsStreamAsync().Result)
                using (FileStream file = new FileStream(tempImagePath, FileMode.Create, FileAccess.Write))
                {
                    imgStream.CopyTo(file);
                }
            }

            // Initialize the PdfFileStamp facade using the new API
            using (PdfFileStamp fileStamp = new PdfFileStamp())
            {
                fileStamp.BindPdf(inputPdf); // load source PDF (replaces obsolete InputFile)

                // Create a stamp and bind the downloaded image
                Stamp stamp = new Stamp();
                stamp.BindImage(tempImagePath);               // use the image as stamp
                stamp.SetOrigin(140, 400);                    // position on the page (X, Y)
                stamp.SetImageSize(50, 50);                   // size of the stamp
                stamp.Opacity = 0.8f;                         // optional transparency
                stamp.IsBackground = false;                  // place on top of content
                stamp.Pages = new int[] { 2 };                // apply only to page 2

                // Add the stamp to the PDF and save the result (replaces obsolete OutputFile)
                fileStamp.AddStamp(stamp);
                fileStamp.Save(outputPdf);
            }
        }
        finally
        {
            // Clean up the temporary image file
            if (File.Exists(tempImagePath))
                File.Delete(tempImagePath);
        }

        Console.WriteLine($"Image stamp added to page 2 and saved as '{outputPdf}'.");
    }
}
