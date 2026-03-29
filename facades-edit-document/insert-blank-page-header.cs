using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

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
            // Insert a blank page at position 3 (1‑based indexing)
            Page newPage = doc.Pages.Insert(3);

            // Determine page dimensions
            double pageWidth = newPage.PageInfo.Width;
            double pageHeight = newPage.PageInfo.Height;

            // Define a rectangle at the top of the page for the header annotation
            Aspose.Pdf.Rectangle headerRect = new Aspose.Pdf.Rectangle(0, pageHeight - 50, pageWidth, pageHeight);

            // Create a header annotation (PageInformationAnnotation) and set its properties
            PageInformationAnnotation header = new PageInformationAnnotation(newPage, headerRect)
            {
                Contents = "Header Text",
                Color = Aspose.Pdf.Color.LightGray
            };

            // Add the annotation to the newly inserted page
            newPage.Annotations.Add(header);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved file with inserted page and header to '{outputPath}'.");
    }
}