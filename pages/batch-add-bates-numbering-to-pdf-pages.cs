using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input and output directories
        const string inputDir = "InputPdfs";
        const string outputDir = "OutputPdfs";

        // Verify input directory exists
        if (!Directory.Exists(inputDir))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDir}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Get all PDF files in the input folder
        string[] pdfFiles = Directory.GetFiles(inputDir, "*.pdf");

        // Bates numbering settings
        const int startNumber = 1;          // starting number
        const int increment = 5;            // increment per page
        const int numberOfDigits = 5;       // zero‑padded digits
        const string prefix = "BN";        // prefix (without separator)
        const string separator = "-";      // separator between prefix and number

        foreach (string inputPath in pdfFiles)
        {
            string fileName   = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(outputDir, $"{fileName}_bates.pdf");

            try
            {
                // Load the PDF inside a using block for deterministic disposal
                using (Document doc = new Document(inputPath))
                {
                    // Apply Bates numbering using TextStamp on each page
                    for (int i = 1; i <= doc.Pages.Count; i++)
                    {
                        int currentNumber = startNumber + (i - 1) * increment;
                        string formattedNumber = currentNumber.ToString().PadLeft(numberOfDigits, '0');
                        string stampText = $"{prefix}{separator}{formattedNumber}";

                        // Create a text stamp
                        TextStamp stamp = new TextStamp(stampText)
                        {
                            // Position the stamp at the bottom‑right corner
                            HorizontalAlignment = HorizontalAlignment.Right,
                            VerticalAlignment   = VerticalAlignment.Bottom,
                            // Small margins from the edges
                            BottomMargin = 10,
                            RightMargin  = 10
                        };

                        // Configure the visual style via the read‑only TextState object
                        stamp.TextState.Font = FontRepository.FindFont("Arial");
                        stamp.TextState.FontSize = 12;
                        stamp.TextState.FontStyle = FontStyles.Bold;
                        stamp.TextState.ForegroundColor = Color.Black;

                        // Add the stamp to the current page
                        doc.Pages[i].AddStamp(stamp);
                    }

                    // Save the modified PDF; extension .pdf ensures PDF output
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Processed: {inputPath} → {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }
    }
}
