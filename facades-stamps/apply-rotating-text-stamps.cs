using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // for FormattedText and EncodingType

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Initialize the facade with input and output files
        PdfFileStamp fileStamp = new PdfFileStamp(inputPath, outputPath);

        // Load the document to obtain the page count
        using (Document doc = new Document(inputPath))
        {
            int pageCount = doc.Pages.Count;

            // Apply a different rotation to each page
            for (int i = 1; i <= pageCount; i++)
            {
                // Example: rotate each page by 30 degrees multiplied by its index
                float rotationAngle = (i * 30) % 360;

                // Create a simple text stamp
                Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
                stamp.BindLogo(new FormattedText(
                    $"Page {i}",
                    System.Drawing.Color.Red,   // FormattedText expects System.Drawing.Color
                    "Helvetica",
                    EncodingType.Winansi,
                    false,
                    24));

                // Set the calculated rotation
                stamp.Rotation = rotationAngle;

                // Restrict the stamp to the current page only
                stamp.Pages = new int[] { i };

                // Add the stamp to the facade
                fileStamp.AddStamp(stamp);
            }
        }

        // Finalize and write the output PDF
        fileStamp.Close();

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}