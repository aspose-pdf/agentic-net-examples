using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "bates_numbered.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Create a Bates number stamp
                PageNumberStamp batesStamp = new PageNumberStamp
                {
                    // We start counting from 1 and prepend the fixed prefix "1000-"
                    StartingNumber = 1,
                    Format = "1000-{0}",               // dash separator between prefix and page number
                    HorizontalAlignment = HorizontalAlignment.Right,
                    VerticalAlignment = VerticalAlignment.Bottom,
                    BottomMargin = 10,
                    RightMargin = 10
                };

                // Configure the visual appearance of the stamp
                batesStamp.TextState.Font = FontRepository.FindFont("Arial");
                batesStamp.TextState.FontSize = 9;
                batesStamp.TextState.ForegroundColor = Color.Black;

                // Apply the stamp to every page
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    doc.Pages[i].AddStamp(batesStamp);
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Bates numbering added. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
