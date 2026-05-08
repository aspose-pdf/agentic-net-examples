using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class BatchLogoAdder
{
    // Resolve paths relative to the executable location to work on any OS.
    private static readonly string BaseDirectory = AppDomain.CurrentDomain.BaseDirectory;
    private static readonly string InputFolder = Path.Combine(BaseDirectory, "PdfInput");
    private static readonly string OutputFolder = Path.Combine(BaseDirectory, "PdfOutput");
    private static readonly string LogoPath = Path.Combine(BaseDirectory, "company_logo.png");

    static void Main()
    {
        // Ensure input, output and logo directories/files exist.
        if (!Directory.Exists(InputFolder))
        {
            Console.WriteLine($"Input folder not found: {InputFolder}");
            return;
        }
        if (!File.Exists(LogoPath))
        {
            Console.WriteLine($"Logo file not found: {LogoPath}");
            return;
        }
        Directory.CreateDirectory(OutputFolder);

        // Get all PDF files in the input folder
        string[] pdfFiles = Directory.GetFiles(InputFolder, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string inputPdfPath in pdfFiles)
        {
            // Build output file path (same name, different folder)
            string outputPdfPath = Path.Combine(OutputFolder, Path.GetFileName(inputPdfPath));

            // Process each PDF using the Facades API
            AddLogoToPdf(inputPdfPath, outputPdfPath);
            Console.WriteLine($"Processed: {Path.GetFileName(inputPdfPath)}");
        }

        Console.WriteLine("Batch processing completed.");
    }

    private static void AddLogoToPdf(string sourcePdf, string destinationPdf)
    {
        // Retrieve the dimensions of the first page using the Document API.
        // This avoids the non‑existent GetPageInfo method on PdfFileStamp.
        var doc = new Document(sourcePdf);
        var firstPageInfo = doc.Pages[1].PageInfo;
        float pageWidth = (float)firstPageInfo.Width;   // Cast double to float
        float pageHeight = (float)firstPageInfo.Height; // Cast double to float

        // PdfFileStamp is a facade for adding stamps (images, text, etc.) to a PDF.
        // It implements IDisposable, so we wrap it in a using block.
        using (PdfFileStamp stampFacade = new PdfFileStamp())
        {
            // Bind the source PDF file to the facade.
            stampFacade.BindPdf(sourcePdf);

            // Create a Stamp object that will hold the logo image.
            Aspose.Pdf.Facades.Stamp logoStamp = new Aspose.Pdf.Facades.Stamp();

            // Bind the logo image file to the stamp.
            logoStamp.BindImage(LogoPath);

            // Optionally set the size of the logo (width, height in points).
            logoStamp.SetImageSize(80, 40); // 80pt wide, 40pt high

            // Position the logo at the top‑right corner with a 20‑point margin.
            float x = pageWidth - 20 - 80; // page width - right margin - logo width
            float y = pageHeight - 20 - 40; // page height - top margin - logo height
            logoStamp.SetOrigin(x, y);

            // Add the stamp to all pages of the document.
            stampFacade.AddStamp(logoStamp);

            // Save the modified PDF to the destination path.
            stampFacade.Save(destinationPdf);
        }
    }
}
