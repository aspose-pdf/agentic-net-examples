using System;
using System.Globalization;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for the source PDF and the result PDF
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // Prompt the user for the custom page width and height (points)
        Console.Write("Enter page width (points): ");
        string widthInput = Console.ReadLine();
        Console.Write("Enter page height (points): ");
        string heightInput = Console.ReadLine();

        // Parse the user input; use InvariantCulture to ensure '.' as decimal separator
        if (!double.TryParse(widthInput, NumberStyles.Float, CultureInfo.InvariantCulture, out double pageWidth) ||
            !double.TryParse(heightInput, NumberStyles.Float, CultureInfo.InvariantCulture, out double pageHeight))
        {
            Console.Error.WriteLine("Invalid numeric input for page dimensions.");
            return;
        }

        // Open the existing PDF, insert a new empty page, set its size, and save
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath))
        {
            // Insert a new empty page at the end of the document
            // Page numbers are 1‑based; inserting at Count+1 appends the page
            Aspose.Pdf.Page newPage = doc.Pages.Insert(doc.Pages.Count + 1);

            // Apply the custom size to the newly inserted page
            newPage.SetPageSize(pageWidth, pageHeight);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Custom-sized page inserted and document saved to '{outputPath}'.");
    }
}