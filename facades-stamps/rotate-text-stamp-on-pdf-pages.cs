using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath = "input.pdf";
        const string stampImagePath = "stamp.png";
        const string outputPdfPath = "stamped_output.pdf";

        // Verify files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(stampImagePath))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImagePath}");
            return;
        }

        // Create an ImageStamp (concrete class) and configure it
        ImageStamp stamp = new ImageStamp(stampImagePath);
        stamp.Background = false;          // appear on top of page content
        stamp.RotateAngle = 90;            // rotate 90 degrees clockwise
        stamp.XIndent = 0;                 // lower‑left corner X offset
        stamp.YIndent = 0;                 // lower‑left corner Y offset

        // Load the source PDF, apply the stamp to every page, and save
        using (Document doc = new Document(inputPdfPath))
        {
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(stamp);
            }
            doc.Save(outputPdfPath);
        }

        // Verify that the text in the stamped PDF is still readable
        using (Document doc = new Document(outputPdfPath))
        {
            var absorber = new TextAbsorber();
            doc.Pages.Accept(absorber);
            string extractedText = absorber.Text ?? string.Empty;

            Console.WriteLine($"Extracted text length after stamping: {extractedText.Length}");
            if (extractedText.Length > 0)
                Console.WriteLine("Text remains readable.");
            else
                Console.WriteLine("No text extracted – verification failed.");
        }
    }
}
