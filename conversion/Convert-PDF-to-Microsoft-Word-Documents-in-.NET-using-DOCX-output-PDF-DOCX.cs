using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Directory that contains the source PDF and where the DOCX will be written.
        string dataDir = "YOUR_DATA_DIRECTORY";

        // Input PDF file.
        string pdfPath = Path.Combine(dataDir, "input.pdf");

        // Desired output DOCX file.
        string docxPath = Path.Combine(dataDir, "output.docx");

        // Verify that the source PDF exists.
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load the PDF document inside a using block (document-disposal-with-using rule).
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Configure DOCX save options (save-to-non-pdf-always-use-save-options rule).
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                // Specify DOCX output format.
                Format = DocSaveOptions.DocFormat.DocX,

                // Use the Flow recognition mode for maximum editability.
                Mode = DocSaveOptions.RecognitionMode.Flow,

                // Enable bullet recognition (optional but often useful).
                RecognizeBullets = true,

                // Adjust horizontal proximity for better paragraph detection.
                RelativeHorizontalProximity = 2.5f
            };

            // Save the PDF as a DOCX file using the explicit save options.
            pdfDoc.Save(docxPath, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to DOCX: {docxPath}");
    }
}