using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_numbered.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a PageNumberStamp with default format ("#")
            PageNumberStamp pageNumberStamp = new PageNumberStamp();

            // Configure the appearance: Arial, 14 points, black color
            pageNumberStamp.TextState.Font = FontRepository.FindFont("Arial");
            pageNumberStamp.TextState.FontSize = 14;
            pageNumberStamp.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

            // Position the stamp at the bottom-center of each page
            pageNumberStamp.HorizontalAlignment = HorizontalAlignment.Center;
            pageNumberStamp.VerticalAlignment   = VerticalAlignment.Bottom;
            pageNumberStamp.BottomMargin = 20; // optional margin from bottom edge

            // Apply the stamp to every page in the document
            foreach (Page page in doc.Pages)
            {
                // The Put method adds the page number to the given page
                pageNumberStamp.Put(page);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page numbers added and saved to '{outputPath}'.");
    }
}