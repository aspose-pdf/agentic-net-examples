using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class MagazineCreator
{
    static void Main()
    {
        const string outputPath = "Magazine.pdf";
        const string adImagePath = "advertisement.jpg";

        // Sample article text – in a real scenario load from a file or other source
        string articleText = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. 
Sed non risus. Suspendisse lectus tortor, dignissim sit amet, adipiscing nec, 
ultricies sed, dolor. Cras elementum ultrices diam. Maecenas ligula massa, 
varius a, semper congue, euismod non, mi. Proin porttitor, orci nec nonummy 
molestie, enim est eleifend mi, non fermentum diam nisl sit amet erat. 
Duis semper. Duis arcu massa, scelerisque vitae, consequat in, pretium a, 
enim. Pellentesque congue. Ut in risus volutpat libero pharetra tempor. 
Cras vestibulum bibendum augue. Praesent egestas leo in pede. 
Praesent blandit odio eu enim. Pellentesque sed dui ut augue blandit 
cursus. Vestibulum ante ipsum primis in faucibus orci luctus et 
ultrices posuere cubilia Curae; Aliquam nibh. Mauris ac mauris sed 
pede pellentesque fermentum. Maecenas adipiscing ante non diam sodales 
hendrerit.";

        // Ensure advertisement image exists; otherwise skip adding it
        bool adImageExists = File.Exists(adImagePath);

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // -------------------------------------------------
            // 1. Add a page for the magazine article (two‑column layout)
            // -------------------------------------------------
            Page articlePage = doc.Pages.Add();

            // Split the article text roughly in half for a simple two‑column simulation
            string[] words = articleText.Split(new[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            int mid = words.Length / 2;
            string leftText = string.Join(" ", words, 0, mid);
            string rightText = string.Join(" ", words, mid, words.Length - mid);

            // Create a table with two columns to mimic newspaper columns
            Table table = new Table();
            table.ColumnWidths = "50% 50%";
            // Hide borders
            table.DefaultCellBorder = new BorderInfo(BorderSide.All, 0);

            // Add a single row
            Row row = table.Rows.Add();

            // Left column cell
            Cell leftCell = row.Cells.Add();
            TextFragment leftFragment = new TextFragment(leftText);
            leftFragment.TextState.Font = FontRepository.FindFont("Arial");
            leftFragment.TextState.FontSize = 12;
            leftFragment.TextState.ForegroundColor = Color.Black;
            leftCell.Paragraphs.Add(leftFragment);

            // Right column cell
            Cell rightCell = row.Cells.Add();
            TextFragment rightFragment = new TextFragment(rightText);
            rightFragment.TextState.Font = FontRepository.FindFont("Arial");
            rightFragment.TextState.FontSize = 12;
            rightFragment.TextState.ForegroundColor = Color.Black;
            rightCell.Paragraphs.Add(rightFragment);

            // Add the table to the page
            articlePage.Paragraphs.Add(table);

            // -------------------------------------------------
            // 2. Add an advertisement page (optional)
            // -------------------------------------------------
            if (adImageExists)
            {
                Page adPage = doc.Pages.Add();

                // ImageStamp works as a full‑page background image
                ImageStamp adStamp = new ImageStamp(adImagePath)
                {
                    Width = adPage.PageInfo.Width,
                    Height = adPage.PageInfo.Height,
                    Background = false, // draw on top of page content
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };

                // Apply the stamp to the page
                adPage.AddStamp(adStamp);
            }

            // -------------------------------------------------
            // 3. Save the resulting PDF
            // -------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"Magazine PDF created at '{Path.GetFullPath(outputPath)}'.");
    }
}
