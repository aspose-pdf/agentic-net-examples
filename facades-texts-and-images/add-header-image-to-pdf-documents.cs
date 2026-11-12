using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    // Path to the header image that will be placed on each page
    private const string HeaderImagePath = "header.png";

    static void Main(string[] args)
    {
        // Expect a list of PDF file paths as command‑line arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: AddHeaderImage <pdf1> <pdf2> ...");
            return;
        }

        // Verify the header image exists once, before processing any PDF
        if (!File.Exists(HeaderImagePath))
        {
            Console.Error.WriteLine($"Header image not found: {HeaderImagePath}");
            return;
        }

        // Directory where processed PDFs will be written
        string outputDir = "Processed";
        Directory.CreateDirectory(outputDir);

        foreach (string pdfPath in args)
        {
            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"File not found: {pdfPath}");
                continue;
            }

            try
            {
                // Load the PDF using the high‑level Document API
                using (Document doc = new Document(pdfPath))
                {
                    int pageCount = doc.Pages.Count;
                    // Assume all pages have the same size as the first page
                    double pageWidth  = doc.Pages[1].PageInfo.Width;
                    double pageHeight = doc.Pages[1].PageInfo.Height;

                    // Define the header image size (full width, fixed height)
                    double headerHeight = 50; // points
                    double headerWidth  = pageWidth;

                    // Create an ImageStamp – this is the modern way to place an image on a page
                    ImageStamp headerStamp = new ImageStamp(HeaderImagePath)
                    {
                        Width       = headerWidth,
                        Height      = headerHeight,
                        LeftMargin  = 0,                     // X coordinate (lower‑left)
                        BottomMargin = pageHeight - headerHeight // Y coordinate (lower‑left)
                    };

                    // Add the stamp to every page
                    for (int i = 1; i <= pageCount; i++)
                    {
                        doc.Pages[i].AddStamp(headerStamp);
                    }

                    // Save the modified PDF to the output directory
                    string outputPath = Path.Combine(outputDir,
                        Path.GetFileNameWithoutExtension(pdfPath) + "_header.pdf");
                    doc.Save(outputPath);

                    Console.WriteLine($"Processed: {outputPath}");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}
