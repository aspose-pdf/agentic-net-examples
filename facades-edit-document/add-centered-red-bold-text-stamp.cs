using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

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

        // Open the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least 5 pages
            if (doc.Pages.Count < 5)
            {
                Console.Error.WriteLine("The document does not contain a fifth page.");
                return;
            }

            // Create a TextStamp with the desired text
            Aspose.Pdf.TextStamp textStamp = new Aspose.Pdf.TextStamp(stampText);

            // Configure the text appearance: bold font, red color
            Aspose.Pdf.Text.TextState ts = textStamp.TextState;
            ts.Font = Aspose.Pdf.Text.FontRepository.FindFont("Helvetica-Bold");
            ts.FontSize = 24; // adjust size as needed
            ts.ForegroundColor = Aspose.Pdf.Color.Red;

            // Center the stamp horizontally on the page
            textStamp.HorizontalAlignment = Aspose.Pdf.HorizontalAlignment.Center;

            // Optionally, set vertical alignment (e.g., middle of the page)
            textStamp.VerticalAlignment = Aspose.Pdf.VerticalAlignment.Center;

            // Add the stamp to page 5 (Aspose.Pdf uses 1‑based indexing)
            doc.Pages[5].AddStamp(textStamp);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text stamp added and saved to '{outputPath}'.");
    }
}