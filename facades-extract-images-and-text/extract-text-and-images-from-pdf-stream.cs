using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Aspose.Pdf.Facades;
using Aspose.Pdf;
using System.Drawing.Imaging;

class PdfStreamProcessor
{
    // Holds extracted text per page
    private readonly List<string> _pageTexts = new List<string>();
    // Holds extracted images as byte arrays
    private readonly List<byte[]> _images = new List<byte[]>();

    public void ProcessPdfStream(Stream pdfStream)
    {
        // Use PdfExtractor facade to work directly on the incoming stream
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF data from the provided stream
            extractor.BindPdf(pdfStream);

            // ---------- Extract Text ----------
            // Extract all text (Unicode encoding) from the document
            extractor.ExtractText(Encoding.Unicode);

            int pageNumber = 1;
            while (extractor.HasNextPageText())
            {
                using (MemoryStream textMs = new MemoryStream())
                {
                    // Save the text of the current page into a memory stream
                    extractor.GetNextPageText(textMs);
                    // Convert the bytes to a string using the same encoding
                    string pageText = Encoding.Unicode.GetString(textMs.ToArray());
                    _pageTexts.Add(pageText);
                    Console.WriteLine($"[Text] Page {pageNumber} extracted, {pageText.Length} characters.");
                }
                pageNumber++;
            }

            // ---------- Extract Images ----------
            // Prepare image extraction (default mode is fine)
            extractor.ExtractImage();

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                using (MemoryStream imgMs = new MemoryStream())
                {
#pragma warning disable CA1416 // System.Drawing.ImageFormat is Windows‑specific
                    // Save each image as PNG into the memory stream
                    bool success = extractor.GetNextImage(imgMs, ImageFormat.Png);
#pragma warning restore CA1416
                    if (success)
                    {
                        _images.Add(imgMs.ToArray());
                        Console.WriteLine($"[Image] Image {imageIndex} extracted, {imgMs.Length} bytes.");
                    }
                    else
                    {
                        Console.WriteLine($"[Image] Image {imageIndex} extraction failed.");
                    }
                }
                imageIndex++;
            }
        }
    }

    // Example method showing how to receive PDFs over a TCP socket
    public void ListenAndProcess(int port)
    {
        TcpListener listener = null;
        try
        {
            // Enable address reuse to avoid "Address already in use" when the socket is in TIME_WAIT state
            listener = new TcpListener(IPAddress.Any, port);
            listener.Server.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            listener.Start();
            Console.WriteLine($"Listening on port {port}...");
        }
        catch (SocketException ex)
        {
            Console.WriteLine($"Failed to start listener on port {port}: {ex.Message}");
            return;
        }

        while (true)
        {
            try
            {
                using (TcpClient client = listener.AcceptTcpClient())
                using (NetworkStream netStream = client.GetStream())
                {
                    Console.WriteLine("Client connected, receiving PDF...");

                    // Read the entire PDF into a MemoryStream (no intermediate files)
                    using (MemoryStream pdfMs = new MemoryStream())
                    {
                        netStream.CopyTo(pdfMs);
                        pdfMs.Position = 0; // Reset for reading
                        ProcessPdfStream(pdfMs);
                    }

                    Console.WriteLine("Processing completed for current client.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while handling client: {ex.Message}");
            }
        }
    }

    // Entry point
    static void Main()
    {
        const int listenPort = 5000; // Example port
        PdfStreamProcessor processor = new PdfStreamProcessor();
        processor.ListenAndProcess(listenPort);
    }
}
