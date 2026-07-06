using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_margins.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source PDF
            using (Document doc = new Document(inputPath))
            {
                // Define three different margin sets using the correct property names (Top, Bottom, Left, Right)
                MarginInfo marginSetA = new MarginInfo
                {
                    Top = 20,
                    Bottom = 20,
                    Left = 30,
                    Right = 30
                };

                MarginInfo marginSetB = new MarginInfo
                {
                    Top = 40,
                    Bottom = 40,
                    Left = 10,
                    Right = 10
                };

                MarginInfo marginSetC = new MarginInfo
                {
                    Top = 15,
                    Bottom = 15,
                    Left = 15,
                    Right = 15
                };

                // Apply margins conditionally based on page ranges (sections)
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    // Example: pages 1-3 use marginSetA, pages 4-6 use marginSetB, rest use marginSetC
                    if (i >= 1 && i <= 3)
                    {
                        doc.Pages[i].PageInfo.Margin = marginSetA;
                    }
                    else if (i >= 4 && i <= 6)
                    {
                        doc.Pages[i].PageInfo.Margin = marginSetB;
                    }
                    else
                    {
                        doc.Pages[i].PageInfo.Margin = marginSetC;
                    }
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF saved with distinct margins: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}
