using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";                 // source PDF
        const string outputPdfPath  = "output_with_attachment.pdf"; // result PDF
        const string attachmentUrl  = "https://example.com/file.bin"; // network resource
        const string attachmentName = "file.bin";                  // name inside PDF

        // Parameters for retry logic
        const int maxRetries = 3;
        const int delayBetweenRetriesMs = 2000; // 2 seconds

        // Temporary file to hold the downloaded attachment
        string tempAttachmentPath = Path.GetTempFileName();

        bool downloadSucceeded = false;
        for (int attempt = 1; attempt <= maxRetries && !downloadSucceeded; attempt++)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // Set a reasonable timeout for the request
                    client.Timeout = TimeSpan.FromSeconds(10);

                    // Download the content to the temporary file
                    using (HttpResponseMessage response = client.GetAsync(attachmentUrl).Result)
                    {
                        response.EnsureSuccessStatusCode();
                        using (Stream remoteStream = response.Content.ReadAsStreamAsync().Result)
                        using (FileStream localFile = new FileStream(tempAttachmentPath, FileMode.Create, FileAccess.Write, FileShare.None))
                        {
                            remoteStream.CopyTo(localFile);
                        }
                    }
                }

                downloadSucceeded = true; // exit loop
            }
            catch (TaskCanceledException ex) when (!ex.CancellationToken.IsCancellationRequested)
            {
                // Timeout occurred (HttpClient throws TaskCanceledException on timeout)
                Console.Error.WriteLine($"Attempt {attempt}: download timed out.");
            }
            catch (TimeoutException)
            {
                Console.Error.WriteLine($"Attempt {attempt}: download timed out.");
            }
            catch (Exception ex)
            {
                // Other network errors – do not retry
                Console.Error.WriteLine($"Attempt {attempt}: error downloading attachment – {ex.Message}");
                break;
            }

            if (!downloadSucceeded && attempt < maxRetries)
            {
                Thread.Sleep(delayBetweenRetriesMs);
                Console.WriteLine($"Retrying download (attempt {attempt + 1})...");
            }
        }

        if (!downloadSucceeded)
        {
            Console.Error.WriteLine("Failed to download attachment after multiple attempts.");
            if (File.Exists(tempAttachmentPath)) File.Delete(tempAttachmentPath);
            return;
        }

        try
        {
            // Load the source PDF
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Create a FileSpecification for the attachment using the EmbeddedFiles collection
                var fileSpec = new FileSpecification(attachmentName);
                fileSpec.Contents = new MemoryStream(File.ReadAllBytes(tempAttachmentPath));

                pdfDoc.EmbeddedFiles.Add(fileSpec);

                // Save the modified PDF
                pdfDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"Attachment added successfully. Output saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error adding attachment to PDF: {ex.Message}");
        }
        finally
        {
            // Ensure the temporary file is removed
            if (File.Exists(tempAttachmentPath))
                File.Delete(tempAttachmentPath);
        }
    }
}
