using System;
using Aspose.Pdf;

namespace AddBlankPageWithLabel
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a new PDF document
            using (Document document = new Document())
            {
                // Insert a blank page at the beginning (page number 1, 1‑based indexing)
                Page insertedPage = document.Pages.Insert(1);

                // Create a page label that will display a lowercase Roman numeral "i"
                PageLabel pageLabel = new PageLabel();
                pageLabel.NumberingStyle = NumberingStyle.NumeralsRomanLowercase;
                pageLabel.StartingValue = 1;
                pageLabel.Prefix = "";

                // Apply the label to the first page (zero‑based index)
                document.PageLabels.UpdateLabel(0, pageLabel);

                // Save the resulting PDF
                document.Save("output.pdf");
            }
        }
    }
}