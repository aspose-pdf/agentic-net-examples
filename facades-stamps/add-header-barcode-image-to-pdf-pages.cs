using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        // Ensure the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Initialize the PdfFileStamp facade
        var fileStamp = new PdfFileStamp();
        fileStamp.InputFile  = inputPdf;   // input PDF
        fileStamp.OutputFile = outputPdf;  // output PDF with header barcodes

        // Loop through each page (Aspose.Pdf uses 1‑based indexing)
        // We'll generate a simple barcode‑like image containing the page number.
        // For a real barcode, replace the image generation logic with a proper barcode library.
        for (int pageNumber = 1; ; pageNumber++)
        {
            // Stop when the page does not exist – PdfFileStamp does not expose page count,
            // so we attempt to bind a stamp to the page; if the page index exceeds the document,
            // an exception will be thrown and we break the loop.
            try
            {
                // Create a bitmap with the page number drawn on it
                using (var bitmap = new System.Drawing.Bitmap(150, 50))
                {
                    using (var graphics = System.Drawing.Graphics.FromImage(bitmap))
                    {
                        graphics.Clear(System.Drawing.Color.White);
                        using (var font = new System.Drawing.Font("Arial", 24, System.Drawing.FontStyle.Bold))
                        {
                            string text = $"Page {pageNumber}";
                            var textSize = graphics.MeasureString(text, font);
                            float x = (bitmap.Width - textSize.Width) / 2;
                            float y = (bitmap.Height - textSize.Height) / 2;
                            graphics.DrawString(text, font, System.Drawing.Brushes.Black, x, y);
                        }
                    }

                    // Save the bitmap to a memory stream (PNG format)
                    using (var imageStream = new MemoryStream())
                    {
                        bitmap.Save(imageStream, System.Drawing.Imaging.ImageFormat.Png);
                        imageStream.Position = 0; // reset stream position for reading

                        // Create a stamp for this page
                        var stamp = new Stamp();
                        stamp.BindImage(imageStream);          // use the generated image
                        stamp.SetOrigin(20f, 800f);            // position near the top (adjust as needed)
                        stamp.IsBackground = false;           // draw over existing content
                        stamp.Pages = new int[] { pageNumber }; // apply only to the current page

                        // Add the stamp to the document
                        fileStamp.AddStamp(stamp);
                    }
                }
            }
            catch (Exception ex)
            {
                // When the page index exceeds the document length, break the loop.
                // Any other exception is re‑thrown.
                if (ex.Message.Contains("Page number out of range") ||
                    ex.Message.Contains("Index was out of range"))
                {
                    break;
                }
                else
                {
                    Console.Error.WriteLine($"Error processing page {pageNumber}: {ex.Message}");
                    throw;
                }
            }
        }

        // Save the modified PDF and release resources
        fileStamp.Save(outputPdf);
        fileStamp.Close();

        Console.WriteLine($"Header barcodes added. Output saved to '{outputPdf}'.");
    }
}