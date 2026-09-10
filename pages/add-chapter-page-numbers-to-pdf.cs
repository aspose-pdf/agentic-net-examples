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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create a page number stamp with custom prefix "Chapter"
            PageNumberStamp pageNumberStamp = new PageNumberStamp("Chapter #");

            // Position the stamp at the bottom center of each page
            pageNumberStamp.HorizontalAlignment = HorizontalAlignment.Center;
            pageNumberStamp.VerticalAlignment   = VerticalAlignment.Bottom;
            pageNumberStamp.BottomMargin        = 20; // 20 points margin from bottom

            // Define visual appearance of the stamp
            pageNumberStamp.TextState.Font       = FontRepository.FindFont("Helvetica");
            pageNumberStamp.TextState.FontSize   = 12;
            pageNumberStamp.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

            // Add the stamp to every page in the document
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(pageNumberStamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page numbers with prefix \"Chapter\" added. Saved to '{outputPath}'.");
    }
}