using System;
using System.IO;
using System.Net.Http;
using System.Web;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_qr.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF to read its metadata
        using (Document doc = new Document(inputPath))
        {
            // Gather metadata into a single string
            string metadata = $"Title:{doc.Info.Title}|Author:{doc.Info.Author}|Subject:{doc.Info.Subject}|Keywords:{doc.Info.Keywords}";
            // URL‑encode the metadata for the QR‑code service
            string encodedData = HttpUtility.UrlEncode(metadata);

            // Build a request to a free QR‑code generation service
            string qrUrl = $"https://api.qrserver.com/v1/create-qr-code/?size=150x150&data={encodedData}";

            // Download the QR‑code image into a memory stream
            using (HttpClient http = new HttpClient())
            using (Stream qrStream = http.GetAsync(qrUrl).Result.Content.ReadAsStreamAsync().Result)
            {
                // Prepare the stamp (fully‑qualified to avoid ambiguity)
                Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp
                {
                    IsBackground = true,          // place behind page content
                    Opacity      = 0.8f           // semi‑transparent
                };
                // Position the stamp (bottom‑left corner)
                stamp.SetOrigin(50, 50);
                // Set the size of the stamp (matches the QR image size)
                stamp.SetImageSize(150, 150);
                // Bind the QR‑code image stream to the stamp
                stamp.BindImage(qrStream);

                // Apply the stamp to the PDF using PdfFileStamp facade
                using (PdfFileStamp fileStamp = new PdfFileStamp())
                {
                    fileStamp.BindPdf(inputPath);          // load source PDF
                    fileStamp.AddStamp(stamp);             // add the QR stamp
                    fileStamp.Save(outputPath);            // write result
                    fileStamp.Close();                     // finalize
                }
            }
        }

        Console.WriteLine($"QR‑code stamp added. Output saved to '{outputPath}'.");
    }
}
