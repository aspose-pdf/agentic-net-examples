using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class BatchAddLogo
{
    static void Main()
    {
        // Folder containing PDFs to process
        const string inputFolder = @"C:\PdfFolder";
        // Path to the company logo PNG
        const string logoPath = @"C:\Images\company_logo.png";
        // Output folder for processed PDFs
        const string outputFolder = @"C:\PdfFolder\Processed";

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Validate logo file exists
        if (!File.Exists(logoPath))
        {
            Console.Error.WriteLine($"Logo file not found: {logoPath}");
            return;
        }

        // Process each PDF file in the input folder
        foreach (string pdfFile in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            try
            {
                // Determine output file name (original name with suffix)
                string fileName = Path.GetFileNameWithoutExtension(pdfFile);
                string outputPath = Path.Combine(outputFolder, $"{fileName}_logo.pdf");

                // Load the document only to obtain the page count (required for AddImage)
                int[] pageNumbers;
                using (Document doc = new Document(pdfFile))
                {
                    // Aspose.Pdf uses 1‑based page indexing
                    pageNumbers = Enumerable.Range(1, doc.Pages.Count).ToArray();
                }

                // Use PdfFileMend (Facade) to add the logo image to all pages
                using (PdfFileMend mend = new PdfFileMend())
                {
                    // Bind the source PDF
                    mend.BindPdf(pdfFile);

                    // Define logo placement coordinates.
                    // Adjust these values according to your page size and desired margin.
                    // Example assumes a standard A4 page (595 x 842 points) and a 50x50 logo.
                    float logoWidth = 50f;
                    float logoHeight = 50f;
                    float marginRight = 20f;
                    float marginTop = 20f;

                    // Upper‑right corner coordinates
                    // lowerLeftX = pageWidth - marginRight - logoWidth
                    // lowerLeftY = pageHeight - marginTop - logoHeight
                    // upperRightX = pageWidth - marginRight
                    // upperRightY = pageHeight - marginTop
                    // For simplicity, use typical A4 dimensions; if pages differ, adjust accordingly.
                    float pageWidth = 595f;
                    float pageHeight = 842f;

                    float lowerLeftX = pageWidth - marginRight - logoWidth;
                    float lowerLeftY = pageHeight - marginTop - logoHeight;
                    float upperRightX = pageWidth - marginRight;
                    float upperRightY = pageHeight - marginTop;

                    // Add the logo image to all pages
                    bool added = mend.AddImage(logoPath, pageNumbers,
                                               lowerLeftX, lowerLeftY,
                                               upperRightX, upperRightY);

                    if (!added)
                    {
                        Console.Error.WriteLine($"Failed to add logo to: {pdfFile}");
                        continue;
                    }

                    // Save the modified PDF
                    mend.Save(outputPath);
                }

                Console.WriteLine($"Processed: {pdfFile} -> {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfFile}': {ex.Message}");
            }
        }
    }
}