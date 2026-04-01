using System;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Create a sample PDF with a single page
        using (Document sampleDoc = new Document())
        {
            sampleDoc.Pages.Add();
            sampleDoc.Save("input.pdf");
        }

        // Open the sample PDF
        using (Document doc = new Document("input.pdf"))
        {
            // Insert a new empty page at the beginning (position 1)
            Page newPage = doc.Pages.Insert(1);
            // Set custom dimensions: 200 x 300 points
            newPage.SetPageSize(200.0, 300.0);

            // Save the modified document
            doc.Save("output.pdf");
        }
    }
}