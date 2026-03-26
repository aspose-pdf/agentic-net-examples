using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Create a page number stamp with default format "#"
            PageNumberStamp pageNumberStamp = new PageNumberStamp();
            // Align the stamp to the center of the footer
            pageNumberStamp.HorizontalAlignment = HorizontalAlignment.Center;
            pageNumberStamp.VerticalAlignment = VerticalAlignment.Bottom;
            // Set a bottom margin (distance from the page edge)
            pageNumberStamp.BottomMargin = 10f;
            // Define font appearance
            pageNumberStamp.TextState.Font = FontRepository.FindFont("Helvetica");
            pageNumberStamp.TextState.FontSize = 12;
            pageNumberStamp.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

            // Apply the stamp to every page in the document
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                page.AddStamp(pageNumberStamp);
            }

            doc.Save(outputPath);
        }

        Console.WriteLine($"Page numbers added. Saved to '{outputPath}'.");
    }
}