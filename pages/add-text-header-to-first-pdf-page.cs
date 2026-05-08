using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_header.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page firstPage = doc.Pages[1];

            // Create a HeaderFooter object for the page header
            HeaderFooter header = new HeaderFooter();

            // Configure the margin for the header (example: 20 points from the top)
            header.Margin = new MarginInfo();
            header.Margin.Top = 20;    // distance from the top edge of the page
            header.Margin.Left = 0;
            header.Margin.Right = 0;
            header.Margin.Bottom = 0;

            // Create a text fragment that will appear in the header
            TextFragment headerText = new TextFragment("Sample Header Text");
            headerText.TextState.Font = FontRepository.FindFont("Helvetica");
            headerText.TextState.FontSize = 12;
            headerText.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

            // Add the text fragment to the header's paragraph collection
            header.Paragraphs.Add(headerText);

            // Assign the header to the first page
            firstPage.Header = header;

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Header added and saved to '{outputPath}'.");
    }
}