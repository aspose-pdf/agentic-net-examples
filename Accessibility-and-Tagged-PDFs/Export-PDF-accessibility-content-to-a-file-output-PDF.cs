using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;

class ExportAccessibility
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPath = "input.pdf";
        const string outputPath = "output_accessible.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        try
        {
            // Load the existing PDF document
            Document pdfDocument = new Document(inputPath);

            // If the document contains tagged (accessibility) content,
            // prepare it for saving. PreSave builds the structure tree
            // and ensures all logical elements are correctly linked.
            if (pdfDocument.TaggedContent != null)
            {
                pdfDocument.TaggedContent.PreSave();
            }

            // Save the document – this writes the (potentially updated)
            // accessibility information to the output PDF file.
            pdfDocument.Save(outputPath);

            Console.WriteLine($"Accessibility content exported successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}