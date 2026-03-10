using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // ← added for TextFragment, FontStyles, HorizontalAlignment, VerticalAlignment, MarginInfo

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_modified.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least one page
            if (doc.Pages.Count == 0)
            {
                Console.WriteLine("Document contains no pages.");
                return;
            }

            // Access the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Retrieve various page attributes
            Console.WriteLine($"Page Number: {page.Number}");
            Console.WriteLine($"MediaBox: {page.MediaBox}");
            Console.WriteLine($"CropBox : {page.CropBox}");
            Console.WriteLine($"Rotate  : {page.Rotate}");
            Console.WriteLine($"Duration (seconds): {page.Duration}");
            Console.WriteLine($"Background color: {(page.Background != null ? page.Background.ToString() : "None")}" );

            // Manipulate page attributes
            // 1. Change rotation to 90 degrees (use the correct enum values)
            page.Rotate = Rotation.on90;

            // 2. Set a light gray background color
            page.Background = Aspose.Pdf.Color.LightGray;

            // 3. Define a display duration of 5 seconds (useful for presentations)
            page.Duration = 5.0;

            // 4. Adjust the MediaBox to a custom size (e.g., A5 size)
            //    MediaBox is defined as a rectangle: llx, lly, urx, ury
            page.MediaBox = new Rectangle(0, 0, 420, 595); // 420x595 points ≈ A5

            // 5. Set the CropBox to a smaller region within the MediaBox
            page.CropBox = new Rectangle(50, 50, 370, 545);

            // 6. Add simple header and footer text fragments
            var headerParagraph = new TextFragment("Sample Header")
            {
                TextState = { FontSize = 12, FontStyle = FontStyles.Bold, ForegroundColor = Color.Black },
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new MarginInfo(0, 0, 20, 0)
            };
            page.Paragraphs.Add(headerParagraph);

            var footerParagraph = new TextFragment($"Page {page.Number}")
            {
                TextState = { FontSize = 12, ForegroundColor = Color.Black },
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Bottom,
                Margin = new MarginInfo(0, 0, 20, 0)
            };
            page.Paragraphs.Add(footerParagraph);

            // 7. Modify the UserUnit (scale of user space units)
            page.UserUnit = 2.0; // each user unit equals 2/72 inch

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
