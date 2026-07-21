using System;
using System.IO;
using System.Text;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputCsv = "pages_dimensions.csv";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document (using statement ensures proper disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Create the CSV file and write header
            using (StreamWriter writer = new StreamWriter(outputCsv, false, Encoding.UTF8))
            {
                writer.WriteLine("PageNumber,Width,Height");

                // Pages are 1‑based in Aspose.Pdf
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Page page = doc.Pages[i];
                    double width = page.PageInfo.Width;
                    double height = page.PageInfo.Height;

                    writer.WriteLine($"{i},{width},{height}");
                }
            }
        }

        Console.WriteLine($"Page dimensions exported to '{outputCsv}'.");
    }
}