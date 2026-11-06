using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "resized.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF into a memory stream
        using (FileStream fileStream = File.OpenRead(inputPath))
        using (MemoryStream inputStream = new MemoryStream())
        {
            fileStream.CopyTo(inputStream);
            inputStream.Position = 0; // reset for reading

            // Bind the stream to PdfPageEditor (Facades API) – kept for API compatibility
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                editor.BindPdf(inputStream); // stream overload (no further use needed)
            }

            // Reset the stream again before loading it into Document
            inputStream.Position = 0;

            // Resize each page using Document/PageInfo (the supported way)
            using (Document doc = new Document(inputStream))
            {
                const double targetWidth  = 1024; // points (1 point = 1/72 inch)
                const double targetHeight = 768;

                foreach (Page page in doc.Pages)
                {
                    page.PageInfo.Width  = targetWidth;
                    page.PageInfo.Height = targetHeight;
                }

                // Save the modified PDF into another memory stream
                using (MemoryStream outputStream = new MemoryStream())
                {
                    doc.Save(outputStream);
                    outputStream.Position = 0; // reset for verification

                    // Verify that all pages have the expected dimensions
                    bool dimensionsMatch = true;
                    foreach (Page page in doc.Pages)
                    {
                        // Allow a tiny tolerance for floating‑point rounding
                        if (Math.Abs(page.PageInfo.Width  - targetWidth)  > 0.01 ||
                            Math.Abs(page.PageInfo.Height - targetHeight) > 0.01)
                        {
                            dimensionsMatch = false;
                            break;
                        }
                    }

                    Console.WriteLine(dimensionsMatch
                        ? "Resize successful, dimensions verified."
                        : "Resize verification failed.");

                    // Write the final PDF to disk
                    File.WriteAllBytes(outputPath, outputStream.ToArray());
                    Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
                }
            }
        }
    }
}
