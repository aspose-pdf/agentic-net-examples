using System;
using System.IO;
using System.Drawing;                     // System.Drawing.Color, System.Drawing.Rectangle
using Aspose.Pdf;                         // Aspose.Pdf.Color, Aspose.Pdf.Rectangle, etc.
using Aspose.Pdf.Facades;                // PdfFileStamp, Stamp, FormattedText, EncodingType

class Program
{
    static void Main()
    {
        // Folder containing source PDFs
        const string inputFolder = "input_pdfs";
        // Folder where watermarked PDFs will be written
        const string outputFolder = "output_pdfs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Multi‑line watermark text
        string watermarkText = "CONFIDENTIAL\nDo Not Distribute";

        // Create a FormattedText object that defines the watermark appearance
        // NOTE: use System.Drawing.Color for the color argument and a float for the font size
        Aspose.Pdf.Facades.FormattedText ft = new Aspose.Pdf.Facades.FormattedText(
            watermarkText,                 // text (supports line breaks)
            System.Drawing.Color.Red,     // text color (System.Drawing.Color)
            "Helvetica",                 // font name
            Aspose.Pdf.Facades.EncodingType.Winansi, // text encoding
            false,                        // embed font flag
            48f);                         // font size (float)

        // Process each PDF file in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(outputFolder, $"{fileNameWithoutExt}_watermarked.pdf");

            // Initialize the PdfFileStamp facade and bind the source PDF
            PdfFileStamp fileStamp = new PdfFileStamp();
            fileStamp.BindPdf(inputPath);   // loads the PDF to be stamped

            // Configure the stamp (watermark)
            Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
            stamp.BindLogo(ft);            // use the formatted text as the stamp content
            stamp.IsBackground = true;    // place the watermark behind page content
            stamp.Opacity = 0.5f;          // semi‑transparent appearance
            // Optional: adjust position; (0,0) is bottom‑left of the page
            stamp.SetOrigin(0, 0);

            // Add the stamp to the document (applies to all pages by default)
            fileStamp.AddStamp(stamp);

            // Save the watermarked PDF and release resources
            fileStamp.Save(outputPath);
            fileStamp.Close();

            Console.WriteLine($"Watermarked: {inputPath} → {outputPath}");
        }
    }
}
