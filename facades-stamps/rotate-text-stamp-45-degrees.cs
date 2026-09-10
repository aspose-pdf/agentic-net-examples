using System;
using System.IO;
using Aspose.Pdf;                 // Core PDF API
using Aspose.Pdf.Text;           // TextStamp class

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "rotated_stamp.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Open the source PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Create a textual stamp with the desired text
            TextStamp textStamp = new TextStamp("CONFIDENTIAL");

            // Rotate the stamp 45 degrees around its centre
            // RotateAngle allows arbitrary angles (in degrees)
            textStamp.RotateAngle = 45;

            // Optional: position the stamp on the page
            textStamp.XIndent = 100; // distance from the left edge
            textStamp.YIndent = 500; // distance from the bottom edge

            // Add the stamp to the page
            page.AddStamp(textStamp);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with rotated text stamp: {outputPdf}");
    }
}