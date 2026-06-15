using System;
using System.IO;
using Aspose.Pdf;

public class Program
{
    public static void Main()
    {
        // Create a sample PDF with three pages (evaluation mode allows up to 4 pages)
        using (Document sampleDoc = new Document())
        {
            sampleDoc.Pages.Add();
            sampleDoc.Pages.Add();
            sampleDoc.Pages.Add();
            sampleDoc.Save("input.pdf");
        }

        // Load the PDF into a byte array
        byte[] pdfBytes = File.ReadAllBytes("input.pdf");

        // Load the PDF from the byte array
        using (MemoryStream pdfStream = new MemoryStream(pdfBytes))
        {
            using (Document doc = new Document(pdfStream))
            {
                // Rotate the third page by 180 degrees (1‑based indexing)
                doc.Pages[3].Rotate = Rotation.on180;

                // Save the modified PDF
                doc.Save("output.pdf");
            }
        }
    }
}