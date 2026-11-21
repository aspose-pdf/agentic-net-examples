using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Collect custom page dimensions from the user (points)
        double width = PromptForPositiveDouble("Enter page width (points): ");
        double height = PromptForPositiveDouble("Enter page height (points): ");

        try
        {
            // Open the existing PDF inside a using block for deterministic disposal
            using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath))
            {
                // Add a new empty page at the end of the document
                Aspose.Pdf.Page newPage = doc.Pages.Add();

                // Apply the custom size to the newly added page
                newPage.SetPageSize(width, height);

                // Save the modified document
                doc.Save(outputPath);
            }

            Console.WriteLine($"Empty page added (size: {width}×{height} points). Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Helper method to read a positive double value from the console
    static double PromptForPositiveDouble(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine();
            if (double.TryParse(input, out double value) && value > 0)
                return value;

            Console.WriteLine("Invalid input. Please enter a positive numeric value.");
        }
    }
}