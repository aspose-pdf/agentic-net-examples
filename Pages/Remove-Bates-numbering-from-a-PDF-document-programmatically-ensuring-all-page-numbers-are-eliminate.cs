using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_no_bates.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document (using statement ensures proper disposal)
            using (Document doc = new Document(inputPath))
            {
                // Delete all Bates numbering artifacts from each page
                doc.Pages.DeleteBatesNumbering();

                // Save the modified document (PDF format)
                doc.Save(outputPath);
            }

            Console.WriteLine($"Bates numbering removed. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}