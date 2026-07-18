using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string importXmlPath = "data.xml";

        if (!File.Exists(inputPdfPath) || !File.Exists(importXmlPath))
        {
            Console.Error.WriteLine("Required input files are missing.");
            return;
        }

        try
        {
            // Load the source PDF
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Initialize the Form facade with the loaded document
                Form formFacade = new Form(pdfDoc);

                // Import field values from the XML file
                using (FileStream xmlStream = new FileStream(importXmlPath, FileMode.Open, FileAccess.Read))
                {
                    formFacade.ImportXml(xmlStream);
                }

                // Examine the import result for fields that were not found
                foreach (Aspose.Pdf.Facades.Form.FormImportResult result in formFacade.ImportResult)
                {
                    if (result.Status == Aspose.Pdf.Facades.Form.ImportStatus.FieldNotFound)
                    {
                        Console.WriteLine($"Missing field: {result.FieldName}");
                    }
                }

                // Save the updated PDF
                pdfDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"Import completed. Saved to '{outputPdfPath}'.");
        }
        // Aspose.Pdf.Facades does not expose a specific FormException type.
        // Missing fields are reported via ImportResult, so we only need a generic catch.
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}
