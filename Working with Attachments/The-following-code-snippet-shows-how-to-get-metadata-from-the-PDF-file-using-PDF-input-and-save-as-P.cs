using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source PDF exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the metadata object using doc.Info for standard PDF info dictionary
            Console.WriteLine($"Title:  {doc.Info.Title ?? "N/A"}");
            Console.WriteLine($"Author: {doc.Info.Author ?? "N/A"}");
            Console.WriteLine($"Subject:{doc.Info.Subject ?? "N/A"}");
            Console.WriteLine($"Keywords:{doc.Info.Keywords ?? "N/A"}");
            Console.WriteLine($"Creator: {doc.Info.Creator ?? "N/A"}");
            Console.WriteLine($"Producer:{doc.Info.Producer ?? "N/A"}");
            
            // Handle DateTime fields separately since Aspose.Pdf uses non-nullable DateTime
            // In Aspose.Pdf, CreationDate and ModDate are DateTime (not DateTime?), so no HasValue/Value
            // However, Aspose.Pdf returns DateTime.MinValue for missing dates, so check for that
            string creationDate = doc.Info.CreationDate != DateTime.MinValue ? doc.Info.CreationDate.ToString("yyyy-MM-dd HH:mm:ss") : "N/A";
            string modDate = doc.Info.ModDate != DateTime.MinValue ? doc.Info.ModDate.ToString("yyyy-MM-dd HH:mm:ss") : "N/A";
            
            Console.WriteLine($"CreationDate: {creationDate}");
            Console.WriteLine($"ModDate: {modDate}");

            // Save the (unchanged) document to a new PDF file.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}