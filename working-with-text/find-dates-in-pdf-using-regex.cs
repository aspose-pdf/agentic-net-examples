using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Regular expression to match dates in MM/DD/YYYY format
            Regex dateRegex = new Regex(@"\b\d{2}/\d{2}/\d{4}\b");

            // Create a TextFragmentAbsorber that uses regular expressions
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(dateRegex, new TextSearchOptions(true));

            // Search the entire document
            doc.Pages.Accept(absorber);

            // Output all found dates
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                Console.WriteLine($"Found date: {fragment.Text}");
            }

            // Save the (unchanged) document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processing complete. Output saved to '{outputPath}'.");
    }
}