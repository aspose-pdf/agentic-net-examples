using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // for EncodingType

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Initialize the PdfFileMend facade with the loaded document
            using (PdfFileMend mend = new PdfFileMend(doc))
            {
                // Enable word wrap and set the algorithm to wrap by whole words
                mend.IsWordWrap = true;
                mend.WrapMode = Aspose.Pdf.Facades.WordWrapMode.ByWords;

                // Create formatted text (content, color, font, encoding, embed flag, font size)
                var formattedText = new FormattedText(
                    "This is a very long paragraph that will exceed the defined width of the rectangle and should wrap by words automatically.",
                    System.Drawing.Color.Blue,
                    "Helvetica",
                    EncodingType.Winansi,
                    false,
                    12);

                // Define the rectangle area where the text will be placed
                // lower-left corner (X, Y) and upper-right corner (X, Y)
                float lowerLeftX = 50f;
                float lowerLeftY = 500f;
                float upperRightX = 550f;
                float upperRightY = 700f;

                // Add the text to page 1 within the specified rectangle
                mend.AddText(formattedText, 1, lowerLeftX, lowerLeftY, upperRightX, upperRightY);

                // Save the modified PDF document
                mend.Save(outputPath);
            }
        }

        Console.WriteLine($"Word‑wrapped paragraph added and saved to '{outputPath}'.");
    }
}