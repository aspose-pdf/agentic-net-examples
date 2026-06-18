using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

namespace ExtractPdfStreamExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Step 1: Create a sample PDF file (self‑contained example requirement).
            using (Document sampleDoc = new Document())
            {
                sampleDoc.Pages.Add();
                TextFragment fragment = new TextFragment("Hello World from Aspose.Pdf!");
                sampleDoc.Pages[1].Paragraphs.Add(fragment);
                sampleDoc.Save("sample.pdf");
            }

            // Step 2: Start a simple TCP server that sends the PDF bytes.
            Thread serverThread = new Thread(() =>
            {
                TcpListener listener = new TcpListener(IPAddress.Loopback, 9000);
                listener.Start();
                using (TcpClient client = listener.AcceptTcpClient())
                {
                    using (NetworkStream networkStream = client.GetStream())
                    {
                        using (FileStream fileStream = new FileStream("sample.pdf", FileMode.Open, FileAccess.Read))
                        {
                            fileStream.CopyTo(networkStream);
                        }
                    }
                }
                listener.Stop();
            });
            serverThread.Start();

            // Give the server a moment to start listening.
            Thread.Sleep(500);

            // Step 3: Connect to the server and receive the PDF stream.
            using (TcpClient tcpClient = new TcpClient())
            {
                tcpClient.Connect(IPAddress.Loopback, 9000);
                using (NetworkStream pdfStream = tcpClient.GetStream())
                {
                    // Step 4: Extract text and images directly from the received stream.
                    using (PdfExtractor extractor = new PdfExtractor())
                    {
                        extractor.BindPdf(pdfStream);

                        // ----- Text extraction -----
                        extractor.ExtractText(Encoding.Unicode);
                        int pageNumber = 1;
                        while (extractor.HasNextPageText())
                        {
                            using (MemoryStream textMemory = new MemoryStream())
                            {
                                extractor.GetNextPageText(textMemory);
                                textMemory.Position = 0;
                                using (StreamReader reader = new StreamReader(textMemory))
                                {
                                    string pageText = reader.ReadToEnd();
                                    Console.WriteLine($"Page {pageNumber} Text: {pageText}");
                                }
                            }
                            pageNumber++;
                        }

                        // ----- Image extraction -----
                        extractor.ExtractImage();
                        int imageIndex = 1;
                        while (extractor.HasNextImage())
                        {
                            using (MemoryStream imageMemory = new MemoryStream())
                            {
                                bool success = extractor.GetNextImage(imageMemory);
                                if (success)
                                {
                                    Console.WriteLine($"Extracted Image {imageIndex}: {imageMemory.Length} bytes");
                                    imageIndex++;
                                }
                            }
                        }
                    }
                }
            }

            // Ensure the server thread finishes before exiting.
            serverThread.Join();
        }
    }
}
