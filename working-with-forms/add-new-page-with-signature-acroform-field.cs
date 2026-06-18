using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_new_page.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF (using statement ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Add a new blank page at the end of the document
            Page newPage = doc.Pages.Add();

            // Define the rectangle where the AcroForm field will be placed
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a signature field on the newly added page
            // The constructor places the field on the specified page and rectangle
            SignatureField sigField = new SignatureField(newPage, fieldRect);

            // Add the field to the document's form collection
            // Using the overload without page number because the field is already associated with the page
            doc.Form.Add(sigField);

            // Save the modified PDF (PDF format, so no SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with new page and AcroForm field: {outputPath}");
    }
}