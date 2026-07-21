using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF, output PDF and the base64‑encoded image.
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string base64Image   = "<BASE64_STRING>"; // replace with a real Base64 string

        // Guard against an empty or placeholder Base64 string.
        if (string.IsNullOrWhiteSpace(base64Image) || base64Image.Trim() == "<BASE64_STRING>")
        {
            Console.WriteLine("No valid Base64 image supplied – stamping is skipped.");
            return;
        }

        byte[] imageBytes;
        try
        {
            imageBytes = Convert.FromBase64String(base64Image);
        }
        catch (FormatException ex)
        {
            Console.WriteLine($"Base64 conversion failed: {ex.Message}");
            return;
        }

        using (MemoryStream imageStream = new MemoryStream(imageBytes))
        {
            // Create the stamp and bind the image stream to it.
            Stamp stamp = new Stamp();
            stamp.BindImage(imageStream);

            // 1 mm = 2.83464567 points. Position the stamp 50 mm from the left
            // and 50 mm from the bottom of the page.
            const float mmToPoints = 2.83464567f;
            float position = 50f * mmToPoints;
            stamp.SetOrigin(position, position);

            // Apply the stamp only to page 3 (1‑based index).
            stamp.PageNumber = 3;

            // Use the non‑obsolete PdfFileStamp API.
            using (PdfFileStamp fileStamp = new PdfFileStamp())
            {
                fileStamp.BindPdf(inputPdfPath);
                fileStamp.AddStamp(stamp);
                fileStamp.Save(outputPdfPath);
                // No need to call Close() explicitly – the using block disposes it.
            }
        }

        Console.WriteLine($"Image stamp added to page 3 of '{outputPdfPath}'.");
    }
}
