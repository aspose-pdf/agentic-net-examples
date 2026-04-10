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

        // Ensure the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Download the image from the external URL into a memory stream
        using (HttpClient httpClient = new HttpClient())
        using (Stream imageStream = httpClient.GetStreamAsync(imageUrl).Result)
        {
            // Initialize the PdfFileStamp facade
            PdfFileStamp fileStamp = new PdfFileStamp();
            fileStamp.InputFile = inputPdf;   // source PDF
            fileStamp.OutputFile = outputPdf; // destination PDF

            // Create a stamp and bind the downloaded image
            Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
            stamp.BindImage(imageStream);          // use the image stream as stamp content
            stamp.SetOrigin(140, 400);             // position (X, Y) on the page
            stamp.SetImageSize(50, 50);            // optional size of the image stamp
            stamp.Pages = new int[] { 2 };         // apply stamp only to page 2

            // Add the stamp to the document and save
            fileStamp.AddStamp(stamp);
            fileStamp.Close(); // saves the output PDF
        }

        Console.WriteLine($"Image stamp added to page 2 and saved as '{outputPdf}'.");
    }
}