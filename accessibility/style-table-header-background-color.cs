using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath  = "tagged_input.pdf";
        const string outputPath = "styled_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content interface
            ITaggedContent taggedContent = doc.TaggedContent;

            // Ensure the document actually contains a tagged structure
            if (taggedContent == null || taggedContent.RootElement == null)
            {
                Console.WriteLine("The PDF is not tagged. No changes applied.");
                doc.Save(outputPath);
                return;
            }

            // Find all table header cells (TH elements) in the structure tree
            var headerCells = taggedContent.RootElement.FindElements<TableTHElement>(true);

            // Apply a background color to each header cell
            foreach (TableTHElement th in headerCells)
            {
                // Use Aspose.Pdf.Color (cross‑platform) instead of System.Drawing.Color
                th.BackgroundColor = Aspose.Pdf.Color.LightGray;
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Styled PDF saved to '{outputPath}'.");
    }
}