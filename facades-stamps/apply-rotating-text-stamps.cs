using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // for FormattedText

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "stamped_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the document to obtain the page count.
        using (Document doc = new Document(inputPath))
        {
            int pageCount = doc.Pages.Count;

            // Initialize the PdfFileStamp facade.
            using (PdfFileStamp fileStamp = new PdfFileStamp())
            {
                // Bind the source PDF.
                fileStamp.BindPdf(inputPath);

                // Create a separate stamp for each page with a unique rotation.
                for (int i = 1; i <= pageCount; i++)
                {
                    // Example: rotation angle increases by 15 degrees per page.
                    float rotationAngle = (i - 1) * 15f;

                    // Create a simple text stamp.
                    Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
                    // Use a text logo as the stamp content.
                    stamp.BindLogo(new FormattedText(
                        $"Page {i}",
                        System.Drawing.Color.Black,
                        "Helvetica",
                        EncodingType.Winansi,
                        false,
                        24));

                    // Apply rotation.
                    stamp.Rotation = rotationAngle;

                    // Restrict the stamp to the current page only.
                    stamp.Pages = new int[] { i };

                    // Add the stamp to the facade.
                    fileStamp.AddStamp(stamp);
                }

                // Save the result.
                fileStamp.Save(outputPath);
                fileStamp.Close();
            }
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}