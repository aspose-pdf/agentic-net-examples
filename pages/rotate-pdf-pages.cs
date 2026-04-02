using System;
using System.Collections.Generic;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Create a sample PDF with three pages
        using (Document sampleDoc = new Document())
        {
            sampleDoc.Pages.Add();
            sampleDoc.Pages.Add();
            sampleDoc.Pages.Add();
            sampleDoc.Save("sample.pdf");
        }

        // Define rotation angles (in degrees) for each page number
        Dictionary<int, int> pageRotations = new Dictionary<int, int>();
        pageRotations.Add(1, 0);
        pageRotations.Add(2, 90);
        pageRotations.Add(3, 180);

        // Load the PDF and apply the rotations
        using (Document doc = new Document("sample.pdf"))
        {
            foreach (KeyValuePair<int, int> kvp in pageRotations)
            {
                int pageNumber = kvp.Key;
                int angle = kvp.Value;
                if (pageNumber >= 1 && pageNumber <= doc.Pages.Count)
                {
                    Page page = doc.Pages[pageNumber];
                    page.Rotate = Page.IntToRotation(angle);
                }
            }

            // Save the rotated PDF
            doc.Save("rotated.pdf");
        }
    }
}
