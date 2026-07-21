using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms; // for TextBoxField

class BatchAddHiddenSessionId
{
    static void Main()
    {
        // Folder containing the source PDFs
        const string inputFolder  = @"C:\PdfBatch\Input";
        // Folder where the processed PDFs will be saved
        const string outputFolder = @"C:\PdfBatch\Output";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Process every *.pdf file in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName   = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(outputFolder, fileName + "_hidden.pdf");

            try
            {
                // Bind the PDF with the Facade class Form (fully qualified to avoid ambiguity)
                using (Aspose.Pdf.Facades.Form form = new Aspose.Pdf.Facades.Form())
                {
                    form.BindPdf(inputPath);                     // load source PDF
                    Document doc = form.Document;                // underlying Document object

                    // Ensure the document has at least one page
                    if (doc.Pages.Count == 0)
                        doc.Pages.Add();

                    // Create a hidden text field named "SessionId"
                    // Position (0,0,0,0) makes the field invisible on the page
                    TextBoxField hiddenField = new TextBoxField(
                        doc.Pages[1],
                        new Aspose.Pdf.Rectangle(0, 0, 0, 0));

                    hiddenField.PartialName = "SessionId";
                    hiddenField.Value       = Guid.NewGuid().ToString();
                    // No need to set Flags – a zero‑size rectangle already hides the field.

                    // Add the field to the document's AcroForm
                    doc.Form.Add(hiddenField);

                    // Save the modified PDF using the Facade's Save method
                    form.Save(outputPath);
                }

                Console.WriteLine($"Processed: {Path.GetFileName(inputPath)} → {Path.GetFileName(outputPath)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch processing completed.");
    }
}
