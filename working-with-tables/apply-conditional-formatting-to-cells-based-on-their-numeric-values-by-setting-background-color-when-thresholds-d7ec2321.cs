using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text; // for TextFragment

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "conditional_formatted.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Assume the first table on the first page is the target
            Page page = doc.Pages[1];
            Table table = page.Paragraphs.OfType<Table>().FirstOrDefault();

            if (table == null)
            {
                Console.WriteLine("No table found in the document.");
                doc.Save(outputPath); // Save unchanged document
                return;
            }

            // Define threshold values
            double highThreshold = 1000.0;   // values > 1000 get red background
            double mediumThreshold = 500.0; // values > 500 get yellow background

            // Iterate over rows and cells – use Row and Cell (the correct Aspose.Pdf types)
            foreach (Row row in table.Rows)
            {
                foreach (Cell cell in row.Cells)
                {
                    // Retrieve the cell text. In Aspose.Pdf a Cell stores its content in Paragraphs.
                    string cellText = string.Empty;
                    var textFragment = cell.Paragraphs.OfType<TextFragment>().FirstOrDefault();
                    if (textFragment != null)
                        cellText = textFragment.Text ?? string.Empty;

                    // Try to parse the cell text as a double
                    if (double.TryParse(cellText, out double numericValue))
                    {
                        // Apply background color based on thresholds
                        if (numericValue > highThreshold)
                        {
                            // Red background for high values
                            cell.BackgroundColor = Aspose.Pdf.Color.Red;
                        }
                        else if (numericValue > mediumThreshold)
                        {
                            // Yellow background for medium values
                            cell.BackgroundColor = Aspose.Pdf.Color.Yellow;
                        }
                        else
                        {
                            // Light green for low values (optional)
                            cell.BackgroundColor = Aspose.Pdf.Color.LightGreen;
                        }
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Conditional formatting applied. Saved to '{outputPath}'.");
    }
}
