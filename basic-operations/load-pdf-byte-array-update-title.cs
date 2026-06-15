using System;
using System.IO;
using Aspose.Pdf;

public class Program
{
    public static void Main()
    {
        // Create a sample PDF
        using (Document sampleDoc = new Document())
        {
            sampleDoc.Pages.Add();
            sampleDoc.Save("input.pdf");
        }

        // Load the PDF into a byte array
        byte[] pdfBytes = File.ReadAllBytes("input.pdf");

        // Load PDF from byte array
        using (MemoryStream ms = new MemoryStream(pdfBytes))
        {
            using (Document doc = new Document(ms))
            {
                // Update the title metadata
                doc.SetTitle("Updated PDF Title");

                // Save the modified PDF
                doc.Save("output.pdf");
            }
        }
    }
}