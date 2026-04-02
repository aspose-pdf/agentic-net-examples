using System;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Step 1: Create a simple PDF with one page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();
            doc.Save("original.pdf");
        }

        // Step 2: Load the PDF and rotate the first page 90 degrees clockwise
        using (Document doc = new Document("original.pdf"))
        {
            Page page = doc.Pages[1];
            page.Rotate = Rotation.on90;
            doc.Save("rotated.pdf");
        }

        // Step 3: Load the rotated PDF and restore the page to its original orientation
        using (Document doc = new Document("rotated.pdf"))
        {
            Page page = doc.Pages[1];
            page.Rotate = Rotation.None;
            doc.Save("restored.pdf");
        }
    }
}