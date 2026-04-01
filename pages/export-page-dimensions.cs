using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        string pdfPath = "input.pdf";
        string csvPath = "pages-dimensions.csv";

        // Ensure the PDF exists – if not, create a simple one with a single blank page.
        if (!File.Exists(pdfPath))
        {
            using (Document tempDoc = new Document())
            {
                // Add a blank page (default size A4).
                tempDoc.Pages.Add();
                tempDoc.Save(pdfPath);
                Console.WriteLine($"Sample PDF created at '{pdfPath}' because the file was missing.");
            }
        }

        using (Document pdfDocument = new Document(pdfPath))
        {
            using (StreamWriter writer = new StreamWriter(csvPath))
            {
                // Write CSV header
                writer.WriteLine("PageNumber,Width,Height");

                for (int pageIndex = 1; pageIndex <= pdfDocument.Pages.Count; pageIndex++)
                {
                    Page page = pdfDocument.Pages[pageIndex];
                    double width = page.PageInfo.Width;
                    double height = page.PageInfo.Height;

                    // Write dimensions for the current page
                    writer.WriteLine(string.Format("{0},{1},{2}", pageIndex, width, height));
                }
            }
        }

        Console.WriteLine("Page dimensions exported to " + csvPath);
    }
}
