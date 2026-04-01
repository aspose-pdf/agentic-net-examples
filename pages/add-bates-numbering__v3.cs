using System;
using Aspose.Pdf;

public class Program
{
    public static void Main()
    {
        // Create a sample PDF with a single blank page
        using (Document document = new Document())
        {
            Page page = document.Pages.Add();

            // Add Bates numbering to each page with the format "2026-####"
            document.Pages.AddBatesNumbering((BatesNArtifact artifact) =>
            {
                artifact.Prefix = "2026-";
                artifact.NumberOfDigits = 4;
                artifact.StartNumber = 1;
            });

            // Save the PDF with Bates numbers applied
            document.Save("output.pdf");
        }
    }
}