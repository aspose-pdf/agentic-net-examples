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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Apply a header to every page
            foreach (Page page in doc.Pages)
            {
                // Create a new header/footer container
                page.Header = new HeaderFooter();

                // Create a text fragment for the header
                TextFragment headerText = new TextFragment("SECTION HEADING");
                headerText.TextState.Font = FontRepository.FindFont("Helvetica");
                headerText.TextState.FontStyle = FontStyles.Bold;   // bold
                headerText.TextState.FontSize = 12;                // reasonable size
                headerText.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
                headerText.HorizontalAlignment = HorizontalAlignment.Center; // center align

                // Add the text fragment to the header
                page.Header.Paragraphs.Add(headerText);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with headers saved to '{outputPath}'.");
    }
}