using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string templatePath = "template.pdf";

        // Create a new blank PDF document
        using (Document doc = new Document())
        {
            // Add a single page (Aspose.Pdf uses 1‑based indexing)
            doc.Pages.Add();

            // Initialize FormEditor with the document instance
            using (FormEditor formEditor = new FormEditor(doc))
            {
                // Add a text field placeholder for a name
                // Parameters: FieldType, field name, page number, llx, lly, urx, ury
                formEditor.AddField(FieldType.Text, "NameField", 1, 50f, 700f, 250f, 730f);

                // Add a text field placeholder for an address
                formEditor.AddField(FieldType.Text, "AddressField", 1, 50f, 650f, 250f, 680f);

                // Add a checkbox placeholder for a subscription option
                formEditor.AddField(FieldType.CheckBox, "SubscribeCheck", 1, 50f, 600f, 70f, 620f);

                // Save the PDF template with the defined placeholders
                formEditor.Save(templatePath);
            }
        }

        Console.WriteLine($"PDF template with placeholders created at: {templatePath}");
    }
}