using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string companyName = "Acme Corporation";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Iterate through all pages (1‑based indexing)
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Page page = doc.Pages[i];

                    // Create a text stamp for the header
                    TextStamp headerStamp = new TextStamp(companyName);
                    headerStamp.TextState.Font = FontRepository.FindFont("Helvetica");
                    headerStamp.TextState.FontSize = 12;
                    headerStamp.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
                    headerStamp.HorizontalAlignment = Aspose.Pdf.HorizontalAlignment.Center;
                    headerStamp.VerticalAlignment = Aspose.Pdf.VerticalAlignment.Top;
                    headerStamp.YIndent = 20f; // distance from the top edge

                    // Add the stamp to the current page
                    page.AddStamp(headerStamp);
                }

                // Save the modified document
                doc.Save(outputPath);
                Console.WriteLine($"Header added and saved to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}