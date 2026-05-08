using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string imagePath = "logo.png";

        if (!File.Exists(inputPdf) || !File.Exists(imagePath))
        {
            Console.Error.WriteLine("Required files not found.");
            return;
        }

        // Load the source PDF
        using (Document doc = new Document(inputPdf))
        {
            // Add the image once to the resources of the first page and obtain its name
            string imgName;
            using (FileStream fs = File.OpenRead(imagePath))
            {
                imgName = doc.Pages[1].Resources.Images.Add(fs);
            }

            // Define the rectangle where the image will be placed on each page
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(50, 700, 150, 800);

            // Reuse the same image on every page by adding it with the same stream
            foreach (Page page in doc.Pages)
            {
                using (FileStream fs = File.OpenRead(imagePath))
                {
                    page.AddImage(fs, rect);
                }
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved to '{outputPdf}'.");
    }
}