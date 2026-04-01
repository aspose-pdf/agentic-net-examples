using System;
using Aspose.Pdf;

public class RotatePageExample
{
    public static void Main()
    {
        // NOTE: In evaluation mode Aspose.PDF allows a maximum of 4 elements in any collection.
        // Therefore we create only 4 pages (the fourth page is the one we will rotate).
        using (Document doc = new Document())
        {
            for (int i = 0; i < 4; i++)
            {
                doc.Pages.Add();
            }

            // Rotate page 4 (1‑based index) by 90 degrees clockwise using the Rotation enum.
            Page pageFour = doc.Pages[4];
            pageFour.Rotate = Rotation.on90; // enum value, not an int

            // Save the modified PDF
            doc.Save("rotated.pdf");
        }
    }
}
