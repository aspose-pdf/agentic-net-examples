using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_header.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (1‑based indexing)
            Page firstPage = doc.Pages[1];

            // Create a HeaderFooter object for the header
            HeaderFooter header = new HeaderFooter();

            // Configure margin information (example: 20 points top margin)
            // In recent Aspose.Pdf versions MarginInfo uses the properties Top, Bottom, Left, Right
            header.Margin = new MarginInfo
            {
                Top = 20 // set the desired top margin in points
            };

            // Add a text fragment to the header
            TextFragment headerText = new TextFragment("Document Header");
            headerText.TextState.Font = FontRepository.FindFont("Helvetica");
            headerText.TextState.FontSize = 12;
            headerText.TextState.ForegroundColor = Aspose.Pdf.Color.DarkGray;

            header.Paragraphs.Add(headerText);

            // Assign the header to the first page
            firstPage.Header = header;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Header added and saved to '{outputPath}'.");
    }
}
