using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input MHT file (HTML archive) and output PDF file paths
        const string mhtPath = "input.mht";
        const string pdfPath = "output.pdf";

        // Validate input file existence
        if (!File.Exists(mhtPath))
        {
            Console.Error.WriteLine($"Error: MHT file not found at '{mhtPath}'.");
            return;
        }

        try
        {
            // Load the MHT file into a PDF Document using MhtLoadOptions
            MhtLoadOptions loadOptions = new MhtLoadOptions();
            using (Document pdfDocument = new Document(mhtPath, loadOptions))
            {
                // -------------------------------------------------
                // Add a text annotation with a title to the first page
                // -------------------------------------------------
                // Define the rectangle (llx, lly, urx, ury) for the annotation
                // Adjust coordinates as needed; here we place it near the top‑left corner
                Rectangle annotRect = new Rectangle(50, 750, 300, 800);

                // Create the TextAnnotation
                TextAnnotation textAnnot = new TextAnnotation(pdfDocument.Pages[1], annotRect)
                {
                    Title = "Sample Title",          // annotation title (tooltip)
                    Contents = "This is a text annotation added via Aspose.Pdf.Facades example.",
                    Color = Color.Yellow,           // background color of the annotation
                    Opacity = 0.5f
                };

                // Set a border for the annotation
                textAnnot.Border = new Border(textAnnot)
                {
                    Style = BorderStyle.Solid,
                    Width = 1
                };

                // Add the annotation to the page
                pdfDocument.Pages[1].Annotations.Add(textAnnot);

                // Save the PDF document (uses the provided document-save rule)
                pdfDocument.Save(pdfPath);
            }

            // -------------------------------------------------
            // Read PDF metadata using the PdfFileInfo facade
            // -------------------------------------------------
            PdfFileInfo pdfInfo = new PdfFileInfo(pdfPath);

            Console.WriteLine("PDF Metadata:");
            Console.WriteLine($" Title       : {pdfInfo.Title}");
            Console.WriteLine($" Author      : {pdfInfo.Author}");
            Console.WriteLine($" Subject     : {pdfInfo.Subject}");
            Console.WriteLine($" Keywords    : {pdfInfo.Keywords}");
            Console.WriteLine($" Creator     : {pdfInfo.Creator}");
            Console.WriteLine($" Producer    : {pdfInfo.Producer}");
            Console.WriteLine($" CreationDate: {pdfInfo.CreationDate}");
            Console.WriteLine($" ModDate     : {pdfInfo.ModDate}");
            Console.WriteLine($" Pages       : {pdfInfo.NumberOfPages}");
            Console.WriteLine($" Encrypted   : {pdfInfo.IsEncrypted}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}