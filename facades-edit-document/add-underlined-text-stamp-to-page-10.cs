using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string stampText  = "UNDERLINED TEXT";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the source PDF using the Facades API
        using (PdfFileStamp fileStamp = new PdfFileStamp())
        {
            fileStamp.BindPdf(inputPath);

            // Access the underlying Document to get the target page (page 10, 1‑based indexing)
            Document doc = fileStamp.Document;
            if (doc.Pages.Count < 10)
            {
                Console.Error.WriteLine("The document has fewer than 10 pages.");
                return;
            }
            Page targetPage = doc.Pages[10];

            // Create a TextStamp with the desired text
            TextStamp stamp = new TextStamp(stampText);

            // Configure underline decoration
            stamp.TextState.Underline = true;

            // Set yellow background for the stamp area
            stamp.TextState.BackgroundColor = Aspose.Pdf.Color.Yellow;

            // Center the stamp horizontally and vertically on the page
            stamp.HorizontalAlignment = HorizontalAlignment.Center;
            stamp.VerticalAlignment   = VerticalAlignment.Center;

            // Place the stamp on the specific page
            stamp.Put(targetPage);

            // Save the modified PDF
            fileStamp.Save(outputPath);
            fileStamp.Close();
        }

        Console.WriteLine($"Text stamp added to page 10 and saved as '{outputPath}'.");
    }
}