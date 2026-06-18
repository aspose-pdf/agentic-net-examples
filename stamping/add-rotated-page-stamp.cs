using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

namespace AddRotatedPageStampExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a sample portrait PDF (input.pdf)
            using (Document sourceDoc = new Document())
            {
                sourceDoc.Pages.Add();
                sourceDoc.Save("input.pdf");
            }

            // Create a sample landscape PDF to be used as a stamp (stamp.pdf)
            using (Document stampDoc = new Document())
            {
                Page stampPage = stampDoc.Pages.Add();
                // Rotate the page to landscape orientation
                stampPage.Rotate = Rotation.on90;
                // Add some identifying text to the stamp page
                TextFragment tf = new TextFragment("Stamp Page");
                tf.Position = new Position(100, 500);
                stampPage.Paragraphs.Add(tf);
                stampDoc.Save("stamp.pdf");
            }

            // Open the source PDF and the stamp PDF
            using (Document sourceDoc = new Document("input.pdf"))
            {
                using (Document stampDoc = new Document("stamp.pdf"))
                {
                    // Create a page stamp from the first page of the stamp PDF
                    PdfPageStamp pageStamp = new PdfPageStamp(stampDoc.Pages[1]);
                    // Rotate the stamp content 90 degrees to match portrait orientation
                    pageStamp.Rotate = Rotation.on90;
                    // Center the stamp on the page
                    pageStamp.HorizontalAlignment = HorizontalAlignment.Center;
                    pageStamp.VerticalAlignment = VerticalAlignment.Center;

                    // Apply the stamp to each page of the source document
                    foreach (Page page in sourceDoc.Pages)
                    {
                        page.AddStamp(pageStamp);
                    }

                    // Save the resulting PDF
                    sourceDoc.Save("output.pdf");
                }
            }
        }
    }
}
