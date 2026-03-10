using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Aspose.Pdf;
using Aspose.Pdf.Text;

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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Extract all text from the document using TextAbsorber
            TextAbsorber absorber = new TextAbsorber();
            doc.Pages.Accept(absorber);
            string allText = absorber.Text ?? string.Empty;

            // Find all numeric values (integers or decimals) in the extracted text
            List<double> numbers = new List<double>();
            foreach (Match match in Regex.Matches(allText, @"-?\d+(\.\d+)?"))
            {
                if (double.TryParse(match.Value, out double value))
                {
                    numbers.Add(value);
                }
            }

            // Prepare results of arithmetic operations
            double sum = 0;
            double product = 1;
            double subtraction = 0;
            double division = 0;
            bool first = true;

            foreach (double num in numbers)
            {
                sum += num;
                product *= num;

                if (first)
                {
                    subtraction = num;
                    division = num;
                    first = false;
                }
                else
                {
                    subtraction -= num;
                    // Guard against division by zero
                    division = num != 0 ? division / num : division;
                }
            }

            // Build a result string
            string resultText = $"Found {numbers.Count} numeric value(s).\n" +
                                $"Sum: {sum}\n" +
                                $"Subtraction (sequential): {subtraction}\n" +
                                $"Product: {product}\n" +
                                $"Division (sequential, zero‑safe): {division}";

            // Add a new page to the document to display the results
            Page resultPage = doc.Pages.Add();

            // Create a TextFragment with the result text
            TextFragment fragment = new TextFragment(resultText);
            fragment.TextState.FontSize = 12;
            fragment.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

            // Add the fragment to the new page
            resultPage.Paragraphs.Add(fragment);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processing complete. Output saved to '{outputPath}'.");
    }
}