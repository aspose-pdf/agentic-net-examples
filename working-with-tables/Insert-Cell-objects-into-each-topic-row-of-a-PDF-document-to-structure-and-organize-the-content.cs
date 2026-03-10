using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_table.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Ensure there is at least one page to place the table
            Page page = doc.Pages.Count > 0 ? doc.Pages[1] : doc.Pages.Add();

            // Create a new table and set its position on the page
            Table table = new Table
            {
                // Position the table (left, top) – adjust as needed
                Left = 50,
                Top  = 700,
                // Optional visual styling
                Border = new BorderInfo(BorderSide.All, 0.5f, Color.Black),
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Color.Gray),
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5)
            };

            // Example topics – replace with actual data as required
            string[] topics = new[]
            {
                "Introduction",
                "Requirements",
                "Design",
                "Implementation",
                "Testing",
                "Deployment",
                "Conclusion"
            };

            // Add a header row
            Row headerRow = table.Rows.Add();
            Cell headerCell = headerRow.Cells.Add();
            headerCell.BackgroundColor = Color.LightGray;
            headerCell.DefaultCellTextState = new TextState
            {
                Font = FontRepository.FindFont("Helvetica-Bold"),
                FontSize = 12,
                ForegroundColor = Color.Black
            };
            headerCell.Paragraphs.Add(new TextFragment("Topic"));

            // Add a row for each topic and insert a Cell into it
            foreach (string topic in topics)
            {
                Row row = table.Rows.Add();               // Create a new row
                Cell cell = row.Cells.Add();               // Insert a new cell into the row
                cell.DefaultCellTextState = new TextState // Set text formatting
                {
                    Font = FontRepository.FindFont("Helvetica"),
                    FontSize = 10,
                    ForegroundColor = Color.DarkBlue
                };
                cell.Paragraphs.Add(new TextFragment(topic)); // Add the topic text
            }

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table with cells added and saved to '{outputPath}'.");
    }
}