using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class MagazinePdfCreator
{
    static void Main()
    {
        // Output file path
        const string outputPath = "magazine.pdf";

        // Advertisement image (optional)
        const string adImagePath = "ad.jpg";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page (A4 size by default)
            Page page = doc.Pages.Add();

            // Define page margins (points)
            const double marginLeft   = 50;
            const double marginTop    = 50;
            const double marginRight  = 50;
            const double marginBottom = 50;

            // ------------------------------------------------------------
            // 1. Add article text (single column – fallback when ColumnText is unavailable)
            // ------------------------------------------------------------
            // Sample long article text (repeat to fill columns)
            string articleText = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. 
Sed non risus. Suspendisse lectus tortor, dignissim sit amet, 
adipiscing nec, ultricies sed, dolor. Cras elementum ultrices diam. 
Maecenas ligula massa, varius a, semper congue, euismod non, mi. 
Proin porttitor, orci nec nonummy molestie, enim est eleifend mi, 
non fermentum diam nisl sit amet erat. Duis semper. 
Duis arcu massa, scelerisque vitae, consequat in, pretium a, enim. 
Pellentesque congue. Ut in risus volutpat libero pharetra tempor. 
Cras vestibulum bibendum augue. Praesent egestas leo in pede. 
Praesent blandit odio eu enim. Pellentesque sed dui ut augue blandit 
lacus. Vestibulum ante ipsum primis in faucibus orci luctus et 
ultrices posuere cubilia Curae; Aliquam nibh. Mauris ac mauris 
sed pede pellentesque fermentum. Maecenas adipiscing ante non 
diam sodales hendrerit.";

            // Create a TextFragment with desired formatting
            TextFragment tf = new TextFragment(articleText);
            tf.TextState.FontSize = 12;
            tf.TextState.Font = FontRepository.FindFont("Arial");
            tf.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

            // Position the fragment inside the defined margins (top‑left corner)
            tf.Position = new Position(marginLeft, page.PageInfo.Height - marginTop);

            // Add the fragment to the page
            page.Paragraphs.Add(tf);

            // ------------------------------------------------------------
            // 2. Insert an advertisement image in the right‑hand margin
            // ------------------------------------------------------------
            if (File.Exists(adImagePath))
            {
                // Create an ImageStamp for the ad
                ImageStamp adStamp = new ImageStamp(adImagePath)
                {
                    // Do not place the image behind page content
                    Background = false,
                    // Desired size of the ad (adjust as needed)
                    Width  = 150,
                    Height = 200
                };

                // Position the ad in the top‑right margin area
                // XIndent = page width - right margin - ad width
                // YIndent = page height - top margin - ad height
                adStamp.XIndent = page.PageInfo.Width - marginRight - adStamp.Width;
                adStamp.YIndent = page.PageInfo.Height - marginTop - adStamp.Height;

                // Add the stamp to the page
                page.AddStamp(adStamp);
            }

            // ------------------------------------------------------------
            // 3. Save the PDF document
            // ------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"Magazine PDF created: {Path.GetFullPath(outputPath)}");
    }
}
