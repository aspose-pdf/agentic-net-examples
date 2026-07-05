using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFdfPath = "checkboxes.fdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the source PDF
        using (Document sourceDoc = new Document(inputPdfPath))
        {
            // Create a new empty PDF that will contain only checkbox fields
            using (Document tempDoc = new Document())
            {
                // Copy all pages from the source document
                tempDoc.Pages.Add(sourceDoc.Pages);

                // Iterate over all form fields in the source document
                foreach (Field srcField in sourceDoc.Form.Fields)
                {
                    // Keep only checkbox fields
                    if (srcField is CheckboxField)
                    {
                        // Clone the field to avoid referencing the original document's objects
                        Field clonedField = (Field)srcField.Clone();
                        tempDoc.Form.Add(clonedField);
                    }
                }

                // Use the Facade Form class to export the fields to FDF (fully qualified to avoid ambiguity)
                using (Aspose.Pdf.Facades.Form formFacade = new Aspose.Pdf.Facades.Form(tempDoc))
                {
                    using (FileStream fdfStream = new FileStream(outputFdfPath, FileMode.Create, FileAccess.Write))
                    {
                        formFacade.ExportFdf(fdfStream);
                    }
                }
            }
        }

        Console.WriteLine($"Checkbox field definitions exported to '{outputFdfPath}'.");
    }
}
