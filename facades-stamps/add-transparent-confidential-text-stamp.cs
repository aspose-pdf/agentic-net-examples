using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Define the pages on which the stamp should appear
        int[] selectedPages = new int[] { 1, 3, 5 };

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the PdfFileStamp facade and bind the source PDF
        using (PdfFileStamp fileStamp = new PdfFileStamp())
        {
            fileStamp.BindPdf(inputPath);

            // Create a text stamp with the desired appearance
            Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();

            // FormattedText requires System.Drawing.Color for the text color
            Aspose.Pdf.Facades.FormattedText formattedText = new Aspose.Pdf.Facades.FormattedText(
                "CONFIDENTIAL",                     // Text to display
                System.Drawing.Color.Red,           // Text color (System.Drawing.Color is required here)
                "Helvetica",                        // Font name
                EncodingType.Winansi,               // Encoding
                false,                              // Do not embed the font
                48);                                // Font size

            stamp.BindLogo(formattedText);

            // Set 70% opacity (0.7) and place the stamp behind page content
            stamp.Opacity = 0.7f;
            stamp.IsBackground = true;

            // Apply the stamp only to the selected pages
            stamp.Pages = selectedPages;

            // Add the stamp to the document and save the result
            fileStamp.AddStamp(stamp);
            fileStamp.Save(outputPath);
            fileStamp.Close();
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}