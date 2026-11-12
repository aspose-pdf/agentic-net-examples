using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string companyName = "Acme Corp";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create a text stamp that will serve as the header
            TextStamp headerStamp = new TextStamp(companyName)
            {
                // Center the stamp at the top of the page
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Top,
                TopMargin           = 10, // distance from the top edge

                // Visual styling
                TextState = {
                    FontSize      = 12,
                    FontStyle     = FontStyles.Bold,
                    ForegroundColor = Color.DarkBlue
                }
            };

            // Add the stamp to every page
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(headerStamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Header stamp added to all pages. Saved as '{outputPath}'.");
    }
}
