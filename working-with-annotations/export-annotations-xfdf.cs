using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

namespace ExportAnnotationsXfdfExample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Step 1: Create a sample PDF with a text annotation
            string pdfPath = "sample.pdf";
            string xfdfPath = "annotations.xfdf";

            using (Document pdfDocument = new Document())
            {
                // Add a blank page (1‑based indexing)
                pdfDocument.Pages.Add();

                // Create a text annotation on the first page using the (Page, Rectangle) constructor
                Page firstPage = pdfDocument.Pages[1];
                Aspose.Pdf.Rectangle annotationRect = new Aspose.Pdf.Rectangle(100, 600, 200, 650);
                TextAnnotation textAnnotation = new TextAnnotation(firstPage, annotationRect);
                textAnnotation.Contents = "This is a sample annotation";
                firstPage.Annotations.Add(textAnnotation);

                // Save the sample PDF (required for a realistic scenario)
                pdfDocument.Save(pdfPath);

                // Export all annotations to an XFDF file
                pdfDocument.ExportAnnotationsToXfdf(xfdfPath);
            }

            // Step 3: Upload the XFDF file to a cloud storage bucket (placeholder URL)
            // Note: In a real scenario replace the URL with the actual bucket endpoint and add authentication as needed.
            using (HttpClient httpClient = new HttpClient())
            using (FileStream xfdfStream = File.OpenRead(xfdfPath))
            {
                HttpContent content = new StreamContent(xfdfStream);
                HttpResponseMessage response = await httpClient.PutAsync("https://example-bucket.s3.amazonaws.com/annotations.xfdf", content);
                Console.WriteLine($"Upload status: {response.StatusCode}");
            }
        }
    }
}
