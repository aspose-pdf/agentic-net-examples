using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // required for FontRepository

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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create a PageNumberStamp with default format "#"
            PageNumberStamp pageNumberStamp = new PageNumberStamp();

            // Set custom font "Arial" and size 14 points
            pageNumberStamp.TextState.Font = FontRepository.FindFont("Arial");
            pageNumberStamp.TextState.FontSize = 14;

            // Optional: position the page number at the bottom center of each page
            pageNumberStamp.HorizontalAlignment = HorizontalAlignment.Center;
            pageNumberStamp.VerticalAlignment   = VerticalAlignment.Bottom;

            // Apply the stamp to every page in the document
            foreach (Page page in doc.Pages)
            {
                pageNumberStamp.Put(page);
            }

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page numbers added and saved to '{outputPath}'.");
    }
}