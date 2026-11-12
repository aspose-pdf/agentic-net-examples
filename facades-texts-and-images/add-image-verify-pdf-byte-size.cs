using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string InputPdfPath = "input.pdf";
        const string ImagePath = "image.png";
        const string OutputPdfPath = "output.pdf";

        // Verify that the required files exist
        if (!File.Exists(InputPdfPath))
        {
            Console.WriteLine($"FAIL Missing PDF: {InputPdfPath}");
            return;
        }
        if (!File.Exists(ImagePath))
        {
            Console.WriteLine($"FAIL Missing image: {ImagePath}");
            return;
        }

        // Record original file size
        long originalSize = new FileInfo(InputPdfPath).Length;

        // Load PDF, add image using ImageStamp, and save
        using (Document doc = new Document(InputPdfPath))
        {
            ImageStamp stamp = new ImageStamp(ImagePath)
            {
                XIndent = 50,
                YIndent = 750,
                Width   = 100,
                Height  = 50
            };

            foreach (Page page in doc.Pages)
            {
                page.AddStamp(stamp);
            }

            doc.Save(OutputPdfPath);
        }

        // Record modified file size
        long modifiedSize = new FileInfo(OutputPdfPath).Length;

        // Simple verification – print PASS/FAIL instead of using a test framework
        if (modifiedSize > originalSize)
        {
            Console.WriteLine("PASS PDF size increased after adding image.");
        }
        else
        {
            Console.WriteLine($"FAIL PDF size not increased. Original: {originalSize}, Modified: {modifiedSize}");
        }
    }
}