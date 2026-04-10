using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class BatchLogoAdder
{
    static void Main(string[] args)
    {
        // args[0] = input folder containing PDFs
        // args[1] = path to the company logo PNG
        // args[2] = output folder for processed PDFs
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: BatchLogoAdder <inputFolder> <logoPath> <outputFolder>");
            return;
        }

        string inputFolder = args[0];
        string logoPath    = args[1];
        string outputFolder = args[2];

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        if (!File.Exists(logoPath))
        {
            Console.Error.WriteLine($"Logo file not found: {logoPath}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Fixed logo size (points) and margin from page edges
        const float logoWidth  = 100f; // width of logo
        const float logoHeight = 50f;  // height of logo
        const float margin     = 10f;  // distance from top/right edges

        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileName(pdfPath);
            string outputPath = Path.Combine(outputFolder, fileName);

            try
            {
                // Load the PDF to obtain page dimensions and count
                using (Document doc = new Document(pdfPath))
                {
                    int pageCount = doc.Pages.Count;

                    // Initialize the facade and bind the loaded document
                    using (PdfFileMend mend = new PdfFileMend())
                    {
                        mend.BindPdf(doc);

                        // Add the logo to each page at the top‑right corner
                        for (int pageNum = 1; pageNum <= pageCount; pageNum++)
                        {
                            Page page = doc.Pages[pageNum];
                            // PageInfo.Width/Height are double – cast to float for AddImage
                            float pageWidth  = (float)page.PageInfo.Width;
                            float pageHeight = (float)page.PageInfo.Height;

                            // Calculate rectangle coordinates for the logo (all float)
                            float lowerLeftX  = pageWidth - margin - logoWidth;
                            float lowerLeftY  = pageHeight - margin - logoHeight;
                            float upperRightX = pageWidth - margin;
                            float upperRightY = pageHeight - margin;

                            // Add the image using the overload that accepts a file path
                            mend.AddImage(logoPath, pageNum, lowerLeftX, lowerLeftY, upperRightX, upperRightY);
                        }

                        // Save the modified PDF to the output location
                        mend.Save(outputPath);
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
