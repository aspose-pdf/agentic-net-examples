using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_footer.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document (using rule for document creation)
        using (Document doc = new Document(inputPath))
        {
            // Create a page number stamp with the desired format.
            // "#" is the placeholder for the current page number,
            // the second "#" will be replaced by the total page count.
            PageNumberStamp pageNumberStamp = new PageNumberStamp("Page # of #");

            // Position the stamp at the bottom center of each page.
            pageNumberStamp.HorizontalAlignment = HorizontalAlignment.Center;
            pageNumberStamp.VerticalAlignment   = VerticalAlignment.Bottom;

            // Add the stamp to every page in the document.
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(pageNumberStamp);
            }

            // Update the placeholders (#) with actual page numbers and total count.
            doc.Pages.UpdatePagination();

            // Save the modified PDF (using rule for document saving)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with footer: {outputPath}");
    }
}