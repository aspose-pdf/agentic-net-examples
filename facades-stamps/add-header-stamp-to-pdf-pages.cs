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

        // Load the document.
        using (Document doc = new Document(inputPath))
        {
            // Iterate through each page and add a header stamp.
            foreach (Page page in doc.Pages)
            {
                // Create a TextStamp for the header.
                TextStamp headerStamp = new TextStamp(companyName);
                headerStamp.TextState.Font = FontRepository.FindFont("Helvetica");
                headerStamp.TextState.FontSize = 12;
                headerStamp.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
                headerStamp.HorizontalAlignment = HorizontalAlignment.Center;
                headerStamp.VerticalAlignment = VerticalAlignment.Top;
                headerStamp.YIndent = 20f; // offset from the top edge

                // Add the stamp to the current page.
                page.AddStamp(headerStamp);
            }

            // Save the modified document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Header stamp added to all pages. Output saved to '{outputPath}'.");
    }
}
