using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class BatchAddLogo
{
    static void Main(string[] args)
    {
        // Input folder containing PDFs
        string inputFolder = @"C:\InputPdfs";
        // Output folder for processed PDFs
        string outputFolder = @"C:\OutputPdfs";
        // Path to the company logo PNG
        string logoPath = @"C:\Resources\logo.png";

        // Ensure output folder exists
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string pdfFile in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            // Load the PDF to obtain page dimensions
            using (Document doc = new Document(pdfFile))
            {
                int pageCount = doc.Pages.Count;

                // Initialize PdfFileMend facade
                using (PdfFileMend mender = new PdfFileMend())
                {
                    // Bind the source PDF
                    mender.BindPdf(pdfFile);

                    // Define logo size and margin (in points) – use float because AddImage expects float
                    const float logoWidth = 100f;   // adjust as needed
                    const float logoHeight = 50f;   // adjust as needed
                    const float margin = 10f;       // distance from top/right edges

                    // Add the logo to each page at the top‑right corner
                    for (int i = 1; i <= pageCount; i++)
                    {
                        Page page = doc.Pages[i];
                        // Convert page dimensions to float
                        float pageWidth = (float)page.PageInfo.Width;
                        float pageHeight = (float)page.PageInfo.Height;

                        // Calculate lower‑left coordinates for the logo using float arithmetic
                        float llx = pageWidth - logoWidth - margin;
                        float lly = pageHeight - logoHeight - margin;
                        float urx = llx + logoWidth;
                        float ury = lly + logoHeight;

                        // Open the logo image stream for each addition
                        using (FileStream imgStream = File.OpenRead(logoPath))
                        {
                            // AddImage expects float coordinates
                            mender.AddImage(imgStream, i, llx, lly, urx, ury);
                        }
                    }

                    // Determine output file path
                    string outputPath = Path.Combine(
                        outputFolder,
                        Path.GetFileNameWithoutExtension(pdfFile) + "_logo.pdf");

                    // Save the modified PDF
                    mender.Save(outputPath);
                }
            }

            Console.WriteLine($"Processed: {Path.GetFileName(pdfFile)}");
        }

        Console.WriteLine("Batch processing completed.");
    }
}
