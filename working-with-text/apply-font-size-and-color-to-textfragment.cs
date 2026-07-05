using System;
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
            doc.Pages.Add();
            Page page = doc.Pages[1];

            // Create a text fragment with the desired content
            TextFragment fragment = new TextFragment("Styled text example");

            // Define font size and color via the TextState
            fragment.TextState.FontSize = 24;                         // Set font size
            fragment.TextState.ForegroundColor = Color.Blue;          // Set text color

            // Add the fragment to the page
            page.Paragraphs.Add(fragment);

            // Save the PDF
            doc.Save("styled_output.pdf");
        }

        Console.WriteLine("PDF created with styled text.");
    }
}