using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "FontCount.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (var doc = new Document(inputPath))
            {
                // Retrieve all fonts used in the document via FontUtilities
                Font[] fonts = doc.FontUtilities.GetAllFonts();
                int fontCount = fonts?.Length ?? 0;

                // Write the count to a text file
                File.WriteAllText(outputPath, fontCount.ToString());

                Console.WriteLine($"Total fonts: {fontCount}. Saved to {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
