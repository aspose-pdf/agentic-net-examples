using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_rotated_stamp.pdf";
        const string stampText = "Rotated Stamp";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Create a text stamp using the Facades API
                TextStamp stamp = new TextStamp(stampText)
                {
                    // Rotate 45 degrees around the stamp's center
                    RotateAngle = 45f,

                    // Center the stamp on each page
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment   = VerticalAlignment.Center,

                    // Optional: adjust margins if needed
                    XIndent = 0,
                    YIndent = 0
                };

                // Configure visual appearance of the stamp text
                stamp.TextState.FontSize = 14;
                stamp.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;
                stamp.TextState.Font = FontRepository.FindFont("Arial");

                // Apply the stamp to every page (AddStamp is a Page method)
                foreach (Page page in doc.Pages)
                {
                    page.AddStamp(stamp);
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF saved with rotated text stamp: '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
