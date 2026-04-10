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
        const string companyName = "Acme Corporation";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document.
        Document doc = new Document(inputPath);

        // Iterate through every page and add a header text stamp.
        foreach (Page page in doc.Pages)
        {
            // Create a TextStamp with the company name.
            TextStamp headerStamp = new TextStamp(companyName);

            // Configure the visual appearance of the stamp.
            headerStamp.TextState.Font = FontRepository.FindFont("Helvetica");
            headerStamp.TextState.FontSize = 12;
            headerStamp.TextState.ForegroundColor = Aspose.Pdf.Color.Black; // fully‑qualified Aspose color

            // Position the stamp at the top centre of the page.
            headerStamp.HorizontalAlignment = HorizontalAlignment.Center;
            headerStamp.VerticalAlignment = VerticalAlignment.Top;
            headerStamp.YIndent = 20f; // distance from the top edge

            // Add the stamp to the current page.
            page.AddStamp(headerStamp);
        }

        // Save the modified document.
        doc.Save(outputPath);
        doc.Dispose();

        Console.WriteLine($"Header stamp added to all pages. Output saved to '{outputPath}'.");
    }
}
