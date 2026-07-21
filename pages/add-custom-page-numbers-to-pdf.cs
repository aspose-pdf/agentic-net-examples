using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // for PageNumberStamp (inherits from TextStamp)

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "paged_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle: load)
        using (Document doc = new Document(inputPath))
        {
            // Determine total page count (required for the "of Y" part)
            int totalPages = doc.Pages.Count;

            // Create a PageNumberStamp with custom format.
            // The placeholder '#' will be replaced by the current page number.
            // The total page count is inserted as a constant.
            string format = $"Page # of {totalPages}";
            PageNumberStamp pageNumberStamp = new PageNumberStamp(format);

            // Position the stamp at the bottom center of each page.
            pageNumberStamp.HorizontalAlignment = HorizontalAlignment.Center;
            pageNumberStamp.VerticalAlignment   = VerticalAlignment.Bottom;
            pageNumberStamp.BottomMargin        = 20; // distance from bottom edge

            // Apply the stamp to every page in the document.
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(pageNumberStamp);
            }

            // Save the modified PDF (lifecycle: save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page numbers added and saved to '{outputPath}'.");
    }
}