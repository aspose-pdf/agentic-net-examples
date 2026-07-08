using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        Document doc = new Document();

        // Add a page that will contain the TOC
        Page tocPage = doc.Pages.Add();

        // Title for the Table of Contents
        TextFragment title = new TextFragment("Table of Contents");
        title.TextState.Font = FontRepository.FindFont("Helvetica");
        title.TextState.FontSize = 20;
        title.TextState.FontStyle = FontStyles.Bold;
        title.Margin = new MarginInfo { Top = 20 };
        tocPage.Paragraphs.Add(title);

        // Create a table to hold TOC entries
        Table tocTable = new Table();
        tocTable.ColumnWidths = "500"; // single column width

        // Helper to add a TOC entry with dot leaders
        void AddTocEntry(string entryText, int pageNumber)
        {
            // Define tab stops: position where the page number will appear,
            // using Dot leader type to draw the dotted line.
            TabStops tabs = new TabStops();
            // Position is measured from the left edge of the page.
            // Adjust as needed for your layout.
            tabs.Add(450, TabLeaderType.Dot);

            // Build the text: entry text, a tab character, then the page number.
            string fullText = $"{entryText}\t{pageNumber}";

            // Create the TextFragment with the defined tab stops.
            TextFragment entryFragment = new TextFragment(fullText, tabs);
            entryFragment.TextState.Font = FontRepository.FindFont("Helvetica");
            entryFragment.TextState.FontSize = 12;
            entryFragment.Margin = new MarginInfo { Top = 5 };

            // Add a new row with a single cell containing the fragment.
            Row row = tocTable.Rows.Add();
            Cell cell = row.Cells.Add();
            cell.Paragraphs.Add(entryFragment);
        }

        // Sample TOC entries
        AddTocEntry("Chapter 1: Introduction", 2);
        AddTocEntry("Chapter 2: Getting Started", 5);
        AddTocEntry("Chapter 3: Advanced Topics", 12);
        AddTocEntry("Appendix A: References", 20);

        // Add the populated table to the page
        tocPage.Paragraphs.Add(tocTable);

        // Save the PDF
        doc.Save("TableOfContents.pdf");
    }
}
