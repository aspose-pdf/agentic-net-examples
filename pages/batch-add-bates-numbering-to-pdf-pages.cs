using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Base directory of the executable (works on any platform)
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;

        // Resolve input / output folders relative to the base directory
        string inputFolder = Path.Combine(baseDir, "InputPdfs");
        string outputFolder = Path.Combine(baseDir, "OutputPdfs");

        // Ensure the folders exist so the sample can run out‑of‑the‑box
        Directory.CreateDirectory(inputFolder);
        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine($"No PDF files found in '{inputFolder}'. Place PDFs there and rerun the program.");
            return;
        }

        // Bates‑numbering configuration (shared across the whole batch)
        int increment = 5;               // increment of 5 as required
        int numberOfDigits = 5;          // zero‑pad to 5 digits
        string prefix = "Bates-";       // optional prefix
        string suffix = string.Empty;    // optional suffix
        int nextNumber = 1;              // first number for the first document

        foreach (string inputPath in pdfFiles)
        {
            try
            {
                string fileName = Path.GetFileNameWithoutExtension(inputPath);
                string outputPath = Path.Combine(outputFolder, fileName + "_bates.pdf");

                using (Document doc = new Document(inputPath))
                {
                    int pageIndex = 0;
                    foreach (Page page in doc.Pages)
                    {
                        int currentNumber = nextNumber + pageIndex * increment;
                        string numberStr = currentNumber.ToString().PadLeft(numberOfDigits, '0');
                        string stampText = $"{prefix}{numberStr}{suffix}";

                        // Create a text stamp for the current page
                        TextStamp stamp = new TextStamp(stampText)
                        {
                            HorizontalAlignment = HorizontalAlignment.Right,
                            VerticalAlignment = VerticalAlignment.Bottom,
                            RightMargin = 20,
                            BottomMargin = 20
                        };
                        // Configure appearance
                        stamp.TextState.Font = FontRepository.FindFont("Arial");
                        stamp.TextState.FontSize = 12;
                        stamp.TextState.FontStyle = FontStyles.Bold;
                        stamp.TextState.ForegroundColor = Color.Black;

                        page.AddStamp(stamp);
                        pageIndex++;
                    }

                    // Update the nextNumber for the following document
                    nextNumber += doc.Pages.Count * increment;

                    // Save the modified PDF
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
