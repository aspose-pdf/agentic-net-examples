using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string companyName = "Acme Corporation";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate through each page and add a header text stamp
            foreach (Page page in doc.Pages)
            {
                // Create a TextStamp with the company name
                TextStamp headerStamp = new TextStamp(companyName);

                // Position the stamp at the top center of the page
                headerStamp.HorizontalAlignment = HorizontalAlignment.Center;
                headerStamp.VerticalAlignment   = VerticalAlignment.Top;
                headerStamp.TopMargin = 10; // distance from the top edge

                // Add the stamp to the current page
                page.AddStamp(headerStamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Header stamp added to all pages. Saved as '{outputPath}'.");
    }
}