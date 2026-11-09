using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Regular expression to match dates in MM/DD/YYYY format
        // Captures month (group 1), day (group 2) and year (group 3)
        Regex datePattern = new Regex(@"\b(\d{2})/(\d{2})/(\d{4})\b");

        // Load the PDF document
        Document pdfDocument = new Document(inputPath);

        // Use TextFragmentAbsorber with the regex to find all matching fragments
        TextFragmentAbsorber absorber = new TextFragmentAbsorber(datePattern);
        pdfDocument.Pages.Accept(absorber);

        // Iterate over each found fragment and replace the text with the desired format
        foreach (TextFragment fragment in absorber.TextFragments)
        {
            Match m = datePattern.Match(fragment.Text);
            if (m.Success)
            {
                // Reorder captured groups to YYYY-MM-DD
                fragment.Text = $"{m.Groups[3].Value}-{m.Groups[1].Value}-{m.Groups[2].Value}";
            }
        }

        // Save the modified PDF
        pdfDocument.Save(outputPath);

        Console.WriteLine($"Dates reformatted and saved to '{outputPath}'.");
    }
}
