using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "invoice_form_v4.pdf";
        const string outputPath = "cleaned_options2.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Bind the FormEditor to the loaded document
            using (FormEditor formEditor = new FormEditor())
            {
                formEditor.BindPdf(doc);

                // Delete the list item "Discount" from the ListBox named "Options"
                // Correct method name is DelListItem (not DeleteListItem)
                formEditor.DelListItem("Options", "Discount");

                // Save the changes made by FormEditor
                formEditor.Save(); // optional, changes are already applied to the bound document
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Cleaned PDF saved to '{outputPath}'.");
    }
}
