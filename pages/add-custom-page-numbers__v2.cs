using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

public class Program
{
    public static void Main()
    {
        // Create a sample PDF with three pages
        using (Document document = new Document())
        {
            // Add three blank pages
            document.Pages.Add();
            document.Pages.Add();
            document.Pages.Add();

            // Define the page number stamp format "current/total"
            string format = "#/#";

            // Loop through each page and add the stamp
            for (int pageNumber = 1; pageNumber <= document.Pages.Count; pageNumber++)
            {
                Page page = document.Pages[pageNumber];
                PageNumberStamp pageNumberStamp = new PageNumberStamp(format);
                pageNumberStamp.HorizontalAlignment = HorizontalAlignment.Center;
                pageNumberStamp.VerticalAlignment = VerticalAlignment.Bottom;
                pageNumberStamp.BottomMargin = 20;
                // Set text appearance
                pageNumberStamp.TextState.Font = FontRepository.FindFont("Helvetica");
                pageNumberStamp.TextState.FontSize = 12;
                pageNumberStamp.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

                page.AddStamp(pageNumberStamp);
            }

            // Save the resulting PDF
            document.Save("output.pdf");
        }
    }
}