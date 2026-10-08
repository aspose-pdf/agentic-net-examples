using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "form.pdf";
        const string stampImagePath = "stamp.png";
        const string outputPath = "form_stamped.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(stampImagePath))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImagePath}");
            return;
        }

        try
        {
            // Load the PDF containing form fields.
            using (Document pdfDoc = new Document(inputPath))
            {
                // Configure the image stamp.
                ImageStamp imgStamp = new ImageStamp(stampImagePath)
                {
                    Background = false,                     // place stamp over existing content
                    Opacity = 0.5f,                         // optional transparency
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Top,
                    TopMargin = 20                          // distance from the top edge of the page
                };

                // Apply the stamp to each page individually (per‑page AddStamp).
                foreach (Page page in pdfDoc.Pages)
                {
                    page.AddStamp(imgStamp);
                }

                // Save the PDF. Form fields remain functional because we do not flatten the document.
                pdfDoc.Save(outputPath);
            }

            Console.WriteLine($"Image stamp added. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
