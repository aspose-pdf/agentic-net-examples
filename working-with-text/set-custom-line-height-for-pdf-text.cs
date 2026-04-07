using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Create a text fragment with multiple lines
            TextFragment fragment = new TextFragment("First line\nSecond line\nThird line");

            // Set custom line spacing (line height) via TextState.LineSpacing
            // The value is in points; here we set 20 points between lines
            fragment.TextState.LineSpacing = 20f;

            // Optionally set other text properties
            fragment.TextState.Font = FontRepository.FindFont("Helvetica");
            fragment.TextState.FontSize = 12;
            fragment.TextState.ForegroundColor = Color.Black;

            // Add the fragment to the page
            page.Paragraphs.Add(fragment);

            // Save the PDF to a file
            doc.Save("CustomLineHeight.pdf");
        }

        Console.WriteLine("PDF created with custom line height.");
    }
}