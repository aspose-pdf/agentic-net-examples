using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_page_numbers.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Create a page number stamp
            PageNumberStamp pageNumberStamp = new PageNumberStamp
            {
                StartingNumber = 1,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Bottom,
                BottomMargin = 20
            };

            // Configure appearance via the existing (read‑only) TextState instance
            pageNumberStamp.TextState.FontSize = 12;
            pageNumberStamp.TextState.FontStyle = FontStyles.Bold;
            pageNumberStamp.TextState.ForegroundColor = Color.Black;

            // Apply the stamp to every page
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(pageNumberStamp);
            }

            doc.Save(outputPath);
        }

        Console.WriteLine($"Page numbers added and saved to '{outputPath}'.");
    }
}
