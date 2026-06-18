using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string stampImage = "stamp.jpg";

        if (!File.Exists(inputPdf) || !File.Exists(stampImage))
        {
            Console.Error.WriteLine("Input PDF or stamp image not found.");
            return;
        }

        // Load the source PDF
        using (Document doc = new Document(inputPdf))
        {
            // Initialize the PdfFileStamp facade and bind the document
            using (PdfFileStamp fileStamp = new PdfFileStamp())
            {
                fileStamp.BindPdf(doc);

                // Create a stamp, bind the image, and rotate it 90 degrees clockwise
                Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
                stamp.BindImage(stampImage);
                stamp.Rotation = 90f; // clockwise rotation

                // Optional: set position and size of the stamp
                stamp.SetOrigin(100, 100);
                stamp.SetImageSize(200, 200);
                stamp.IsBackground = false; // place on top of page content

                // Add the stamp to the PDF
                fileStamp.AddStamp(stamp);
                fileStamp.Close(); // finalize stamping
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Stamp rotated and saved to '{outputPdf}'.");
    }
}