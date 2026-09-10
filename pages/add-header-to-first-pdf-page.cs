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

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Create a HeaderFooter object for the first page header
            HeaderFooter header = new HeaderFooter();

            // Configure the margin for the header using MarginInfo (values are in points)
            header.Margin = new MarginInfo
            {
                Top = 20,    // 20 points from the top of the page
                Left = 0,
                Right = 0,
                Bottom = 0
            };

            // Create a text fragment that will appear in the header
            TextFragment headerText = new TextFragment("Document Header");
            // Set visual appearance of the header text via the existing TextState instance
            headerText.TextState.Font = FontRepository.FindFont("Helvetica");
            headerText.TextState.FontSize = 12;
            headerText.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;

            // Add the text fragment to the header's paragraph collection
            header.Paragraphs.Add(headerText);

            // Assign the header to the first page
            doc.Pages[1].Header = header;

            // Save the modified PDF (lifecycle rule: save inside using)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Header added and saved to '{outputPath}'.");
    }
}