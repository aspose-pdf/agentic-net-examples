using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Expect at least one image and an output PDF path.
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: <program> <image1> [<image2> ...] <outputPdf>");
            return;
        }

        // The last argument is the output PDF file.
        string outputPath = args[args.Length - 1];
        // All preceding arguments are image file paths.
        string[] imagePaths = args[..^1];

        // Verify that every image file exists.
        foreach (string imgPath in imagePaths)
        {
            if (!File.Exists(imgPath))
            {
                Console.WriteLine($"Error: Image file not found – {imgPath}");
                return;
            }
        }

        try
        {
            // Create a new blank PDF document.
            Document pdfDocument = new Document();

            // Add one page per image and place the image on that page.
            foreach (string imgPath in imagePaths)
            {
                // Add a blank page.
                Page page = pdfDocument.Pages.Add();

                // Load the image via a stream – Aspose.Pdf.Image does not have a constructor that accepts a file path.
                using (FileStream fs = new FileStream(imgPath, FileMode.Open, FileAccess.Read))
                {
                    Image img = new Image();
                    img.ImageStream = fs; // Assign the stream to the Image object.
                    page.Paragraphs.Add(img);
                }
            }

            // Save the assembled PDF using the standard Document.Save method.
            pdfDocument.Save(outputPath);

            Console.WriteLine($"Successfully created PDF '{outputPath}' with {imagePaths.Length} image(s).");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during PDF creation: {ex.Message}");
        }
    }
}
