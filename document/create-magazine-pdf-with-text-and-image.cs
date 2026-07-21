using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;   // needed for TextFragment

class MagazinePdfGenerator
{
    static void Main()
    {
        const string outputPath = "magazine_article.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page with default size (A4)
            Page page = doc.Pages.Add();

            // ------------------------------------------------------------
            // Add article text (single column – Aspose.Pdf core API does not expose ColumnInfo on TextFragment in recent versions)
            // ------------------------------------------------------------
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

            // Create a TextFragment to hold the article text
            TextFragment tf = new TextFragment(articleText);
            // Set a readable font and size
            tf.TextState.Font = FontRepository.FindFont("Helvetica");
            tf.TextState.FontSize = 12;
            tf.TextState.ForegroundColor = Color.Black;

            // Add the text fragment to the page
            page.Paragraphs.Add(tf);

            // ------------------------------------------------------------
            // Add an advertisement image on the right side of the page
            // ------------------------------------------------------------
            // Assume an image file "ad_banner.jpg" exists in the same folder
            string adImagePath = "ad_banner.jpg";
            if (File.Exists(adImagePath))
            {
                // Define a rectangle where the ad will be placed (right column, top area)
                // Coordinates are in points; origin is bottom‑left.
                // Here we place the ad at the top‑right corner, 200pt wide, 100pt high.
                Aspose.Pdf.Rectangle adRect = new Aspose.Pdf.Rectangle(
                    page.Rect.URX - 210,   // left (page width - margin - ad width)
                    page.Rect.URY - 110,   // bottom (page height - margin - ad height)
                    page.Rect.URX - 10,    // right (page right margin)
                    page.Rect.URY - 10);   // top (page top margin)

                // Add the image to the page within the defined rectangle
                page.AddImage(adImagePath, adRect);
            }

            // ------------------------------------------------------------
            // Save the PDF document
            // ------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"Magazine‑style PDF saved to '{outputPath}'.");
    }
}
