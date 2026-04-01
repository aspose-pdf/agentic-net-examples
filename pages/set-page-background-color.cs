using System;
using Aspose.Pdf;

public class SetPageBackgroundColor
{
    public static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Define the background color using RGB values (example corporate blue)
            Aspose.Pdf.Color backgroundColor = Aspose.Pdf.Color.FromArgb(0, 112, 192);

            // Apply the background color to the page
            page.Background = backgroundColor;

            // Save the PDF document
            doc.Save("output.pdf");
        }
    }
}