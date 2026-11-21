using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;   // Facades namespace included as requested
using Aspose.Pdf.Text;      // for TextStamp (inherits from Stamp)

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "even_pages_stamped.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Document must be disposed – use a using block
            using (Document doc = new Document(inputPath))
            {
                // Pages are 1‑based (Aspose.Pdf rule)
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    // Apply stamp only to even‑numbered pages
                    if (i % 2 == 0)
                    {
                        Page page = doc.Pages[i];

                        // Concrete stamp class – TextStamp (Stamp is abstract)
                        TextStamp stamp = new TextStamp("CONFIDENTIAL")
                        {
                            // Position the stamp in the centre of the page
                            HorizontalAlignment = HorizontalAlignment.Center,
                            VerticalAlignment   = VerticalAlignment.Center,
                            // Optional rotation (degrees)
                            RotateAngle = 0.0,
                            // Make the stamp semi‑transparent
                            Opacity = 0.5
                        };

                        // Add the stamp to the current page
                        page.AddStamp(stamp);
                    }
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Even pages stamped and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}