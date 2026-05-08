using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

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
            // Create a PageNumberStamp with custom prefix "Chapter"
            // The placeholder '#' will be replaced by the actual page number.
            PageNumberStamp pageNumberStamp = new PageNumberStamp("Chapter #");

            // Optional styling – center the stamp at the bottom of each page
            pageNumberStamp.HorizontalAlignment = HorizontalAlignment.Center;
            pageNumberStamp.VerticalAlignment   = VerticalAlignment.Bottom;
            pageNumberStamp.BottomMargin        = 20; // distance from bottom edge
            pageNumberStamp.TextState.FontSize  = 12;
            pageNumberStamp.TextState.ForegroundColor = Aspose.Pdf.Color.DarkBlue;

            // Apply the stamp to every page in the document
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(pageNumberStamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page numbers with prefix \"Chapter\" added to '{outputPath}'.");
    }
}