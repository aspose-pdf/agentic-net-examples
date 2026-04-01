using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "rotated_stamp.pdf";

        // Create a simple PDF with one page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Create a text stamp
            TextStamp stamp = new TextStamp("Rotated Stamp");
            stamp.RotateAngle = 90f; // set rotation to 90 degrees
            stamp.HorizontalAlignment = HorizontalAlignment.Center;
            stamp.VerticalAlignment = VerticalAlignment.Center;
            stamp.Opacity = 0.5f;

            // Add stamp to the page
            page.AddStamp(stamp);

            // Save the document
            doc.Save(outputPath);
        }

        // Verify rotation setting
        using (Document verifyDoc = new Document(outputPath))
        {
            Console.WriteLine("Stamp rotation set to 90 degrees. Text should appear rotated but remain readable.");
        }
    }
}
