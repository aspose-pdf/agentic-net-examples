using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputFolder = @"C:\InputPdfs";
        const string outputFolder = @"C:\OutputPdfs";
        const string logoPath = @"C:\Assets\company_logo.png";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);
        foreach (string pdfFile in pdfFiles)
        {
            string fileName = Path.GetFileName(pdfFile);
            string outputPath = Path.Combine(outputFolder, fileName);

            try
            {
                using (Document doc = new Document(pdfFile))
                {
                    // Assume all pages have the same size as the first page
                    Page firstPage = doc.Pages[1];
                    double pageWidth = firstPage.PageInfo.Width;
                    double pageHeight = firstPage.PageInfo.Height;

                    // Desired logo size (in points) and margin from edges
                    const double logoWidth = 100;   // adjust as needed
                    const double logoHeight = 50;   // adjust as needed
                    const double margin = 10;

                    // Calculate rectangle for top‑right corner
                    double lowerLeftX = pageWidth - logoWidth - margin;
                    double lowerLeftY = pageHeight - logoHeight - margin;
                    double upperRightX = pageWidth - margin;
                    double upperRightY = pageHeight - margin;

                    // Build an array of all page numbers (1‑based)
                    int pageCount = doc.Pages.Count;
                    int[] allPages = new int[pageCount];
                    for (int i = 0; i < pageCount; i++)
                        allPages[i] = i + 1; // 1‑based indexing

                    // Use PdfFileMend to add the logo to every page
                    using (PdfFileMend mend = new PdfFileMend(doc))
                    {
                        mend.AddImage(logoPath, allPages,
                                      (float)lowerLeftX, (float)lowerLeftY,
                                      (float)upperRightX, (float)upperRightY);
                        mend.Save(outputPath);
                        mend.Close();
                    }
                }

                Console.WriteLine($"Processed: {fileName}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{fileName}': {ex.Message}");
            }
        }
    }
}