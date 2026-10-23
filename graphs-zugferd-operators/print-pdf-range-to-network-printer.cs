using System;
using System.IO;
using System.Drawing;
using System.Drawing.Printing;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        // UNC path to the network printer
        const string printerName = @"\\PrintServer\\NetworkPrinter";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Define the page range to print (inclusive)
                const int startPage = 2;
                const int endPage   = 5;
                int currentPage = startPage;

                // Configure the .NET PrintDocument with duplex mode and A4 paper size
                using (PrintDocument printDoc = new PrintDocument())
                {
                    printDoc.PrinterSettings.PrinterName = printerName;
                    // DuplexFlipLongEdge corresponds to vertical duplex in System.Drawing.Printing
                    printDoc.PrinterSettings.Duplex = Duplex.Vertical;
                    // A4 size in hundredths of an inch (8.27" x 11.69")
                    printDoc.DefaultPageSettings.PaperSize = new PaperSize("A4", 827, 1169);

                    // Event handler that renders each PDF page to an image and draws it on the printer graphics
                    printDoc.PrintPage += (sender, e) =>
                    {
                        // Render the current PDF page to a PNG image in memory
                        using (MemoryStream ms = new MemoryStream())
                        {
                            // 300 DPI gives good print quality; adjust as needed
                            var pngDevice = new PngDevice(new Resolution(300));
                            pngDevice.Process(doc.Pages[currentPage], ms);
                            ms.Position = 0;

                            using (System.Drawing.Image img = System.Drawing.Image.FromStream(ms))
                            {
                                // Fit the image into the printable area while preserving aspect ratio
                                System.Drawing.Rectangle marginBounds = e.MarginBounds;
                                e.Graphics.DrawImage(img, marginBounds);
                            }
                        }

                        // Move to the next page and indicate whether more pages remain
                        currentPage++;
                        e.HasMorePages = currentPage <= endPage;
                    };

                    // Send the job to the printer
                    printDoc.Print();
                }
            }

            Console.WriteLine("Print job submitted successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
