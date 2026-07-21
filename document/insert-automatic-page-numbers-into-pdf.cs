using System;
using System.IO;
using Aspose.Pdf;

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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a page number stamp with the default format "#"
            PageNumberStamp pageNumberStamp = new PageNumberStamp();

            // Configure stamp appearance (centered at the bottom with a margin)
            pageNumberStamp.HorizontalAlignment = HorizontalAlignment.Center;
            pageNumberStamp.VerticalAlignment   = VerticalAlignment.Bottom;
            pageNumberStamp.BottomMargin        = 20; // points from the bottom edge

            // Apply the stamp to all existing pages
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(pageNumberStamp);
            }

            // Example of inserting a new page after the first page
            // The stamp is also applied to the newly inserted page
            Page newPage = doc.Pages.Insert(2);
            newPage.AddStamp(pageNumberStamp);

            // Update pagination so that all "#" placeholders reflect the current page order
            doc.Pages.UpdatePagination();

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with automatic page numbers saved to '{outputPath}'.");
    }
}