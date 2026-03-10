using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class PdfComparisonDemo
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // -----------------------------------------------------------------
            // Example 1: Compare the number of pages with a threshold
            // -----------------------------------------------------------------
            int pageCount = doc.Pages.Count; // Pages are 1‑based; Count returns total pages

            // Evaluate various comparison operators
            bool moreThanFive = pageCount > 5;
            bool lessOrEqualTen = pageCount <= 10;
            bool exactlySeven = pageCount == 7;

            Console.WriteLine($"Page count: {pageCount}");
            Console.WriteLine($" > 5 ? {moreThanFive}");
            Console.WriteLine($" <= 10 ? {lessOrEqualTen}");
            Console.WriteLine($" == 7 ? {exactlySeven}");

            // -----------------------------------------------------------------
            // Example 2: Extract numeric value from the first page text and compare
            // -----------------------------------------------------------------
            // Use TextAbsorber to get all text from the first page
            TextAbsorber absorber = new TextAbsorber();
            doc.Pages[1].Accept(absorber);
            string pageText = absorber.Text ?? string.Empty;

            // Find the first integer number in the extracted text
            int extractedNumber = 0;
            Match match = Regex.Match(pageText, @"\d+");
            if (match.Success && int.TryParse(match.Value, out int number))
            {
                extractedNumber = number;
                Console.WriteLine($"Extracted number from page 1: {extractedNumber}");
            }
            else
            {
                Console.WriteLine("No numeric value found on page 1.");
            }

            // Perform comparisons on the extracted number
            bool isPositive = extractedNumber > 0;
            bool isLessThanHundred = extractedNumber < 100;
            bool isEqualTo42 = extractedNumber == 42;

            Console.WriteLine($"Number > 0 ? {isPositive}");
            Console.WriteLine($"Number < 100 ? {isLessThanHundred}");
            Console.WriteLine($"Number == 42 ? {isEqualTo42}");

            // -----------------------------------------------------------------
            // Example 3: Conditional action based on comparisons
            // -----------------------------------------------------------------
            // If the document has more than 5 pages and the extracted number is 42,
            // add a simple text annotation to the last page.
            if (moreThanFive && isEqualTo42)
            {
                // Create a rectangle for the annotation (fully qualified to avoid ambiguity)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

                // Create a TextAnnotation and set its properties
                var annotation = new Aspose.Pdf.Annotations.TextAnnotation(doc.Pages[doc.Pages.Count], rect)
                {
                    Title = "Info",
                    Contents = "Document has >5 pages and contains the number 42.",
                    Open = true,
                    Icon = Aspose.Pdf.Annotations.TextIcon.Note,
                    Color = Aspose.Pdf.Color.Yellow
                };

                // Add the annotation to the last page
                doc.Pages[doc.Pages.Count].Annotations.Add(annotation);
                Console.WriteLine("Added annotation to the last page based on comparison results.");
            }

            // Save the (potentially modified) document
            doc.Save(outputPath);
            Console.WriteLine($"Document saved to '{outputPath}'.");
        }
    }
}