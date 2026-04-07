using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Add a new blank page at the end of the document
            doc.Pages.Add();

            // The new page number (Aspose.Pdf uses 1‑based indexing)
            int newPageNumber = doc.Pages.Count;

            // Initialize FormEditor with the loaded document
            using (FormEditor formEditor = new FormEditor(doc))
            {
                // Add a text box field on the new page
                formEditor.AddField(FieldType.Text, "NewTextField", newPageNumber,
                    100, 600, 300, 630); // llx, lly, urx, ury

                // Add a check box field on the new page
                formEditor.AddField(FieldType.CheckBox, "NewCheckBox", newPageNumber,
                    100, 560, 120, 580);

                // Add a radio button field on the new page
                formEditor.AddField(FieldType.Radio, "NewRadioGroup", newPageNumber,
                    100, 520, 120, 540);

                // Add a signature field on the new page
                formEditor.AddField(FieldType.Signature, "NewSignature", newPageNumber,
                    100, 460, 250, 510);

                // Persist the changes to a new file
                formEditor.Save(outputPath);
            }
        }

        Console.WriteLine($"PDF with added page and form fields saved to '{outputPath}'.");
    }
}