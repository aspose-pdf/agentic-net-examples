using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class ExportFormFieldAppearances
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the source PDF containing the form fields
        using (Aspose.Pdf.Document sourceDoc = new Aspose.Pdf.Document(inputPdfPath))
        {
            // Iterate over each field in the form
            foreach (Aspose.Pdf.Forms.Field field in sourceDoc.Form.Fields)
            {
                // Create a fresh copy of the source document for each field
                // (so that added pages do not accumulate)
                using (Aspose.Pdf.Document tempDoc = new Aspose.Pdf.Document(inputPdfPath))
                {
                    // Add a new blank page where the appearance will be placed
                    Aspose.Pdf.Page appearancePage = tempDoc.Pages.Add();

                    // Use the field's original rectangle for placement
                    Aspose.Pdf.Rectangle fieldRect = field.Rect;

                    // Add the field's appearance to the newly added page
                    // This copies the appearance stream onto the page
                    tempDoc.Form.AddFieldAppearance(field, appearancePage.Number, fieldRect);

                    // Extract only the page that contains the appearance into a separate document
                    using (Aspose.Pdf.Document singlePageDoc = new Aspose.Pdf.Document())
                    {
                        // Pages are 1‑based indexed
                        singlePageDoc.Pages.Add(appearancePage);
                        // Build a safe file name from the field's full name
                        string safeFieldName = field.FullName.Replace("/", "_").Replace("\\", "_");
                        string outputPath = $"{safeFieldName}_appearance.pdf";

                        // Save the appearance as an independent PDF file
                        singlePageDoc.Save(outputPath);
                        Console.WriteLine($"Exported appearance of field '{field.FullName}' to '{outputPath}'.");
                    }
                }
            }
        }
    }
}