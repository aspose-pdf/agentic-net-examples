using System;
using System.IO;
using System.Net;
using System.Threading;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string attachmentUrl = "https://example.com/file.pdf";
        const int maxRetries = 3;
        const int delayMs = 2000;

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPdf);

        int attempt = 0;
        bool attached = false;
        while (attempt < maxRetries && !attached)
        {
            attempt++;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(attachmentUrl);
                request.Timeout = 5000; // milliseconds
                using (WebResponse response = request.GetResponse())
                using (Stream networkStream = response.GetResponseStream())
                {
                    System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, 100, 100);
                    editor.CreateFileAttachment(rect, "Network attachment", networkStream, "file.pdf", 1, "Paperclip");
                }
                attached = true;
            }
            catch (WebException ex) when (ex.Status == WebExceptionStatus.Timeout)
            {
                Console.Error.WriteLine($"Attempt {attempt} timed out. Retrying...");
                if (attempt < maxRetries)
                {
                    Thread.Sleep(delayMs);
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to add attachment: {ex.Message}");
                break;
            }
        }

        if (attached)
        {
            editor.Save(outputPdf);
            Console.WriteLine($"Attachment added and saved to '{outputPdf}'.");
        }
        else
        {
            Console.Error.WriteLine("Attachment could not be added after retries.");
        }
    }
}
