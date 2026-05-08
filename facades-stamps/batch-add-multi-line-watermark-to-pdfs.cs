using System;
using System.IO;
using Aspose.Pdf.Facades; // PdfFileStamp, Stamp, FormattedText, EncodingType
using System.Drawing;   // Color

class BatchWatermark
{
    static void Main()
    {
        // Define input and output folders. Use absolute paths when they exist, otherwise fall back to a folder relative to the current directory.
        string inputFolder = GetValidFolderPath(@"C:\PdfInput");
        string outputFolder = GetValidFolderPath(@"C:\PdfOutput");

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Prepare the multi‑line watermark text
        FormattedText watermarkText = new FormattedText(
            "Confidential\r\nDo Not Distribute\r\nCompany XYZ",
            Color.Gray,               // text color
            "Helvetica",            // font name
            EncodingType.Winansi,    // encoding
            false,                    // isEmbedded
            48);                      // font size

        // Verify that the input folder actually exists before trying to enumerate files
        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Process each PDF in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileNameWithoutExtension(pdfPath);
            string outputPath = Path.Combine(outputFolder, fileName + "_watermarked.pdf");

            // Initialise the facade and bind the source PDF
            using (PdfFileStamp fileStamp = new PdfFileStamp())
            {
                fileStamp.BindPdf(pdfPath); // Bind the source PDF

                // Create a stamp and bind the formatted text as a logo (watermark)
                Stamp stamp = new Stamp();
                stamp.BindLogo(watermarkText);
                stamp.IsBackground = true;   // render behind page content
                stamp.Opacity = 0.5f;        // semi‑transparent
                stamp.SetOrigin(100, 400);   // position of the watermark (optional)

                // Apply the stamp to the whole document
                fileStamp.AddStamp(stamp);

                // Save the result
                fileStamp.Save(outputPath);
            }
        }

        Console.WriteLine("Batch watermarking completed.");
    }

    /// <summary>
    /// Returns a valid folder path. If the supplied path is rooted and exists, it is returned unchanged.
    /// Otherwise, the method treats the supplied string as a relative folder name and combines it with the
    /// current working directory. The resulting directory is created if it does not already exist.
    /// </summary>
    private static string GetValidFolderPath(string path)
    {
        if (Path.IsPathRooted(path))
        {
            // Absolute path – ensure it exists (or create it for the output folder)
            return path;
        }
        // Relative path – resolve against the current directory
        string combined = Path.Combine(Directory.GetCurrentDirectory(), path);
        return combined;
    }
}
