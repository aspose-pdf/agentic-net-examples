using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input PDF path and signature image path
        if (args.Length != 2)
        {
            Console.Error.WriteLine("Usage: <app> <pdfPath> <signatureImagePath>");
            return;
        }

        string pdfPath = args[0];
        string imagePath = args[1];

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Signature image file not found: {imagePath}");
            return;
        }

        // Prepare output file name
        string outputPath = Path.Combine(
            Path.GetDirectoryName(pdfPath) ?? string.Empty,
            Path.GetFileNameWithoutExtension(pdfPath) + "_signed.pdf");

        try
        {
            // Create an ImageStamp for the signature image
            ImageStamp signatureStamp = new ImageStamp(imagePath)
            {
                // Position the stamp at the bottom‑right corner of the page
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment   = VerticalAlignment.Bottom,
                // Use individual margin properties instead of a non‑existent Margin object
                BottomMargin = 20,
                RightMargin  = 20,
                // Set opacity if desired (1.0 = fully opaque)
                Opacity = 1.0
            };

            // Load the PDF document
            using (Document doc = new Document(pdfPath))
            {
                // Iterate through all pages (1‑based indexing) and stamp each page
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Page page = doc.Pages[i];
                    page.AddStamp(signatureStamp);
                }

                // Save the modified document
                doc.Save(outputPath);
            }

            Console.WriteLine($"Signature image applied. Output saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
