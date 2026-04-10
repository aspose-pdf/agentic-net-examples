using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // for Rotation enum if needed (actually in Aspose.Pdf)

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "rotated_page4.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least four pages
            if (doc.Pages.Count < 4)
            {
                Console.Error.WriteLine("The document does not contain a fourth page.");
                return;
            }

            // Access the fourth page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[4];

            // Rotate the page 90 degrees clockwise.
            // Use the static IntToRotation method to convert an integer angle to the Rotation enum.
            page.Rotate = Page.IntToRotation(90); // equivalent to Rotation.on90

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page 4 rotated and saved to '{outputPath}'.");
    }
}