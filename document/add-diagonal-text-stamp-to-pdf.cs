using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_line_stamp.pdf";
        const string message = "CONFIDENTIAL";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath))
            {
                // Iterate through all pages and add a rotated text stamp
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Aspose.Pdf.Page page = doc.Pages[i];

                    // Create a TextStamp with the custom message
                    Aspose.Pdf.TextStamp textStamp = new Aspose.Pdf.TextStamp(message)
                    {
                        Background = false,
                        Opacity = 0.5,
                        HorizontalAlignment = Aspose.Pdf.HorizontalAlignment.Center,
                        VerticalAlignment = Aspose.Pdf.VerticalAlignment.Center,
                        // Use RotateAngle (float) instead of the non‑existent Rotate property
                        RotateAngle = 45f // diagonal across the page
                    };

                    // Set font and color
                    textStamp.TextState.Font = Aspose.Pdf.Text.FontRepository.FindFont("Arial");
                    textStamp.TextState.FontSize = 72;
                    textStamp.TextState.ForegroundColor = Aspose.Pdf.Color.FromRgb(255, 0, 0); // red

                    // Add the stamp to the current page
                    page.AddStamp(textStamp);
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Line stamp added and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
