using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    // Helper to infer file extension from the first bytes of an image stream.
    private static string GetExtensionFromHeader(Stream s)
    {
        if (!s.CanSeek) return ".bin";
        long originalPos = s.Position;
        s.Seek(0, SeekOrigin.Begin);
        byte[] header = new byte[8];
        int read = s.Read(header, 0, header.Length);
        s.Seek(originalPos, SeekOrigin.Begin);
        if (read >= 2)
        {
            // JPEG
            if (header[0] == 0xFF && header[1] == 0xD8) return ".jpg";
            // PNG
            if (header[0] == 0x89 && header[1] == 0x50) return ".png";
            // GIF
            if (header[0] == 0x47 && header[1] == 0x49) return ".gif";
            // BMP
            if (header[0] == 0x42 && header[1] == 0x4D) return ".bmp";
            // TIFF (II*)
            if (header[0] == 0x49 && header[1] == 0x49) return ".tif";
            // TIFF (MM*)
            if (header[0] == 0x4D && header[1] == 0x4D) return ".tif";
        }
        return ".bin";
    }

    static void Main()
    {
        const string inputPdf = "signed_document.pdf";
        const string outputDir = "SignatureImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Get all signature fields in the document
            var signatureFields = doc.Form?.Fields?.OfType<SignatureField>()?.ToList();
            if (signatureFields == null || signatureFields.Count == 0)
            {
                Console.WriteLine("No signature fields found in the document.");
                return;
            }

            // Iterate over each signature field
            foreach (SignatureField sigField in signatureFields)
            {
                int pageIndex = sigField.PageIndex; // 0‑based index
                // Extract the appearance image (if any) from the signature field
                using (Stream appearanceStream = sigField.ExtractImage())
                {
                    if (appearanceStream == null)
                    {
                        Console.WriteLine($"No appearance image found for signature on page {pageIndex + 1}.");
                        continue;
                    }

                    string ext = GetExtensionFromHeader(appearanceStream);
                    string outPath = Path.Combine(outputDir,
                        $"sigPage{pageIndex + 1}{ext}");

                    // Save the extracted image to a file
                    using (FileStream fs = File.Create(outPath))
                    {
                        appearanceStream.CopyTo(fs);
                    }

                    Console.WriteLine($"Extracted signature appearance saved to: {outPath}");
                }
            }
        }
    }
}
