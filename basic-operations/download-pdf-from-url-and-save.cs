using System;
using System.IO;
using System.Net;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfUrl = "https://example.com/sample.pdf"; // replace with actual URL
        const string localFile = "sample.pdf";

        try
        {
            // Create a web request to fetch the PDF
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(pdfUrl);
            request.Method = "GET";

            // Obtain the response stream
            using (WebResponse response = request.GetResponse())
            using (Stream pdfStream = response.GetResponseStream())
            {
                // Load the PDF directly from the stream using the core Document API
                using (Document doc = new Document(pdfStream))
                {
                    // Save the document to a local file
                    doc.Save(localFile);
                }
            }

            Console.WriteLine($"PDF successfully saved to '{localFile}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}