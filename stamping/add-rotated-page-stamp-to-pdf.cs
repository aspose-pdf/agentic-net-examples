using System;
using System.IO;
using Aspose.Pdf;               // Core API
using Aspose.Pdf.Facades;      // For PdfPageStamp (inherits from Stamp)

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_stamped.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document doc = new Document(inputPath))
        {
            // Use the first page as the stamp source (any page can be used)
            Page stampSource = doc.Pages[1];

            // Apply the rotated stamp to each page in the document
            foreach (Page targetPage in doc.Pages)
            {
                // Create a new PdfPageStamp for the current target page
                PdfPageStamp stamp = new PdfPageStamp(stampSource);

                // Rotate the stamp content by 90 degrees (portrait -> landscape)
                stamp.Rotate = Rotation.on90; // Fixed enum value

                // Optional: place the stamp in the center of the page
                stamp.HorizontalAlignment = HorizontalAlignment.Center;
                stamp.VerticalAlignment   = VerticalAlignment.Center;

                // Add the stamp to the target page (lifecycle rule: Page.AddStamp)
                targetPage.AddStamp(stamp);
            }

            // Save the modified PDF (lifecycle rule: Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}
