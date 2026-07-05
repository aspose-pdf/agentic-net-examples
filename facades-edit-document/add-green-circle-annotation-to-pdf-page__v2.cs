using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF document to the facade
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);

        // Define the rectangle that surrounds the diagram (placeholder coordinates)
        // System.Drawing.Rectangle expects (x, y, width, height)
        // Aspose.Pdf.Rectangle constructor uses (llx, lly, urx, ury)
        // Convert the Aspose coordinates to System.Drawing dimensions.
        System.Drawing.Rectangle rect = new System.Drawing.Rectangle(
            100,                     // x (LLX)
            100,                     // y (LLY)
            400 - 100,               // width (URX - LLX)
            300 - 100);              // height (URY - LLY)

        // Add a circle annotation (square = false) with a thick green outline on page 6
        string contents = ""; // optional annotation contents
        System.Drawing.Color outlineClr = System.Drawing.Color.Green; // fully qualified to avoid ambiguity
        bool isSquare = false; // false => circle
        int pageNumber = 6; // target page (1‑based indexing)
        int borderWidth = 5; // thickness of the outline

        editor.CreateSquareCircle(rect, contents, outlineClr, isSquare, pageNumber, borderWidth);

        // Save the modified PDF
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"Circle annotation added and saved to '{outputPath}'.");
    }
}
