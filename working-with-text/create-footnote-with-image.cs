using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using Aspose.Pdf;                     // Core PDF classes
using Aspose.Pdf.Text;                // TextFragment, Note
using Aspose.Pdf.Drawing;             // Aspose.Pdf.Image for footnote

class FootnoteExample
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Create a text fragment that will contain the footnote
            TextFragment mainText = new TextFragment("This is a paragraph with a footnote reference.");

            // Create the footnote (Note) object
            Note footNote = new Note();

            // ------------------------------------------------------------
            // Generate a simple PNG image in memory and add it to the footnote
            // ------------------------------------------------------------
            MemoryStream imageStream = new MemoryStream();
            using (Bitmap bmp = new Bitmap(100, 50))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.Clear(System.Drawing.Color.LightGray);
                    g.DrawString(
                        "Img",
                        new System.Drawing.Font("Arial", 12),
                        System.Drawing.Brushes.Black,
                        new PointF(5, 5));
                }
                bmp.Save(imageStream, System.Drawing.Imaging.ImageFormat.Png);
            }
            imageStream.Position = 0; // reset for reading

            // Use Aspose.Pdf.Image for the PDF footnote image
            Aspose.Pdf.Image pdfFootImage = new Aspose.Pdf.Image { ImageStream = imageStream };
            footNote.Paragraphs.Add(pdfFootImage);

            // Optionally add descriptive text after the image
            footNote.Paragraphs.Add(new TextFragment("Figure 1: Sample illustration."));

            // Assign the footnote to the TextFragment
            mainText.FootNote = footNote;

            // Add the TextFragment (with footnote) to the page
            page.Paragraphs.Add(mainText);

            // Save the PDF
            doc.Save("output.pdf");

            // Dispose the in‑memory image stream after the document is saved
            imageStream.Dispose();
        }

        Console.WriteLine("PDF with footnote and image created successfully.");
    }
}
