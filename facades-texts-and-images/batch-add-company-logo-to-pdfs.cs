using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class BatchAddLogo
{
    static void Main()
    {
        // Folder containing the PDFs to process
        const string inputFolder = @"C:\PdfInput";
        // Folder where the processed PDFs will be saved
        const string outputFolder = @"C:\PdfOutput";
        // Path to the company logo PNG
        const string logoPath = @"C:\Assets\company_logo.png";

        // Ensure output folder exists
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string pdfFile in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            // Bind the PDF, add the logo, and save the result
            using (PdfFileMend mend = new PdfFileMend())
            {
                // Load the PDF into the facade
                mend.BindPdf(pdfFile);

                // Determine the pages to which the logo will be added (all pages)
                int pageCount = mend.Document.Pages.Count; // 1‑based indexing
                int[] allPages = Enumerable.Range(1, pageCount).ToArray();

                // Retrieve page dimensions from the first page (assumes uniform size)
                Page firstPage = mend.Document.Pages[1];
                float pageWidth  = (float)firstPage.PageInfo.Width;   // explicit cast from double to float
                float pageHeight = (float)firstPage.PageInfo.Height;  // explicit cast from double to float

                // Desired logo size (adjust as needed)
                const float logoWidth  = 100f; // points
                const float logoHeight = 50f;  // points

                // Position the logo at the top‑right corner with a 10‑point margin
                float lowerLeftX  = pageWidth  - logoWidth  - 10f;
                float lowerLeftY  = pageHeight - logoHeight - 10f;
                float upperRightX = lowerLeftX + logoWidth;
                float upperRightY = lowerLeftY + logoHeight;

                // Add the logo image to all pages
                using (FileStream imgStream = File.OpenRead(logoPath))
                {
                    mend.AddImage(imgStream, allPages,
                                  lowerLeftX, lowerLeftY,
                                  upperRightX, upperRightY);
                }

                // Save the modified PDF to the output folder
                string outputPath = Path.Combine(outputFolder,
                                   Path.GetFileNameWithoutExtension(pdfFile) + "_logo.pdf");
                mend.Save(outputPath);
            }

            Console.WriteLine($"Processed: {Path.GetFileName(pdfFile)}");
        }

        Console.WriteLine("Batch processing completed.");
    }
}
