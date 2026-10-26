using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string imagePath = "newImage.jpg";
        const string altText = "Diagram showing sales growth over 2023";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Open the existing PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Add a new page to host the image (optional; you can use an existing page)
            Page page = doc.Pages.Add();

            // Create an ImageStamp, set its alternative text and positioning, then add it to the page
            ImageStamp stamp = new ImageStamp(imagePath)
            {
                Background = false,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                AlternativeText = altText // <-- set descriptive alt text for assistive technologies
            };
            page.AddStamp(stamp);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Image added with alt text and saved to '{outputPdf}'.");
    }
}
