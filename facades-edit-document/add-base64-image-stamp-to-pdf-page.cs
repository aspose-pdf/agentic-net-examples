using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        // Replace this string with the actual Base64-encoded image data
        const string base64Image = "iVBORw0KGgoAAAANSUhEUgAA...";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Convert Base64 string to a memory stream containing the image bytes
        byte[] imageBytes = Convert.FromBase64String(base64Image);
        using (MemoryStream imgStream = new MemoryStream(imageBytes))
        {
            // Create an ImageStamp from the image stream
            Aspose.Pdf.ImageStamp stamp = new Aspose.Pdf.ImageStamp(imgStream);

            // Convert 50 mm to points (1 point = 1/72 inch, 1 inch = 25.4 mm)
            const double mmToPoints = 72.0 / 25.4;
            double offset = 50.0 * mmToPoints; // 50 mm in points

            // Position the stamp 50 mm from the lower‑left corner of the page
            stamp.XIndent = (float)offset;
            stamp.YIndent = (float)offset;

            // Load the PDF, add the stamp to page 3, and save
            using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPdf))
            {
                if (doc.Pages.Count < 3)
                {
                    Console.Error.WriteLine("The document does not contain a third page.");
                    return;
                }

                Aspose.Pdf.Page page = doc.Pages[3];
                page.AddStamp(stamp);
                doc.Save(outputPdf);
            }
        }

        Console.WriteLine($"Image stamp added and saved to '{outputPdf}'.");
    }
}