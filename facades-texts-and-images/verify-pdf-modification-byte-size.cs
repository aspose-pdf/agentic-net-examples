using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string originalPath = "original.pdf";
        const string imagePath = "sample.png";
        const string modifiedPath = "modified.pdf";

        // Verify that the image file exists
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Create a simple one‑page PDF if it does not already exist
        if (!File.Exists(originalPath))
        {
            using (Document doc = new Document())
            {
                doc.Pages.Add();
                doc.Save(originalPath);
            }
        }

        // Record the original file size in bytes
        long originalSize = new FileInfo(originalPath).Length;

        // Add the image to the PDF using PdfFileMend
        using (PdfFileMend mend = new PdfFileMend())
        {
            mend.BindPdf(originalPath);
            // Add image to page 1 at coordinates (100,500) – (300,700)
            mend.AddImage(imagePath, new int[] { 1 }, 100f, 500f, 300f, 700f);
            mend.Save(modifiedPath);
        }

        // Record the modified file size in bytes
        long modifiedSize = new FileInfo(modifiedPath).Length;

        // Compare sizes and output the result
        if (modifiedSize > originalSize)
        {
            Console.WriteLine($"PASS: Modified PDF size increased from {originalSize} to {modifiedSize} bytes.");
        }
        else
        {
            Console.WriteLine($"FAIL: Modified PDF size did not increase (original {originalSize}, modified {modifiedSize}).");
        }
    }
}