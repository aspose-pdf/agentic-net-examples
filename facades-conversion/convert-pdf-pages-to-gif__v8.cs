using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath   = "input.pdf";
        const string outputDir = "GifFrames";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        try
        {
            // PdfConverter implements IDisposable, so wrap it in a using block
            using (PdfConverter converter = new PdfConverter())
            {
                // Bind the source PDF file
                converter.BindPdf(pdfPath);

                // Start the conversion process
                converter.DoConvert();

                int pageNumber = 1;
                // Retrieve each rendered page as an image
                while (converter.HasNextImage())
                {
                    // GetNextImage requires a destination (stream or file path).
                    // Use a MemoryStream to keep the image in memory, then load it as a System.Drawing.Image.
                    using (var imgStream = new MemoryStream())
                    {
                        converter.GetNextImage(imgStream); // writes the image to the stream
                        imgStream.Position = 0; // reset for reading
                        using (Image img = Image.FromStream(imgStream))
                        {
                            string outPath = Path.Combine(outputDir, $"page_{pageNumber}.gif");
                            img.Save(outPath, ImageFormat.Gif);
                            Console.WriteLine($"Saved GIF: {outPath}");
                            pageNumber++;
                        }
                    }
                }
            }
        }
        // Image conversion relies on GDI+, which is Windows‑only
        catch (TypeInitializationException)
        {
            Console.WriteLine("Image conversion requires GDI+ and is only supported on Windows.");
        }
        catch (DllNotFoundException)
        {
            Console.WriteLine("GDI+ library not found. Skipping image conversion.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
