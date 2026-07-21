using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create a page number stamp
            PageNumberStamp stamp = new PageNumberStamp();
            stamp.StartingNumber      = 1;                         // start numbering at 1
            stamp.HorizontalAlignment = HorizontalAlignment.Center; // center horizontally
            stamp.VerticalAlignment   = VerticalAlignment.Bottom;   // place at bottom (optional)

            // Apply the stamp to every page
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(stamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page numbers added and saved to '{outputPath}'.");
    }
}