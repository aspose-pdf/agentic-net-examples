using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

// Minimal stub for HttpResponse to allow compilation in non‑Web contexts.
// In a real ASP.NET (Core or Classic) application you should use the actual HttpResponse
// type from Microsoft.AspNetCore.Http (Core) or System.Web (Classic) instead of this stub.
namespace StubHttp
{
    public class HttpResponse
    {
        public string ContentType { get; set; }
        private readonly MemoryStream _output = new MemoryStream();

        // Adds a header – for the stub we simply ignore it.
        public void AddHeader(string name, string value) { /* no‑op */ }

        // Writes binary data to the internal stream.
        public void BinaryWrite(byte[] buffer)
        {
            _output.Write(buffer, 0, buffer.Length);
        }

        // Ends the response – for the stub we just flush the stream.
        public void End()
        {
            _output.Flush();
        }

        // Helper to retrieve the written bytes (useful for testing).
        public byte[] GetResponseBytes() => _output.ToArray();
    }
}

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the image to be added
        string inputPdfPath = "input.pdf";   // can be missing – we will create a placeholder PDF
        string imagePath    = "logo.png";   // must exist for the image to be added

        // Obtain the current HTTP response (in a real web application use HttpContext.Current.Response
        // or the HttpResponse from Microsoft.AspNetCore.Http). Here we use the stub defined above.
        StubHttp.HttpResponse response = GetHttpResponse();
        if (response == null)
        {
            Console.Error.WriteLine("HttpResponse is not available in this context.");
            return;
        }

        // Ensure we have a PDF document to work with. If the file does not exist we create a simple one‑page PDF.
        Document pdfDocument;
        if (File.Exists(inputPdfPath))
        {
            pdfDocument = new Document(inputPdfPath);
        }
        else
        {
            pdfDocument = new Document();
            // Add a blank page so that AddImage has a target page.
            pdfDocument.Pages.Add();
        }

        // Use PdfFileMend to add the image.
        using (pdfDocument)
        {
            PdfFileMend mend = new PdfFileMend();
            mend.BindPdf(pdfDocument);

            // Verify that the image file exists before trying to add it – otherwise Aspose will throw.
            if (File.Exists(imagePath))
            {
                // Add the image to page 1 at the specified rectangle (llx, lly, urx, ury)
                mend.AddImage(imagePath, 1, 100f, 500f, 300f, 700f);
            }
            else
            {
                Console.Error.WriteLine($"Image file '{imagePath}' not found – the PDF will be returned without the image.");
            }

            // Save the modified PDF into a memory stream
            using (MemoryStream memoryStream = new MemoryStream())
            {
                mend.Save(memoryStream);
                memoryStream.Position = 0; // reset for reading

                // Write the PDF stream to the HTTP response
                response.ContentType = "application/pdf";
                response.AddHeader("Content-Disposition", "attachment; filename=modified.pdf");
                response.BinaryWrite(memoryStream.ToArray());
                response.End();
            }
        }
    }

    // Placeholder method to obtain the current HttpResponse.
    // Replace with HttpContext.Current.Response (ASP.NET) or
    // HttpContext.Response (ASP.NET Core) in a real web application.
    private static StubHttp.HttpResponse GetHttpResponse()
    {
        // For demonstration purposes we return a new stub instance.
        // In a console‑app scenario there is no real HTTP response, so this method
        // can be adapted to write the PDF to a file instead.
        return new StubHttp.HttpResponse();
    }
}
