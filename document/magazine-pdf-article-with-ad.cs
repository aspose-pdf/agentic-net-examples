using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing;

#pragma warning disable NU1903 // Suppress known high‑severity vulnerability warning for Microsoft.Bcl.Memory

class MagazinePdfCreator
{
    static void Main()
    {
        // Output file path
        const string outputPath = "magazine_article.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page (A4 size)
            Page page = doc.Pages.Add();
            page.SetPageSize(PageSize.A4.Width, PageSize.A4.Height);

            // ------------------------------------------------------------
            // 1. Add the main article text (single column – multi‑column layout
            //    can be achieved with a Table if needed; omitted for simplicity)
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

            // Create a TextFragment containing the article
            TextFragment articleFragment = new TextFragment(articleText)
            {
                // Set a readable font and size
                TextState = {
                    Font = FontRepository.FindFont("Helvetica"),
                    FontSize = 12,
                    ForegroundColor = Aspose.Pdf.Color.Black
                }
            };

            // Add the fragment to the page
            page.Paragraphs.Add(articleFragment);

            // ------------------------------------------------------------
            // 2. Add a simple rectangular advertisement at the bottom
            // ------------------------------------------------------------
            // Define the rectangle size and position (left, bottom, width, height)
            // Position it near the bottom of the page
            Aspose.Pdf.Drawing.Rectangle adRect = new Aspose.Pdf.Drawing.Rectangle(50.0f, 50.0f, 500.0f, 120.0f);
            adRect.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray,   // Background of the ad
                Color = Aspose.Pdf.Color.DarkBlue,        // Border color
                LineWidth = 1f
            };

            // Create a Graph container to hold the rectangle shape
            Graph adGraph = new Graph(500.0, 120.0);
            adGraph.Shapes.Add(adRect);

            // Add the ad graphic to the page
            page.Paragraphs.Add(adGraph);

            // Add promotional text inside the advertisement
            TextFragment adText = new TextFragment("Your Advertisement Here")
            {
                Position = new Position(200.0f, 100.0f), // Position relative to the page
                TextState = {
                    Font = FontRepository.FindFont("Helvetica-Bold"),
                    FontSize = 18,
                    ForegroundColor = Aspose.Pdf.Color.White
                }
            };
            page.Paragraphs.Add(adText);

            // ------------------------------------------------------------
            // 3. Save the PDF document
            // ------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"Magazine‑style PDF created at '{System.IO.Path.GetFullPath(outputPath)}'.");
    }
}

#pragma warning restore NU1903 // Re‑enable the warning for the rest of the project