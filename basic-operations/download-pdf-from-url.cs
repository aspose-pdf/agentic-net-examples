using System;
using System.IO;
using System.Net;
using Aspose.Pdf;

namespace DownloadPdfFromUrl
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a sample PDF that will be accessed via a URL.
            string sampleFile = "sample.pdf";
            using (Document sampleDoc = new Document())
            {
                sampleDoc.Pages.Add();
                sampleDoc.Save(sampleFile);
            }

            // Build a file URL pointing to the sample PDF.
            string fileUrl = new Uri(Path.GetFullPath(sampleFile)).AbsoluteUri;

            // Create a WebRequest to obtain the PDF stream.
            WebRequest request = WebRequest.Create(fileUrl);
            using (WebResponse response = request.GetResponse())
            {
                using (Stream pdfStream = response.GetResponseStream())
                {
                    // Load the PDF from the stream using ComHelper.
                    Aspose.Pdf.ComHelper helper = new Aspose.Pdf.ComHelper();
                    using (Document downloadedDoc = helper.OpenStream(pdfStream))
                    {
                        // Save the downloaded PDF locally.
                        downloadedDoc.Save("downloaded.pdf");
                    }
                }
            }
        }
    }
}