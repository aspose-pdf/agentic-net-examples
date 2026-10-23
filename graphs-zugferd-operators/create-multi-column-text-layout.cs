using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "multi_column_output.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Use a FloatingBox to define a multi‑column layout
            FloatingBox box = new FloatingBox();
            box.ColumnInfo = new ColumnInfo
            {
                ColumnCount = 2,                 // Two columns
                ColumnWidths = "250 250",       // Width of each column (space‑separated)
                ColumnSpacing = "10"           // Gap between columns (points)
            };

            // Sample long text to demonstrate column flow
            string sampleText = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. 
Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. 
Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. 
Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. 
Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";

            // Create a TextFragment with the sample text
            TextFragment textFragment = new TextFragment(sampleText);
            textFragment.TextState.Font = FontRepository.FindFont("Arial");
            textFragment.TextState.FontSize = 12;
            textFragment.TextState.ForegroundColor = Color.Black;

            // Add the TextFragment to the FloatingBox
            box.Paragraphs.Add(textFragment);

            // Add the FloatingBox to the page
            page.Paragraphs.Add(box);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with multi‑column text saved to '{outputPath}'.");
    }
}
