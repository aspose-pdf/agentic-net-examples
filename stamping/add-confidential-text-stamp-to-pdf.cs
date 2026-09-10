using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string stampText  = "CONFIDENTIAL";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Create a text stamp with the desired label
            TextStamp textStamp = new TextStamp(stampText);
            textStamp.Opacity = 0.6; // subtle opacity

            // Position the stamp (centered on each page)
            textStamp.HorizontalAlignment = HorizontalAlignment.Center;
            textStamp.VerticalAlignment   = VerticalAlignment.Center;

            // Apply the stamp to every page
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(textStamp);
            }

            // Save the modified PDF (lifecycle rule: use Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}