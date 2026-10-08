using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string csvPath   = "output.csv";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Extract raw text preserving layout (pure formatting)
                TextAbsorber absorber = new TextAbsorber();
                absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);
                doc.Pages.Accept(absorber);
                string rawText = absorber.Text;

                // Write CSV with delimiter markers around each cell
                using (StreamWriter writer = new StreamWriter(csvPath))
                {
                    // Split the extracted text into lines
                    string[] lines = rawText.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string line in lines)
                    {
                        // Assume table columns are separated by two or more spaces
                        string[] cells = Regex.Split(line.Trim(), @"\s{2,}");

                        // Add visual delimiter markers (e.g., |cell|) to each cell
                        for (int i = 0; i < cells.Length; i++)
                        {
                            cells[i] = $"|{cells[i].Trim()}|";
                        }

                        // Join cells with commas to form a CSV line
                        string csvLine = string.Join(",", cells);
                        writer.WriteLine(csvLine);
                    }
                }
            }

            Console.WriteLine($"CSV exported to '{csvPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}