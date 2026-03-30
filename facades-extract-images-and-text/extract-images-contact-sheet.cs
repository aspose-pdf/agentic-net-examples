using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "contact_sheet.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document sourceDoc = new Document(inputPath))
        using (Document sheetDoc = new Document())
        {
            // Create a new page for the contact sheet
            Page sheetPage = sheetDoc.Pages.Add();

            // Grid settings
            int columns = 3;
            float thumbWidth = 150f;
            float thumbHeight = 150f;
            float margin = 20f;

            // Starting positions (top‑left)
            float startX = margin;
            // PageInfo.Height is double – cast to float to avoid CS0266
            float startY = (float)(sheetPage.PageInfo.Height - margin - thumbHeight);

            int imageIndex = 0;

            foreach (Page srcPage in sourceDoc.Pages)
            {
                foreach (XImage xImg in srcPage.Resources.Images)
                {
                    // Compute position on the grid
                    int col = imageIndex % columns;
                    int row = imageIndex / columns;
                    float posX = startX + col * (thumbWidth + margin);
                    float posY = startY - row * (thumbHeight + margin);

                    // Aspose.Pdf.Rectangle expects double arguments (float is implicitly convertible)
                    Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(
                        posX,
                        posY,
                        posX + thumbWidth,
                        posY + thumbHeight);

                    // Write the image to a memory stream and place it on the contact sheet
                    using (MemoryStream imgStream = new MemoryStream())
                    {
                        xImg.Save(imgStream);
                        imgStream.Position = 0;
                        sheetPage.AddImage(imgStream, rect);
                    }

                    imageIndex++;
                }
            }

            sheetDoc.Save(outputPath);
            Console.WriteLine($"Contact sheet saved to '{outputPath}'.");
        }
    }
}
