using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // TextStamp lives here

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

        // Load the PDF and add a header stamp to every page
        using (Document doc = new Document(inputPath))
        {
            foreach (Page page in doc.Pages)
            {
                // Create a TextStamp that will act as the header
                TextStamp headerStamp = new TextStamp(companyName);
                headerStamp.TextState.Font = FontRepository.FindFont("Helvetica");
                headerStamp.TextState.FontSize = 12;
                headerStamp.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
                headerStamp.HorizontalAlignment = HorizontalAlignment.Center;
                headerStamp.VerticalAlignment = VerticalAlignment.Top;
                headerStamp.YIndent = 20; // distance from the top edge

                // Apply the stamp to the current page
                page.AddStamp(headerStamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Header stamp added to all pages. Output saved to '{outputPath}'.");
    }
}
