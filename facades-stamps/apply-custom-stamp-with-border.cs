using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "highlighted.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Apply a stamp to each page (customize as needed)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Define the stamp rectangle (left, bottom, right, top)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

                // Create a stamp annotation on the page
                StampAnnotation stamp = new StampAnnotation(page, rect)
                {
                    // Optional text shown when the stamp is selected
                    Contents = "Important Section",
                    // Set the border color (the border itself has no Color property)
                    Color = Aspose.Pdf.Color.Red
                };

                // Set border thickness via the Border object (requires the parent annotation)
                stamp.Border = new Border(stamp) { Width = 2 };

                // Add the stamp annotation to the page
                page.Annotations.Add(stamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}