using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // ---------------------------------------------------------------------
        // 1. Create a minimal PDF document in memory (so we don't depend on a file).
        // ---------------------------------------------------------------------
        using (var pdfDoc = new Document())
        {
            pdfDoc.Pages.Add(); // add a blank page

            using (var pdfStream = new MemoryStream())
            {
                // Guard Document.Save against missing GDI+ on non‑Windows platforms.
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    pdfDoc.Save(pdfStream);
                }
                else
                {
                    Console.WriteLine("Skipping Document.Save – GDI+ (libgdiplus) is not available on this platform.");
                    // Still produce an empty PDF stream so the rest of the demo can continue.
                    pdfDoc.Save(pdfStream, SaveFormat.Pdf);
                }
                pdfStream.Position = 0; // reset for reading

                // ---------------------------------------------------------------
                // 2. Create a simple stamp image in memory (red rectangle with text).
                // ---------------------------------------------------------------
                MemoryStream imgStream = null;
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    var bmp = new Bitmap(200, 100);
                    using (var g = Graphics.FromImage(bmp))
                    {
                        g.Clear(System.Drawing.Color.Red);
                        g.DrawString(
                            "STAMP",
                            new System.Drawing.Font("Arial", 20),
                            System.Drawing.Brushes.White,
                            new PointF(10, 35)
                        );
                    }

                    imgStream = new MemoryStream();
                    bmp.Save(imgStream, ImageFormat.Png);
                    imgStream.Position = 0; // reset for reading
                }
                else
                {
                    Console.WriteLine("System.Drawing is not supported on this platform – stamp will be skipped.");
                }

                // -----------------------------------------------------------
                // 3. Bind the PDF and the image to the stamp using stream overloads.
                // -----------------------------------------------------------
                var fileStamp = new PdfFileStamp();
                fileStamp.BindPdf(pdfStream); // bind PDF from stream

                if (imgStream != null)
                {
                    var stamp = new Aspose.Pdf.Facades.Stamp();
                    stamp.BindImage(imgStream);   // bind image from stream

                    // Ensure the stamp is drawn on top of existing page content.
                    stamp.IsBackground = false;

                    // Optional positioning and sizing (points).
                    stamp.SetOrigin(100, 200);      // X‑indent, Y‑indent (bottom‑left origin)
                    stamp.SetImageSize(150, 100);   // Width, Height

                    // Apply the stamp to all pages.
                    fileStamp.AddStamp(stamp);
                }

                // Save the stamped PDF to disk.
                const string outputPdf = "output.pdf";
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    fileStamp.Save(outputPdf);
                }
                else
                {
                    // On non‑Windows platforms we still attempt to save; Aspose.Pdf can write the PDF
                    // without needing GDI+ because we are not rendering System.Drawing objects.
                    fileStamp.Save(outputPdf);
                }
                fileStamp.Close();

                Console.WriteLine($"Stamp applied and saved to '{outputPdf}'.");
            }
        }
    }
}
