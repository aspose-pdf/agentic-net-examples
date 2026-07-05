using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Ensure the input PDF exists – create a minimal one if it does not.
        if (!File.Exists(inputPath))
        {
            CreateSamplePdf(inputPath);
            Console.WriteLine($"Sample PDF created at '{inputPath}'.");
        }

        // Bind the PDF to the XMP metadata facade using the stream overload (more robust than the file‑path overload).
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        using (FileStream fs = new FileStream(inputPath, FileMode.Open, FileAccess.ReadWrite, FileShare.Read))
        {
            xmp.BindPdf(fs);

            // Retrieve the raw XMP metadata as a byte array.
            byte[] rawData = xmp.GetXmpMetadata();

            // Determine if XMP metadata exists (non‑empty byte array).
            bool hasXmp = rawData != null && rawData.Length > 0;
            Console.WriteLine(hasXmp ? "XMP metadata is present." : "No XMP metadata found.");

            // Example modification only if metadata exists.
            if (hasXmp)
            {
                // Add or update a standard XMP property (Nickname).
                // Using the string overload avoids the need for the Aspose.Pdf.Xmp assembly.
                xmp.Add("xmp:Nickname", "NewNickname");
            }

            // Save the (potentially modified) PDF.
            // Reset the stream position before saving to ensure the whole document is written.
            fs.Position = 0;
            xmp.Save(outputPath);
        }

        Console.WriteLine($"Processed file saved to '{outputPath}'.");
    }

    private static void CreateSamplePdf(string path)
    {
        // Create a simple one‑page PDF that can be used for XMP detection.
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();
            page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment("Sample PDF for XMP detection."));
            doc.Save(path);
        }
    }
}