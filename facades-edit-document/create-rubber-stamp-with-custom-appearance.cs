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
        const string appearancePath = "appearance.pdf";

        if (!File.Exists(inputPath) || !File.Exists(appearancePath))
        {
            Console.Error.WriteLine("Required files not found.");
            return;
        }

        // Load the source PDF
        using (Document doc = new Document(inputPath))
        {
            // Bind the PDF to the content editor facade
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(doc);

            // Define the annotation rectangle (System.Drawing.Rectangle is required by the API)
            System.Drawing.Rectangle annotRect = new System.Drawing.Rectangle(100, 500, 200, 100);

            // Open the custom appearance PDF as a stream
            using (FileStream appearanceStream = File.OpenRead(appearancePath))
            {
                // Create a rubber stamp annotation with a custom appearance stream
                editor.CreateRubberStamp(
                    page: 1,
                    annotRect: annotRect,
                    annotContents: "Custom Stamp",
                    color: System.Drawing.Color.Red,
                    appearanceStream: appearanceStream);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotated PDF saved to '{outputPath}'.");
    }
}