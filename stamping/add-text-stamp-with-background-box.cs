using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;   // TextState, needed for styling the stamp

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "stamped_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a text stamp that will be placed on every page
            TextStamp txtStamp = new TextStamp("CONFIDENTIAL")
            {
                // Draw a background rectangle behind the text for readability
                Background = true,

                // Position the stamp (centered both horizontally and vertically)
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center,

                // Optional offsets (in points) from the alignment position
                XIndent = 0,
                YIndent = 0
            };

            // Style the text and its background
            txtStamp.TextState.FontSize = 36;
            txtStamp.TextState.FontStyle = FontStyles.Bold;
            txtStamp.TextState.ForegroundColor = Aspose.Pdf.Color.White;          // text color
            txtStamp.TextState.BackgroundColor = Aspose.Pdf.Color.Black;          // box color
            txtStamp.Opacity = 0.5;                                                // semi‑transparent

            // Apply the stamp to each page individually
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(txtStamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text stamp added and saved to '{outputPath}'.");
    }
}