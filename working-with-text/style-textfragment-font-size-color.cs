using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "styled_output.pdf";

        // Create a new PDF document inside a using block for proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Create a TextFragment with the desired text
            TextFragment fragment = new TextFragment("Hello, styled world!");

            // Set font size, color, and font via the fragment's TextState
            fragment.TextState.FontSize = 20;                         // font size
            fragment.TextState.ForegroundColor = Aspose.Pdf.Color.Blue; // text color
            fragment.TextState.Font = FontRepository.FindFont("Helvetica"); // font family

            // Add the styled fragment to the page
            page.Paragraphs.Add(fragment);

            // Save the document to the specified path
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}