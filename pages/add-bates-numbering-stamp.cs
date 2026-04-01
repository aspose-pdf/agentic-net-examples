using System;
using Aspose.Pdf;

public class Program
{
    public static void Main()
    {
        // Create a new PDF document with a single blank page
        using (Document document = new Document())
        {
            document.Pages.Add();

            // Add Bates numbering to every page
            document.Pages.AddBatesNumbering(delegate (BatesNArtifact artifact)
            {
                // Start numbering at 1000
                artifact.StartNumber = 1000;
                // Append a dash after each number
                artifact.Suffix = "-";
                // Optional: center the stamp and set a bottom margin
                artifact.ArtifactHorizontalAlignment = HorizontalAlignment.Center;
                artifact.BottomMargin = 10f;
            });

            // Save the modified PDF
            document.Save("output.pdf");
        }
    }
}