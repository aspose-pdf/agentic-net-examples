using System;
using Aspose.Pdf;

namespace RemoveAddBatesNumbering
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a sample PDF to work with
            using (Document sampleDoc = new Document())
            {
                sampleDoc.Pages.Add();
                sampleDoc.Save("input.pdf");
            }

            // Load the PDF, delete existing Bates numbering, and add new numbering
            using (Document doc = new Document("input.pdf"))
            {
                // Delete any existing Bates numbering artifacts
                doc.Pages.DeleteBatesNumbering();

                // Configure new Bates numbering artifact
                BatesNArtifact newBates = new BatesNArtifact();
                newBates.StartNumber = 1000;
                newBates.NumberOfDigits = 5;
                newBates.Prefix = "DOC-";
                newBates.Position = new Aspose.Pdf.Point(100f, 20f);
                newBates.IsBackground = false;

                // Add the new Bates numbering to all pages
                doc.Pages.AddBatesNumbering(newBates);

                // Save the updated PDF
                doc.Save("output.pdf");
            }
        }
    }
}
