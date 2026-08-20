using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for the source PDF and the resulting portfolio PDF
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "portfolio.pdf";

        // Files that will be embedded into the portfolio
        string[] filesToEmbed = { "file1.txt", "image1.png", "doc1.docx" };

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdfPath}");
            return;
        }

        // Verify that each attachment file exists
        foreach (string attachment in filesToEmbed)
        {
            if (!File.Exists(attachment))
            {
                Console.Error.WriteLine($"Attachment file not found: {attachment}");
                return;
            }
        }

        // Open the existing PDF inside a using block for deterministic disposal
        using (Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document(inputPdfPath))
        {
            // Ensure the document has at least one page (required for a portfolio)
            if (pdfDoc.Pages.Count == 0)
            {
                pdfDoc.Pages.Add();
            }

            // Add each file as an embedded file (FileSpecification) to the document
            foreach (string filePath in filesToEmbed)
            {
                // Create a FileSpecification for the attachment
                Aspose.Pdf.FileSpecification fileSpec = new Aspose.Pdf.FileSpecification(filePath);
                // Optional: set a description (displayed in PDF viewers)
                fileSpec.Description = Path.GetFileName(filePath);

                // Add the file specification to the EmbeddedFiles collection
                pdfDoc.EmbeddedFiles.Add(fileSpec);
            }

            // Save the modified document; the presence of embedded files makes it a PDF Portfolio
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Portfolio PDF created successfully at '{outputPdfPath}'.");
    }
}