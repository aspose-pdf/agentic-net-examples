using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string attachmentUrl = "https://example.com/file.bin";
        const string attachmentName = "file.bin";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // Load the existing PDF document
        using (Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document(pdfPath))
        {
            // Retry parameters
            const int maxRetries = 3;
            const int delayMs = 2000;
            int attempt = 0;
            bool success = false;

            while (attempt < maxRetries && !success)
            {
                attempt++;
                try
                {
                    // Download the attachment as a network stream
                    using (HttpClient client = new HttpClient())
                    {
                        client.Timeout = TimeSpan.FromSeconds(10);
                        using (Stream netStream = client.GetStreamAsync(attachmentUrl).Result)
                        {
                            // Aspose.Pdf requires a seekable stream, so copy to MemoryStream
                            using (MemoryStream ms = new MemoryStream())
                            {
                                netStream.CopyTo(ms);
                                ms.Position = 0;

                                // Create a file specification for the attachment
                                Aspose.Pdf.FileSpecification fileSpec = new Aspose.Pdf.FileSpecification(attachmentName);
                                fileSpec.Contents = ms;

                                // Add the file specification to the EmbeddedFiles collection
                                pdfDoc.EmbeddedFiles.Add(fileSpec);
                            }
                        }
                    }

                    success = true; // attachment added successfully
                }
                catch (Exception ex) when (ex is TaskCanceledException || ex is TimeoutException || ex is HttpRequestException)
                {
                    Console.Error.WriteLine($"Attempt {attempt} failed: {ex.Message}");
                    if (attempt < maxRetries)
                    {
                        Thread.Sleep(delayMs);
                        Console.WriteLine("Retrying...");
                    }
                }
            }

            if (!success)
            {
                Console.Error.WriteLine("Failed to add attachment after all retries.");
                return;
            }

            // Save the updated PDF
            const string outputPath = "output.pdf";
            pdfDoc.Save(outputPath);
            Console.WriteLine($"Attachment added and PDF saved to '{outputPath}'.");
        }
    }
}